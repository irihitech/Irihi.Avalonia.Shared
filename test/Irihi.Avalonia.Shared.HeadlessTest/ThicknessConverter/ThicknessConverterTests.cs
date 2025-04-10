using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest.ThicknessConverter;

public class ThicknessConverterTests
{
    [AvaloniaFact]
    public void ThicknessConverter_WithTopRightBottom_ShouldFilterMargin()
    {
        var view = new ThicknessConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 20, 20, 20), view.button.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithUniformThickness_ShouldApplySelectedEdges()
    {
        var view = new ThicknessConverterView
        {
            button =
            {
                Padding = new Thickness(50)
            }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 50, 50, 50), view.button.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithAsymmetricValues_ShouldFilterCorrectly()
    {
        var view = new ThicknessConverterView
        {
            button =
            {
                Padding = new Thickness(10, 20, 30, 40)
            }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 20, 30, 40), view.button.Margin);
    }
}