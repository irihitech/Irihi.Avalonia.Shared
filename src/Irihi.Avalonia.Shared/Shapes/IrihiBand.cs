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
    // Bitmap → rect decomposition → StreamGeometry
    // ------------------------------------------------------------------------

    private Geometry CreateGeometry(List<char> label, double unit, int expectedWidth)
    {
        var bitmap = BuildFullBitmap(label, expectedWidth);
        var rects = DecomposeToRectangles(bitmap);
        return BuildStreamGeometry(rects, unit);
    }

    private byte[,] BuildFullBitmap(List<char> label, int expectedWidth)
    {
        var bitmap = new byte[BandHeight, expectedWidth];

        // 左边填充（阳）
        for (var x = LeftFillX; x < LeftFillX + LeftFillWidth; x++)
        for (var y = 0; y < BandHeight; y++)
            bitmap[y, x] = 1;

        // Logo 镂空（阴）
        for (var r = 0; r < LogoHeight; r++)
        for (var c = 0; c < LogoWidth; c++)
            if (IrihiLogoBitmap[r, c] == 1)
                bitmap[LogoY + r, LogoX + c] = 0;

        // 右边边框（阳）
        for (var x = TextStartX; x < expectedWidth; x++)
        for (var y = 0; y < BandHeight; y++)
            bitmap[y, x] = 1;

        // 右边内部镂空（阴）
        for (var x = InnerX; x < expectedWidth; x++)
        for (var y = InnerY; y < InnerY + InnerHeight; y++)
            bitmap[y, x] = 0;

        // 字母填充（阳）
        var letterX = LetterX;
        foreach (var c in label)
        {
            if (GlyphMappings.TryGetValue(c, out var letterBitmap))
            {
                var charW = letterBitmap.GetLength(1);
                var charH = letterBitmap.GetLength(0);
                for (var r = 0; r < charH; r++)
                for (var col = 0; col < charW; col++)
                    if (letterBitmap[r, col] == 1)
                        bitmap[LetterY + r, letterX + col] = 1;
                letterX += charW + LetterGap;
            }
        }

        return bitmap;
    }

    private static List<(int left, int top, int width, int height)> DecomposeToRectangles(byte[,] bitmap)
    {
        var height = bitmap.GetLength(0);
        var width = bitmap.GetLength(1);
        var result = new List<(int, int, int, int)>();
        var active = new List<(int left, int right, int top)>();

        for (var y = 0; y < height; y++)
        {
            var runs = new List<(int left, int right)>();
            for (var x = 0; x < width; x++)
            {
                if (bitmap[y, x] == 0) continue;
                var runLeft = x;
                while (x < width && bitmap[y, x] == 1) x++;
                runs.Add((runLeft, x - 1));
                x--;
            }

            var stillActive = new List<(int left, int right, int top)>();

            foreach (var (rLeft, rRight) in runs)
            {
                var merged = false;
                for (var i = 0; i < active.Count; i++)
                {
                    if (active[i].left == rLeft && active[i].right == rRight)
                    {
                        stillActive.Add(active[i]);
                        active.RemoveAt(i);
                        merged = true;
                        break;
                    }
                }

                if (!merged)
                    stillActive.Add((rLeft, rRight, y));
            }

            foreach (var (aLeft, aRight, aTop) in active)
                result.Add((aLeft, aTop, aRight - aLeft + 1, y - aTop));

            active = stillActive;
        }

        foreach (var (aLeft, aRight, aTop) in active)
            result.Add((aLeft, aTop, aRight - aLeft + 1, height - aTop));

        return result;
    }

    private static StreamGeometry BuildStreamGeometry(
        List<(int left, int top, int width, int height)> rects, double unit)
    {
        var geometry = new StreamGeometry();
        using var ctx = geometry.Open();

        foreach (var (left, top, w, h) in rects)
        {
            var x = left * unit;
            var y = top * unit;
            var rw = w * unit;
            var rh = h * unit;

            ctx.BeginFigure(new Point(x, y), isFilled: true);
            ctx.LineTo(new Point(x + rw, y));
            ctx.LineTo(new Point(x + rw, y + rh));
            ctx.LineTo(new Point(x, y + rh));
            ctx.EndFigure(isClosed: true);
        }

        return geometry;
    }
}
