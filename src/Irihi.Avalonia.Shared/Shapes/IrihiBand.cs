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
            var edges = ExtractDirectedEdges(bitmap);
            _cachedContours = TraceContours(edges);
            _cachedExpectedWidth = expectedWidth;
        }

        return BuildStreamGeometry(_cachedContours, unit);
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

    /// <summary>
    /// Extract directed edges where pixel values differ.
    /// 
    /// Direction rules (clockwise around filled=1 regions):
    ///   top=0,bot=1 → ←    top=1,bot=0 → →
    ///   left=0,right=1 → ↓  left=1,right=0 → ↑
    /// </summary>
    private static List<(int fromR, int fromC, int toR, int toC)> ExtractDirectedEdges(byte[,] bitmap)
    {
        var h = bitmap.GetLength(0);
        var w = bitmap.GetLength(1);
        var edges = new List<(int, int, int, int)>();

        for (var r = 1; r < h; r++)
        for (var c = 0; c < w; c++)
        {
            var top = bitmap[r - 1, c];
            var bot = bitmap[r, c];
            if (top == 0 && bot == 1)      edges.Add((r, c + 1, r, c));     // ←
            else if (top == 1 && bot == 0) edges.Add((r, c, r, c + 1));     // →
        }

        for (var c = 1; c < w; c++)
        for (var r = 0; r < h; r++)
        {
            var left  = bitmap[r, c - 1];
            var right = bitmap[r, c];
            if (left == 0 && right == 1)      edges.Add((r, c, r + 1, c));   // ↓
            else if (left == 1 && right == 0) edges.Add((r + 1, c, r, c));   // ↑
        }

        return edges;
    }

    /// <summary>
    /// Trace directed edges into closed contours. Edges are consumed (removed)
    /// as they are traced — no separate visited set needed. At every junction,
    /// prefer the left-most turn.
    /// </summary>
    private static List<List<(int r, int c)>> TraceContours(
        List<(int fromR, int fromC, int toR, int toC)> edges)
    {
        // point → outgoing edges (destinations)
        var outgoing = new Dictionary<(int, int), List<(int toR, int toC)>>();
        foreach (var (fr, fc, tr, tc) in edges)
        {
            var key = (fr, fc);
            if (!outgoing.ContainsKey(key))
                outgoing[key] = new List<(int, int)>();
            outgoing[key].Add((tr, tc));
        }

        var contours = new List<List<(int r, int c)>>();

        while (outgoing.Count > 0)
        {
            // Pick any remaining start point + its first outgoing edge
            var startKey = outgoing.Keys.First();
            var startEdges = outgoing[startKey];
            var (sr, sc) = startKey;

            var best = PickBest(startEdges, sr, sc, sr, sc);
            if (!best.found) break;
            startEdges.RemoveAt(best.idx);
            if (startEdges.Count == 0) outgoing.Remove(startKey);

            var contour = new List<(int, int)> { (sr, sc) };
            var cr = best.r; var cc = best.c;
            var pr = sr; var pc = sc;

            while ((cr, cc) != (sr, sc))
            {
                contour.Add((cr, cc));
                var key = (cr, cc);
                if (!outgoing.TryGetValue(key, out var candidates) || candidates.Count == 0)
                    break;

                best = PickBest(candidates, cr, cc, pr, pc);
                if (!best.found) break;
                candidates.RemoveAt(best.idx);
                if (candidates.Count == 0) outgoing.Remove(key);

                pr = cr; pc = cc;
                cr = best.r; cc = best.c;
            }

            if (contour.Count >= 3)
                contours.Add(contour);
        }

        return contours;
    }

    /// <summary>
    /// From a list of candidate destination points, pick the one that
    /// turns left-most relative to the incoming direction (prev → cur).
    /// Returns (found, idx, r, c). Skips U-turns.
    /// </summary>
    private static (bool found, int idx, int r, int c) PickBest(
        List<(int toR, int toC)> candidates, int cr, int cc, int pr, int pc)
    {
        var dir = (dr: cr - pr, dc: cc - pc);
        var bestIdx = -1;
        var bestScore = int.MinValue;
        var bestR = 0;
        var bestC = 0;

        for (var i = 0; i < candidates.Count; i++)
        {
            var (nr, nc) = candidates[i];
            if ((nr, nc) == (pr, pc)) continue; // skip U-turn

            var nd = (dr: nr - cr, dc: nc - cc);
            var score = nd == (-dir.dc, dir.dr) ? 2      // left
                      : nd == dir ? 1                      // straight
                      : 0;                                 // right

            if (score > bestScore)
            {
                bestScore = score;
                bestIdx = i;
                bestR = nr;
                bestC = nc;
            }
        }

        return bestIdx >= 0 ? (true, bestIdx, bestR, bestC) : (false, 0, 0, 0);
    }

    private static StreamGeometry BuildStreamGeometry(
        List<List<(int r, int c)>> contours, double unit)
    {
        var geometry = new StreamGeometry();
        using var ctx = geometry.Open();

        foreach (var contour in contours)
        {
            if (contour.Count == 0) continue;

            var first = contour[0];
            ctx.BeginFigure(
                new Point((first.c - BitmapMargin) * unit, (first.r - BitmapMargin) * unit),
                isFilled: true);

            for (var i = 1; i < contour.Count; i++)
                ctx.LineTo(
                    new Point((contour[i].c - BitmapMargin) * unit, (contour[i].r - BitmapMargin) * unit));

            ctx.EndFigure(isClosed: true);
        }

        return geometry;
    }
}
