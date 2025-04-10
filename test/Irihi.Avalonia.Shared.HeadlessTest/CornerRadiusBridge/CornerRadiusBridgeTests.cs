using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class CornerRadiusBridgeTests
{
    [AvaloniaFact]
    public void CornerRadiusBridge_WithPositionalParameters_ShouldCreateCorrectRadius()
    {
        var view = new CornerRadiusBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(0, 0, 10, 0), view.border1.CornerRadius);
        Assert.Equal(new CornerRadius(10), view.border2.CornerRadius);
        Assert.Equal(new CornerRadius(10, 20, 30, 40), view.border3.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusBridge_WithResourceBinding_ShouldResolveCorrectly()
    {
        var view = new CornerRadiusBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(0, 0, 10, 0), view.border4.CornerRadius);
        Assert.Equal(new CornerRadius(10), view.border5.CornerRadius);
        Assert.Equal(new CornerRadius(10, 20, 30, 40), view.border6.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusBridge_WithNamedParameters_ShouldOverrideCorrectValues()
    {
        var view = new CornerRadiusBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 10, 50, 10), view.border7.CornerRadius);
        Assert.Equal(new CornerRadius(10, 20, 50, 40), view.border8.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusBridge_WithMixedParameters_ShouldPrioritizeCorrectly()
    {
        var view = new CornerRadiusBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 10, 50, 10), view.border9.CornerRadius);
        Assert.Equal(new CornerRadius(10, 20, 50, 40), view.border10.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusBridge_WithPropertyElementSyntax_ShouldWorkSameAsAttribute()
    {
        var view = new CornerRadiusBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(view.border6.CornerRadius, view.border3.CornerRadius);
    }
}