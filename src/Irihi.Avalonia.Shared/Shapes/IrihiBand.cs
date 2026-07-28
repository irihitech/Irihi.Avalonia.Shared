using System.Linq;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Irihi.Avalonia.Shared.Shapes;

public partial class IrihiBand: Shape
{
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
        if (string.IsNullOrEmpty(Label))
            return base.MeasureOverride(availableSize);

        var label = Label
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToList();

        if (label.Count == 0)
            return base.MeasureOverride(availableSize);

        var expectedWidth = GetExpectedWidth(label);
        var aspectRatio = (double)expectedWidth / 8;

        var width = Math.Min(availableSize.Width, availableSize.Height * aspectRatio);
        var height = width / aspectRatio;

        return new Size(width, height);
    }
    
    protected override Geometry? CreateDefiningGeometry()
    {
        if (string.IsNullOrEmpty(Label))
            return null;

        var label = Label
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToList();

        if (label.Count == 0)
            return null;

        var expectedWidth = GetExpectedWidth(label);
        var expectedHeight = 8;
        
        var unitW = double.IsNaN(Width) ? double.PositiveInfinity : Width / expectedWidth;
        var unitH = double.IsNaN(Height) ? double.PositiveInfinity : Height / expectedHeight;
        var unit = Math.Min(unitW, unitH);
        if (double.IsInfinity(unit) || unit is 0) return null;

        return CreateGeometry(label, unit);
    }

    private int GetExpectedWidth(List<char> label)
    {
        int result = 0;
        result += 8; //Logo width
        result += 1; // left margin
        foreach (var c in label)
        {
            if(GlyphWidthMapping.TryGetValue(c, out var width))         
            {
                result += width; // width of glyph
            }
        }

        result += label.Count - 1; // Spaces between characters
        result += 4; // right margin

        return result;
    }

    private Geometry CreateGeometry(List<char> label, double unit)
    {
        var expectedWidth = GetExpectedWidth(label);

        // === 左边：填充区域（阳）抠出 logo（阴）===
        // Logo 区域实心矩形: (2, 0) → (10, 8)
        var leftFill = new RectangleGeometry(new Rect(0, 0, 9 * unit, 8 * unit));

        // Logo bitmap 6×8，在 8 高区域中垂直居中: offset = (8-6)/2 = 1
        var logoGeo = CreateBitmapGeometry(Irihi_Logo_Bitmap, 1 * unit, 1 * unit, unit, unit);

        // 左边 = 填充 - logo
        var leftPart = new CombinedGeometry(GeometryCombineMode.Exclude, leftFill, logoGeo);

        // === 右边：边框（阳）+ 字母（阳），内部空白（阴）===
        const double textStartX = 9; // 2 (left margin) + 8 (logo)
        var textWidth = expectedWidth - 9; // expectedWidth - textStartX - 2 (right margin)

        // 外边框矩形
        var rightOuter = new RectangleGeometry(new Rect(textStartX * unit, 0, textWidth * unit, 8 * unit));

        // 内部空白矩形（缩进 1 unit）
        var rightInner = new RectangleGeometry(new Rect((textStartX + 1) * unit, 1 * unit, (textWidth - 2) * unit, 7 * unit));

        // 边框 = 外框 - 内框
        var rightBorder = new CombinedGeometry(GeometryCombineMode.Exclude, rightOuter, rightInner);

        // 字母填充（在内部区域中，垂直居中: (6-5)/2 = 0.5）
        var lettersGeo = CreateLettersGeometry(label, (textStartX + 2) * unit, 2 * unit, unit);

        // 右边 = 边框 + 字母
        var rightPart = new CombinedGeometry(GeometryCombineMode.Union, rightBorder, lettersGeo);

        // 整体 = 左边 + 右边
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
                x += (charWidth + 1) * unit; // 字母宽度 + 1 unit 间隔
            }
        }
        return group;
    }
}