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

    protected override Geometry? CreateDefiningGeometry()
    {
        var ratio = Math.Min(Bounds.Width / 80, Bounds.Height / 60);
        if (ratio is 0) return null;
        return Corner == 0 ? CreateGeometry(ratio) : CreateComplexGeometry(ratio);
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

    private StreamGeometry CreateGeometry(double unit = 1)
    {
        var unit10 = unit * 10;
        var unit20 = unit * 20;
        var unit30 = unit * 30;
        var unit40 = unit * 40;
        var unit50 = unit * 50;
        var unit60 = unit * 60;
        var unit70 = unit * 70;
        var unit80 = unit * 80;
        var geometry = new StreamGeometry();
        using var geoContext = geometry.Open();
        geoContext.BeginFigure(new Point(0, 0), true);
        geoContext.LineTo(new Point(unit10, 0));
        geoContext.LineTo(new Point(unit10, unit10));
        geoContext.LineTo(new Point(0, unit10));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit20, 0), true);
        geoContext.LineTo(new Point(unit60, 0));
        geoContext.LineTo(new Point(unit60, unit10));
        geoContext.LineTo(new Point(unit20, unit10));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit70, 0), true);
        geoContext.LineTo(new Point(unit80, 0));
        geoContext.LineTo(new Point(unit80, unit10));
        geoContext.LineTo(new Point(unit70, unit10));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(0, unit20), true);
        geoContext.LineTo(new Point(unit10, unit20));
        geoContext.LineTo(new Point(unit10, unit60));
        geoContext.LineTo(new Point(0, unit60));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit20, unit20), true);
        geoContext.LineTo(new Point(unit60, unit20));
        geoContext.LineTo(new Point(unit60, unit30));
        geoContext.LineTo(new Point(unit40, unit30));
        geoContext.LineTo(new Point(unit40, unit40));
        geoContext.LineTo(new Point(unit50, unit40));
        geoContext.LineTo(new Point(unit50, unit50));
        geoContext.LineTo(new Point(unit60, unit50));
        geoContext.LineTo(new Point(unit60, unit60));
        geoContext.LineTo(new Point(unit50, unit60));
        geoContext.LineTo(new Point(unit50, unit50));
        geoContext.LineTo(new Point(unit40, unit50));
        geoContext.LineTo(new Point(unit40, unit40));
        geoContext.LineTo(new Point(unit30, unit40));
        geoContext.LineTo(new Point(unit30, unit60));
        geoContext.LineTo(new Point(unit20, unit60));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit70, unit20), true);
        geoContext.LineTo(new Point(unit80, unit20));
        geoContext.LineTo(new Point(unit80, unit60));
        geoContext.LineTo(new Point(unit70, unit60));
        geoContext.EndFigure(true);

        return geometry;
    }

    private StreamGeometry CreateComplexGeometry(double unit = 1)
    {
        var unit10 = unit * 10;
        var unit20 = unit * 20;
        var unit30 = unit * 30;
        var unit40 = unit * 40;
        var unit50 = unit * 50;
        var unit60 = unit * 60;
        var unit70 = unit * 70;
        var unit80 = unit * 80;
        var corner = Math.Min(unit10 / 2, Corner);
        var geometry = new StreamGeometry();
        using var geoContext = geometry.Open();
        geoContext.BeginFigure(new Point(corner, 0), true);
        geoContext.LineTo(new Point(unit10 - corner, 0));
        geoContext.ArcTo(new Point(unit10, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit10, unit10 - corner));
        geoContext.ArcTo(new Point(unit10 - corner, unit10), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(corner, unit10));
        geoContext.ArcTo(new Point(0, unit10 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(0, corner));
        geoContext.ArcTo(new Point(corner, 0), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit20 + corner, 0), true);
        geoContext.LineTo(new Point(unit60 - corner, 0));
        geoContext.ArcTo(new Point(unit60, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit60, unit10 - corner));
        geoContext.ArcTo(new Point(unit60 - corner, unit10), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit20 + corner, unit10));
        geoContext.ArcTo(new Point(unit20, unit10 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit20, corner));
        geoContext.ArcTo(new Point(unit20 + corner, 0), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit70 + corner, 0), true);
        geoContext.LineTo(new Point(unit80 - corner, 0));
        geoContext.ArcTo(new Point(unit80, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit80, unit10 - corner));
        geoContext.ArcTo(new Point(unit80 - corner, unit10), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit70 + corner, unit10));
        geoContext.ArcTo(new Point(unit70, unit10 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit70, corner));
        geoContext.ArcTo(new Point(unit70 + corner, 0), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(corner, unit20), true);
        geoContext.LineTo(new Point(unit10 - corner, unit20));
        geoContext.ArcTo(new Point(unit10, unit20 + corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit10, unit60 - corner));
        geoContext.ArcTo(new Point(unit10 - corner, unit60), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(corner, unit60));
        geoContext.ArcTo(new Point(0, unit60 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(0, unit20 + corner));
        geoContext.ArcTo(new Point(corner, unit20), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit20 + corner, unit20), true);
        geoContext.LineTo(new Point(unit60 - corner, unit20));
        geoContext.ArcTo(new Point(unit60, unit20 + corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit60, unit30 - corner));
        geoContext.ArcTo(new Point(unit60 - corner, unit30), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit40 + corner, unit30));
        geoContext.ArcTo(new Point(unit40, unit30 + corner), new Size(corner, corner), 90, false,
            SweepDirection.CounterClockwise);
        geoContext.LineTo(new Point(unit40, unit40 - corner));
        geoContext.ArcTo(new Point(unit40 - corner, unit40), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit30 + corner, unit40));
        geoContext.ArcTo(new Point(unit30, unit40 + corner), new Size(corner, corner), 90, false,
            SweepDirection.CounterClockwise);
        geoContext.LineTo(new Point(unit30, unit60 - corner));
        geoContext.ArcTo(new Point(unit30 - corner, unit60), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit20 + corner, unit60));
        geoContext.ArcTo(new Point(unit20, unit60 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit20, unit20 + corner));
        geoContext.ArcTo(new Point(unit20 + corner, unit20), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit40 + corner, unit40), true);
        geoContext.LineTo(new Point(unit50 - corner, unit40));
        geoContext.ArcTo(new Point(unit50, unit40 + corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit50, unit50 - corner));
        geoContext.ArcTo(new Point(unit50 - corner, unit50), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit40 + corner, unit50));
        geoContext.ArcTo(new Point(unit40, unit50 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit40, unit40 + corner));
        geoContext.ArcTo(new Point(unit40 + corner, unit40), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit50 + corner, unit50), true);
        geoContext.LineTo(new Point(unit60 - corner, unit50));
        geoContext.ArcTo(new Point(unit60, unit50 + corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit60, unit60 - corner));
        geoContext.ArcTo(new Point(unit60 - corner, unit60), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit50 + corner, unit60));
        geoContext.ArcTo(new Point(unit50, unit60 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit50, unit50 + corner));
        geoContext.ArcTo(new Point(unit50 + corner, unit50), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(unit70 + corner, unit20), true);
        geoContext.LineTo(new Point(unit80 - corner, unit20));
        geoContext.ArcTo(new Point(unit80, unit20 + corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit80, unit60 - corner));
        geoContext.ArcTo(new Point(unit80 - corner, unit60), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit70 + corner, unit60));
        geoContext.ArcTo(new Point(unit70, unit60 - corner), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.LineTo(new Point(unit70, unit20 + corner));
        geoContext.ArcTo(new Point(unit70 + corner, unit20), new Size(corner, corner), 90, false,
            SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        return geometry;
    }
}