using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class ThicknessBridgeTests
{
    [AvaloniaFact]
    public void ThicknessBridge_WithPositionalParameters_ShouldCreateCorrectThickness()
    {
        var view = new ThicknessBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 0, 10, 0), view.border1.Padding);
        Assert.Equal(new Thickness(10), view.border2.Padding);
        Assert.Equal(new Thickness(10, 20, 10, 20), view.border3.Padding);
        Assert.Equal(new Thickness(10, 20, 30, 40), view.border4.Padding);
    }

    [AvaloniaFact]
    public void ThicknessBridge_WithResourceBinding_ShouldResolveCorrectly()
    {
        var view = new ThicknessBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 0, 10, 0), view.border5.Padding);
        Assert.Equal(new Thickness(10), view.border6.Padding);
        Assert.Equal(new Thickness(10, 20, 10, 20), view.border7.Padding);
        Assert.Equal(new Thickness(10, 20, 30, 40), view.border8.Padding);
    }

    [AvaloniaFact]
    public void ThicknessBridge_WithNamedParameters_ShouldOverrideCorrectValues()
    {
        var view = new ThicknessBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(10, 10, 50, 10), view.border9.Padding);
        Assert.Equal(new Thickness(10, 20, 50, 20), view.border10.Padding);
        Assert.Equal(new Thickness(10, 20, 50, 40), view.border11.Padding);
    }

    [AvaloniaFact]
    public void ThicknessBridge_WithMixedParameters_ShouldPrioritizeCorrectly()
    {
        var view = new ThicknessBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(10, 10, 50, 10), view.border12.Padding);
        Assert.Equal(new Thickness(10, 20, 50, 20), view.border13.Padding);
        Assert.Equal(new Thickness(10, 20, 50, 40), view.border14.Padding);
    }

    [AvaloniaFact]
    public void ThicknessBridge_WithPropertyElementSyntax_ShouldWorkSameAsAttribute()
    {
        var view = new ThicknessBridgeView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(view.border6.Padding, view.border2.Padding);
        Assert.Equal(view.border7.Padding, view.border3.Padding);
    }
}