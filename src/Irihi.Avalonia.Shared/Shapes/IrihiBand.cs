using System.Linq;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Irihi.Avalonia.Shared.Shapes;

public partial class IrihiBand : Shape
{
    // === Design-time layout constants (in logical units) ===

    private const int BandHeight = 8;

    // Logo
    private const int LogoWidth = 8;
    private const int LogoHeight = 6;
    private const int LogoX = LeftMargin;
    private const int LogoY = (BandHeight - LogoHeight) / 2; // = 1

    // Letter
    private const int LetterHeight = 5;

    // Spacing
    private const int LeftMargin = 1;
    private const int RightMargin = 4;
    private const int BorderThickness = 1;
    private const int LetterGap = 1;
    private const int LetterPadding = 1; // extra inset inside border before first letter

    // Derived
    private const int LeftFillX = 0;
    private const int LeftFillWidth = LeftMargin + LogoWidth;            // = 9
    private const int TextStartX = LeftFillX + LeftFillWidth;           // = 9
    private const int InnerX = TextStartX + BorderThickness;            // = 10
    private const int InnerY = BorderThickness;                         // = 1
    private const int InnerHeight = BandHeight - BorderThickness;       // = 7 (bottom-open)
    private const int LetterX = InnerX + LetterPadding;                 // = 11
    private const int LetterY = InnerY + LetterPadding;                 // = 2

    // ------------------------------------------------------------------------

    public static readonly StyledProperty<string?> LabelProperty = AvaloniaProperty.Register<IrihiBand, string?>(
        nameof(Label));

    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    static IrihiBand()
    {
        AffectsGeometry<IrihiBand>(LabelProperty);
        AffectsMeasure<IrihiBand>(LabelProperty);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var label = NormalizeLabel(Label);
        if (label is null)
            return base.MeasureOverride(availableSize);

        var expectedWidth = GetExpectedWidth(label);
        var aspectRatio = (double)expectedWidth / BandHeight;

        var width = Math.Min(availableSize.Width, availableSize.Height * aspectRatio);
        var height = width / aspectRatio;

        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        return DesiredSize;
    }

    protected override Geometry? CreateDefiningGeometry()
    {
        var label = NormalizeLabel(Label);
        if (label is null)
            return null;

        var expectedWidth = GetExpectedWidth(label);

        var unitW = double.IsNaN(Width) ? double.PositiveInfinity : Width / expectedWidth;
        var unitH = double.IsNaN(Height) ? double.PositiveInfinity : Height / BandHeight;
        var unit = Math.Min(unitW, unitH);
        if (double.IsInfinity(unit) || unit is 0) return null;

        return CreateGeometry(label, unit, expectedWidth);
    }

    // ------------------------------------------------------------------------

    private static List<char>? NormalizeLabel(string? raw)
    {
        if (string.IsNullOrEmpty(raw))
            return null;

        var result = raw
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToList();

        return result.Count == 0 ? null : result;
    }

    private static int GetExpectedWidth(List<char> label)
    {
        var result = LeftFillWidth; // LogoWidth + LeftMargin
        foreach (var c in label)
        {
            if (GlyphWidthMapping.TryGetValue(c, out var width))
                result += width;
        }

        result += (label.Count - 1) * LetterGap;
        result += RightMargin;

        return result;
    }

    private Geometry CreateGeometry(List<char> label, double unit, int expectedWidth)
    {
        // === 左边：填充区域（阳）抠出 logo（阴）===
        var leftFill = new RectangleGeometry(new Rect(
            LeftFillX * unit, 0,
            LeftFillWidth * unit, BandHeight * unit));

        var logoGeo = CreateBitmapGeometry(IrihiLogoBitmap,
            LogoX * unit, LogoY * unit, unit, unit);

        var leftPart = new CombinedGeometry(GeometryCombineMode.Exclude, leftFill, logoGeo);

        // === 右边：边框（阳）+ 字母（阳），内部空白（阴）===
        var textWidth = expectedWidth - TextStartX;

        var rightOuter = new RectangleGeometry(new Rect(
            TextStartX * unit, 0,
            textWidth * unit, BandHeight * unit));

        var rightInner = new RectangleGeometry(new Rect(
            InnerX * unit, InnerY * unit,
            (textWidth - BorderThickness * 2) * unit, InnerHeight * unit));

        var rightBorder = new CombinedGeometry(GeometryCombineMode.Exclude, rightOuter, rightInner);

        var lettersGeo = CreateLettersGeometry(label,
            LetterX * unit, LetterY * unit, unit);

        var rightPart = new CombinedGeometry(GeometryCombineMode.Union, rightBorder, lettersGeo);

        return new CombinedGeometry(GeometryCombineMode.Union, leftPart, rightPart);
    }

    private static Geometry CreateBitmapGeometry(byte[,] bitmap, double startX, double startY, double cellW, double cellH)
    {
        var group = new GeometryGroup { FillRule = FillRule.NonZero };
        var rows = bitmap.GetLength(0);
        var cols = bitmap.GetLength(1);
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (bitmap[r, c] == 1)
                {
                    group.Children.Add(new RectangleGeometry(
                        new Rect(startX + c * cellW, startY + r * cellH, cellW, cellH)));
                }
            }
        }
        return group;
    }

    private Geometry CreateLettersGeometry(List<char> label, double startX, double startY, double unit)
    {
        var group = new GeometryGroup { FillRule = FillRule.NonZero };
        var x = startX;
        foreach (var c in label)
        {
            if (GlyphMappings.TryGetValue(c, out var bitmap))
            {
                var charWidth = bitmap.GetLength(1);
                group.Children.Add(CreateBitmapGeometry(bitmap, x, startY, unit, unit));
                x += (charWidth + LetterGap) * unit;
            }
        }
        return group;
    }
}
