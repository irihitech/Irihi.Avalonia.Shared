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

    public static readonly StyledProperty<double> CornerRatioProperty = AvaloniaProperty.Register<IrihiBand, double>(
        nameof(CornerRatio));

    public double CornerRatio
    {
        get => GetValue(CornerRatioProperty);
        set => SetValue(CornerRatioProperty, value);
    }

    static IrihiBand()
    {
        AffectsGeometry<IrihiBand>(LabelProperty, CornerRatioProperty);
        AffectsMeasure<IrihiBand>(LabelProperty);
    }

    private List<List<(int r, int c)>>? _cachedContours;
    private int _cachedExpectedWidth;

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == LabelProperty)
            _cachedContours = null;
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
            .Select(c => char.IsLetter(c) ? char.ToUpper(c) : c)
            .Where(GlyphMappings.ContainsKey)
            .ToList();

        return result.Count == 0 ? null : result;
    }

    private static int GetExpectedWidth(List<char> label)
    {
        var result = LeftFillWidth;
        foreach (var c in label)
        {
            if (GlyphWidthMapping.TryGetValue(c, out var width))
                result += width;
        }

        result += (label.Count - 1) * LetterGap;
        result += RightMargin;

        return result;
    }

    // ------------------------------------------------------------------------
    // Bitmap → directed-edge extraction → contour tracing → StreamGeometry
    // ------------------------------------------------------------------------

    private const int BitmapMargin = 1;

    private Geometry CreateGeometry(List<char> label, double unit, int expectedWidth)
    {
        if (_cachedContours == null || _cachedExpectedWidth != expectedWidth)
        {
            var bitmap = BuildFullBitmap(label, expectedWidth);
            var edges = BitmapContourTracer.ExtractDirectedEdges(bitmap);
            _cachedContours = BitmapContourTracer.TraceContours(edges);
            _cachedExpectedWidth = expectedWidth;
        }

        return BitmapContourTracer.BuildGeometry(_cachedContours, unit, CornerRatio, BitmapMargin);
    }

    /// <summary>
    /// Build a binary bitmap with a 1-pixel zero-margin on all sides,
    /// so that every filled region is surrounded by zeros — no edge-case boundaries.
    /// </summary>
    private byte[,] BuildFullBitmap(List<char> label, int expectedWidth)
    {
        var h = BandHeight + BitmapMargin * 2;
        var w = expectedWidth + BitmapMargin * 2;
        var bitmap = new byte[h, w];

        // 左边填充（阳）— offset by Margin
        for (var x = LeftFillX + BitmapMargin; x < LeftFillX + LeftFillWidth + BitmapMargin; x++)
        for (var y = BitmapMargin; y < BandHeight + BitmapMargin; y++)
            bitmap[y, x] = 1;

        // Logo 镂空（阴）
        for (var r = 0; r < LogoHeight; r++)
        for (var c = 0; c < LogoWidth; c++)
            if (IrihiLogoBitmap[r, c] == 1)
                bitmap[LogoY + r + BitmapMargin, LogoX + c + BitmapMargin] = 0;

        // 右边边框（阳）
        for (var x = TextStartX + BitmapMargin; x < expectedWidth + BitmapMargin; x++)
        for (var y = BitmapMargin; y < BandHeight + BitmapMargin; y++)
            bitmap[y, x] = 1;

        // 右边内部镂空（阴）
        for (var x = InnerX + BitmapMargin; x < expectedWidth - BorderThickness + BitmapMargin; x++)
        for (var y = InnerY + BitmapMargin; y < InnerY + InnerHeight + BitmapMargin; y++)
            bitmap[y, x] = 0;

        // 字母填充（阳）
        var letterX = LetterX + BitmapMargin;
        foreach (var c in label)
        {
            if (GlyphMappings.TryGetValue(c, out var letterBitmap))
            {
                var charW = letterBitmap.GetLength(1);
                var charH = letterBitmap.GetLength(0);
                for (var r = 0; r < charH; r++)
                for (var col = 0; col < charW; col++)
                    if (letterBitmap[r, col] == 1)
                        bitmap[LetterY + r + BitmapMargin, letterX + col] = 1;
                letterX += charW + LetterGap;
            }
        }

        return bitmap;
    }
}
