using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class ThicknessMixerConverterTests
{
    [AvaloniaFact]
    public void ThicknessConverter_WithAll_ShouldKeepOriginalThickness()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(20), view.button1.Margin);
    }

    [AvaloniaTheory]
    [InlineData(50, 50, 50, 50, 0, 50, 50, 50)]
    [InlineData(1, 2, 3, 4, 0, 2, 3, 4)]
    public void ThicknessConverter_WithVerticalRight_ShouldFilterEdges(
        double l, double t, double r, double b,
        double el, double et, double er, double eb)
    {
        var view = new ThicknessMixerConverterView
        {
            button2 = { Padding = new Thickness(l, t, r, b) }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(el, et, er, eb), view.button2.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithHorizontal_ShouldKeepLeftRight()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(20, 0, 20, 0), view.button3.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithTopLeft_ShouldKeepTopLeft()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(20, 20, 0, 0), view.button4.Margin);
    }

    [AvaloniaTheory]
    [InlineData(20)]
    [InlineData(100)]
    public void ThicknessConverter_WithNone_ShouldClearAllEdges(double padding)
    {
        var view = new ThicknessMixerConverterView
        {
            button5 = { Padding = new Thickness(padding) }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0), view.button5.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithBottom_ShouldKeepOnlyBottom()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(0, 0, 0, 20), view.button6.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithVerticalAndBottomLeft_ShouldCombineFlags()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(20, 20, 0, 20), view.button7.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithScale2_ShouldDoubleAllEdges()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new Thickness(40), view.button8.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithHorizontalAndScaleHalf_ShouldHalveLeftRight()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new Thickness(10, 0, 10, 0), view.button9.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_WithTopLeftAndScaleMinus1_ShouldNegateTopLeft()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new Thickness(-20, -20, 0, 0), view.button10.Margin);
    }

    [AvaloniaFact]
    public void ThicknessConverter_ShouldReactToRuntimeChanges()
    {
        var view = new ThicknessMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new Thickness(20, 0), view.button3.Margin);

        view.button3.Padding = new Thickness(50);
        Assert.Equal(new Thickness(50, 0), view.button3.Margin);
    }
}