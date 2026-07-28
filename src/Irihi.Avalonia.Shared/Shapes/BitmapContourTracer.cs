using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;

namespace Irihi.Avalonia.Shared.Shapes;

/// <summary>
/// Extracts closed contours from a binary bitmap and builds a StreamGeometry,
/// suitable for rendering QR codes, pixel-art, or any grid-based filled shapes.
/// </summary>
public static class BitmapContourTracer
{
    // ---- Public API ----

    /// <summary>
    /// Extract directed edges where pixel values differ.
    /// Direction rules (clockwise around filled=1 regions):
    ///   top=0,bot=1 → ←    top=1,bot=0 → →
    ///   left=0,right=1 → ↓  left=1,right=0 → ↑
    /// </summary>
    public static List<(int fromR, int fromC, int toR, int toC)> ExtractDirectedEdges(byte[,] bitmap)
    {
        var h = bitmap.GetLength(0);
        var w = bitmap.GetLength(1);
        var edges = new List<(int, int, int, int)>();

        for (var r = 1; r < h; r++)
        for (var c = 0; c < w; c++)
        {
            var top = bitmap[r - 1, c];
            var bot = bitmap[r, c];
            if (top == 0 && bot == 1) edges.Add((r, c + 1, r, c));
            else if (top == 1 && bot == 0) edges.Add((r, c, r, c + 1));
        }

        for (var c = 1; c < w; c++)
        for (var r = 0; r < h; r++)
        {
            var left = bitmap[r, c - 1];
            var right = bitmap[r, c];
            if (left == 0 && right == 1) edges.Add((r, c, r + 1, c));
            else if (left == 1 && right == 0) edges.Add((r + 1, c, r, c));
        }

        return edges;
    }

    /// <summary>
    /// Trace directed edges into closed contours. Edges are consumed as they
    /// are traced. At every junction, prefer the right-most turn.
    /// </summary>
    public static List<List<(int r, int c)>> TraceContours(
        List<(int fromR, int fromC, int toR, int toC)> edges)
    {
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
            var startKey = outgoing.Keys.First();
            var startEdges = outgoing[startKey];
            var (sr, sc) = startKey;

            var best = PickBest(startEdges, sr, sc, sr, sc);
            if (!best.found) break;
            startEdges.RemoveAt(best.idx);
            if (startEdges.Count == 0) outgoing.Remove(startKey);

            var contour = new List<(int, int)> { (sr, sc) };
            var cr = best.r;
            var cc = best.c;
            var pr = sr;
            var pc = sc;

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

                pr = cr;
                pc = cc;
                cr = best.r;
                cc = best.c;
            }

            if (contour.Count >= 3)
                contours.Add(contour);
        }

        return contours;
    }

    /// <summary>
    /// Remove intermediate collinear points from a closed contour so that only
    /// actual corners remain (and only those get rounded later).
    /// </summary>
    public static List<(int r, int c)> SimplifyContour(List<(int r, int c)> contour)
    {
        var n = contour.Count;
        if (n < 3) return contour;

        var result = new List<(int, int)>(n) { contour[0] };
        for (var i = 1; i < n; i++)
        {
            result.Add(contour[i]);
            while (result.Count >= 3)
            {
                var m = result.Count;
                if (Collinear(result[m - 3], result[m - 2], result[m - 1]))
                    result.RemoveAt(m - 2);
                else
                    break;
            }
        }

        while (result.Count >= 3)
        {
            var m = result.Count;
            if (Collinear(result[m - 2], result[m - 1], result[0]))
                result.RemoveAt(m - 1);
            else if (Collinear(result[m - 1], result[0], result[1]))
                result.RemoveAt(0);
            else
                break;
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool Collinear((int r, int c) a, (int r, int c) b, (int r, int c) c) =>
        (b.r - a.r) * (c.c - b.c) == (b.c - a.c) * (c.r - b.r);

    /// <summary>
    /// Build a StreamGeometry from contour point-lists.
    /// </summary>
    /// <param name="contours">Closed contours in logical pixel-grid coordinates.</param>
    /// <param name="unit">Scale factor (logical unit → pixels).</param>
    /// <param name="cornerRatio">0 = sharp corners, e.g. 0.25 = quarter-unit radius.</param>
    /// <param name="margin">How many pixels of zero-margin were added around the bitmap.</param>
    public static StreamGeometry BuildGeometry(
        List<List<(int r, int c)>> contours, double unit,
        double cornerRatio = 0, int margin = 1)
    {
        var geometry = new StreamGeometry();
        using var ctx = geometry.Open();
        var radius = unit * cornerRatio;

        foreach (var raw in contours)
        {
            var contour = SimplifyContour(raw);
            var n = contour.Count;
            if (n < 3 || radius <= 0)
            {
                if (n == 0) continue;
                var first = contour[0];
                ctx.BeginFigure(ToPoint(first, unit, margin), isFilled: true);
                for (var i = 1; i < n; i++)
                    ctx.LineTo(ToPoint(contour[i], unit, margin));
                ctx.EndFigure(isClosed: true);
                continue;
            }

            var pts = contour.Select(p => ToPoint(p, unit, margin)).ToArray();

            var corners = new (Point cornerIn, Point cornerOut, SweepDirection sweep)[n];
            for (var i = 0; i < n; i++)
            {
                var prev = pts[(i - 1 + n) % n];
                var curr = pts[i];
                var next = pts[(i + 1) % n];

                var vIn = Normalize(prev - curr);
                var vOut = Normalize(next - curr);

                var cross = vIn.X * vOut.Y - vIn.Y * vOut.X;
                corners[i] = (
                    curr + vIn * radius,
                    curr + vOut * radius,
                    cross > 0 ? SweepDirection.CounterClockwise : SweepDirection.Clockwise
                );
            }

            var arcSize = new Size(radius, radius);

            ctx.BeginFigure(corners[0].cornerOut, isFilled: true);
            for (var i = 1; i < n; i++)
            {
                ctx.LineTo(corners[i].cornerIn);
                ctx.ArcTo(corners[i].cornerOut, arcSize, 0, false, corners[i].sweep);
            }

            ctx.LineTo(corners[0].cornerIn);
            ctx.ArcTo(corners[0].cornerOut, arcSize, 0, false, corners[0].sweep);
            ctx.EndFigure(isClosed: true);
        }

        return geometry;
    }

    // ---- Private helpers ----

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
            if ((nr, nc) == (pr, pc)) continue;

            var nd = (dr: nr - cr, dc: nc - cc);
            var score = nd == (dir.dc, -dir.dr) ? 2 // right
                : nd == dir ? 1 // straight
                : 0; // left

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Point ToPoint((int r, int c) p, double unit, int margin) =>
        new((p.c - margin) * unit, (p.r - margin) * unit);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Point Normalize(Point p)
    {
        var len = Math.Sqrt(p.X * p.X + p.Y * p.Y);
        return len > 0 ? new Point(p.X / len, p.Y / len) : p;
    }
}