using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Irihi.Avalonia.Shared.Shapes;

public class IrihiLogo: Control
{
    public static readonly StyledProperty<IBrush?> FillProperty = Shape.FillProperty.AddOwner<IrihiLogo>();

    public IBrush? Fill
    {
        get => GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }

    public static readonly StyledProperty<double> CornerProperty = AvaloniaProperty.Register<IrihiLogo, double>(
        nameof(Corner));

    public double Corner
    {
        get => GetValue(CornerProperty);
        set => SetValue(CornerProperty, value);
    }

    static IrihiLogo()
    {
        WidthProperty.OverrideDefaultValue<IrihiLogo>(40);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var size = base.MeasureOverride(availableSize);
        var height = availableSize.Width * 0.75;
        return new Size(availableSize.Width, height);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var ratio = Math.Min(Bounds.Width / 80, Bounds.Height / 60);
        StreamGeometry geometry = Corner == 0 ? CreateGeometry(ratio) : CreateComplexGeometry(ratio);
        context.DrawGeometry(Fill, null, geometry);
    }

    private StreamGeometry CreateGeometry(double unit = 1)
    {
        StreamGeometry geometry = new StreamGeometry();
        using var geoContext = geometry.Open();
        geoContext.BeginFigure(new Point(0, 0), true);
        geoContext.LineTo(new Point(10*unit, 0));
        geoContext.LineTo(new Point(10*unit, 10*unit));
        geoContext.LineTo(new Point(0, 10*unit));
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(20*unit, 0), true);
        geoContext.LineTo(new Point(60*unit, 0));
        geoContext.LineTo(new Point(60*unit, 10*unit));
        geoContext.LineTo(new Point(20*unit, 10*unit));
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(70*unit, 0), true);
        geoContext.LineTo(new Point(80*unit, 0));
        geoContext.LineTo(new Point(80*unit, 10*unit));
        geoContext.LineTo(new Point(70*unit, 10*unit));
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(0, 20*unit), true);
        geoContext.LineTo(new Point(10*unit, 20*unit));
        geoContext.LineTo(new Point(10*unit, 60*unit));
        geoContext.LineTo(new Point(0, 60*unit));
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(20*unit, 20*unit), true);
        geoContext.LineTo(new Point(60*unit, 20*unit));
        geoContext.LineTo(new Point(60*unit, 30*unit));
        geoContext.LineTo(new Point(40*unit, 30*unit));
        geoContext.LineTo(new Point(40*unit, 40*unit));
        geoContext.LineTo(new Point(50*unit, 40*unit));
        geoContext.LineTo(new Point(50*unit, 50*unit));
        geoContext.LineTo(new Point(60*unit, 50*unit));
        geoContext.LineTo(new Point(60*unit, 60*unit));
        geoContext.LineTo(new Point(50*unit, 60*unit));
        geoContext.LineTo(new Point(50*unit, 50*unit));
        geoContext.LineTo(new Point(40*unit, 50*unit));
        geoContext.LineTo(new Point(40*unit, 40*unit));
        geoContext.LineTo(new Point(30*unit, 40*unit));
        geoContext.LineTo(new Point(30*unit, 60*unit));
        geoContext.LineTo(new Point(20*unit, 60*unit));
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(70*unit, 20*unit), true);
        geoContext.LineTo(new Point(80*unit, 20*unit));
        geoContext.LineTo(new Point(80*unit, 60*unit));
        geoContext.LineTo(new Point(70*unit, 60*unit));
        geoContext.EndFigure(true);

        return geometry;
    }

    private StreamGeometry CreateComplexGeometry(double unit = 1)
    {
        double corner = Math.Min(10 * unit / 2, Corner);
        StreamGeometry geometry = new StreamGeometry();
        using var geoContext = geometry.Open();
        geoContext.BeginFigure(new Point(corner, 0), true);
        geoContext.LineTo(new Point(10*unit-corner, 0));
        geoContext.ArcTo(new Point(10*unit, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(10*unit, 10*unit-corner));
        geoContext.ArcTo(new Point(10*unit-corner, 10*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(corner, 10*unit));
        geoContext.ArcTo(new Point(0, 10*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(0, corner));
        geoContext.ArcTo(new Point(corner, 0), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(20*unit + corner, 0), true);
        geoContext.LineTo(new Point(60*unit-corner, 0));
        geoContext.ArcTo(new Point(60*unit, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(60*unit, 10*unit-corner));
        geoContext.ArcTo(new Point(60*unit-corner, 10*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(20*unit+corner, 10*unit));
        geoContext.ArcTo(new Point(20*unit, 10*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(20*unit, corner));
        geoContext.ArcTo(new Point(20*unit+corner, 0), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(70*unit+corner, 0), true);
        geoContext.LineTo(new Point(80*unit-corner, 0));
        geoContext.ArcTo(new Point(80*unit, corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(80*unit, 10*unit-corner));
        geoContext.ArcTo(new Point(80*unit-corner, 10*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(70*unit+corner, 10*unit));
        geoContext.ArcTo(new Point(70*unit, 10*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(70*unit, corner));
        geoContext.ArcTo(new Point(70*unit+corner, 0), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(corner, 20*unit), true);
        geoContext.LineTo(new Point(10*unit-corner, 20*unit));
        geoContext.ArcTo(new Point(10*unit, 20*unit+corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(10*unit, 60*unit-corner));
        geoContext.ArcTo(new Point(10*unit-corner, 60*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(corner, 60*unit));
        geoContext.ArcTo(new Point(0, 60*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(0, 20*unit+corner));
        geoContext.ArcTo(new Point(corner, 20*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);
            
        geoContext.BeginFigure(new Point(20*unit+corner, 20*unit), true);
        geoContext.LineTo(new Point(60*unit-corner, 20*unit));
        geoContext.ArcTo(new Point(60*unit, 20*unit+corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(60*unit, 30*unit-corner));
        geoContext.ArcTo(new Point(60*unit-corner, 30*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(40*unit+corner, 30*unit));
        geoContext.ArcTo(new Point(40*unit, 30*unit+corner), new Size(corner, corner), 90, false, SweepDirection.CounterClockwise);
        geoContext.LineTo(new Point(40*unit, 40*unit-corner));
        geoContext.ArcTo(new Point(40*unit-corner, 40*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(30*unit+corner, 40*unit));
        geoContext.ArcTo(new Point(30*unit, 40*unit+corner), new Size(corner, corner), 90, false, SweepDirection.CounterClockwise);
        geoContext.LineTo(new Point(30*unit, 60*unit-corner));
        geoContext.ArcTo(new Point(30*unit-corner, 60*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(20*unit+corner, 60*unit));
        geoContext.ArcTo(new Point(20*unit, 60*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(20*unit, 20*unit+corner));
        geoContext.ArcTo(new Point(20*unit+corner, 20*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);
        
        geoContext.BeginFigure(new Point(40*unit+corner, 40*unit), true);
        geoContext.LineTo(new Point(50*unit-corner, 40*unit));
        geoContext.ArcTo(new Point(50*unit, 40*unit+corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(50*unit, 50*unit-corner));
        geoContext.ArcTo(new Point(50*unit-corner, 50*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(40*unit+corner, 50*unit));
        geoContext.ArcTo(new Point(40*unit, 50*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(40*unit, 40*unit+corner));
        geoContext.ArcTo(new Point(40*unit+corner, 40*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(50 * unit + corner, 50 * unit), true);
        geoContext.LineTo(new Point(60 * unit - corner, 50 * unit));
        geoContext.ArcTo(new Point(60 * unit, 50 * unit + corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(60 * unit, 60 * unit - corner));
        geoContext.ArcTo(new Point(60 * unit - corner, 60 * unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(50 * unit + corner, 60 * unit));
        geoContext.ArcTo(new Point(50 * unit, 60 * unit - corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(50 * unit, 50 * unit + corner));
        geoContext.ArcTo(new Point(50 * unit + corner, 50 * unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        geoContext.BeginFigure(new Point(70*unit+corner, 20*unit), true);
        geoContext.LineTo(new Point(80*unit-corner, 20*unit));
        geoContext.ArcTo(new Point(80*unit, 20*unit+corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(80*unit, 60*unit-corner));
        geoContext.ArcTo(new Point(80*unit-corner, 60*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(70*unit+corner, 60*unit));
        geoContext.ArcTo(new Point(70*unit, 60*unit-corner), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.LineTo(new Point(70*unit, 20*unit+corner));
        geoContext.ArcTo(new Point(70*unit+corner, 20*unit), new Size(corner, corner), 90, false, SweepDirection.Clockwise);
        geoContext.EndFigure(true);

        return geometry;
    }
}