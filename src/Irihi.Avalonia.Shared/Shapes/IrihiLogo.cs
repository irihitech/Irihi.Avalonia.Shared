using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Irihi.Avalonia.Shared.Shapes;

public class IrihiLogo : Shape
{
    public static readonly StyledProperty<double> CornerProperty = AvaloniaProperty.Register<IrihiLogo, double>(
        nameof(Corner));

    static IrihiLogo()
    {
        WidthProperty.OverrideDefaultValue<IrihiLogo>(40);
        AffectsGeometry<IrihiLogo>(WidthProperty, CornerProperty, BoundsProperty);
    }

    public double Corner
    {
        get => GetValue(CornerProperty);
        set => SetValue(CornerProperty, value);
    }

    // ---- cached contours ----

    private static readonly List<List<(int r, int c)>> CachedContours = BuildLogoContours();

    private static List<List<(int r, int c)>> BuildLogoContours()
    {
        var src = IrihiBand.IrihiLogoBitmap;
        var rows = src.GetLength(0);
        var cols = src.GetLength(1);

        // add 1-pixel zero margin
        var bitmap = new byte[rows + 2, cols + 2];
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < cols; c++)
            bitmap[r + 1, c + 1] = src[r, c];

        var edges = BitmapContourTracer.ExtractDirectedEdges(bitmap);
        return BitmapContourTracer.TraceContours(edges);
    }

    // ---- Shape overrides ----

    protected override Geometry? CreateDefiningGeometry()
    {
        var ratio = Math.Min(Bounds.Width / 8, Bounds.Height / 6);
        if (ratio is 0) return null;

        var cornerRatio = Corner > 0 ? Corner / ratio : 0;
        return BitmapContourTracer.BuildGeometry(CachedContours, ratio, cornerRatio);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        base.MeasureOverride(availableSize);
        var height = availableSize.Width * 0.75;
        return new Size(availableSize.Width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        return DesiredSize;
    }
}
