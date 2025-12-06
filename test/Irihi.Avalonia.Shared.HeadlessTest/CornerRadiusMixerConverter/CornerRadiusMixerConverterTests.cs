using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class CornerRadiusMixerConverterTests
{
    [AvaloniaFact]
    public void CornerRadiusConverter_Default_ShouldApplyTopLeftBottomRight()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10), view.button1.CornerRadius);
    }

    [AvaloniaTheory]
    [InlineData(50, 50, 50, 50, 0, 50, 50, 50)]
    [InlineData(1, 2, 3, 4, 0, 2, 3, 4)]
    public void CornerRadiusConverter_WithBottomTopRight_ShouldFilterCorrectly(
        double tl, double tr, double br, double bl,
        double etl, double etr, double ebr, double ebl)
    {
        var view = new CornerRadiusMixerConverterView
        {
            border = { CornerRadius = new CornerRadius(tl, tr, br, bl) }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(etl, etr, ebr, ebl), view.button2.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithTopLeftBottomRight_ShouldKeepDiagonalCorners()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 0, 10, 0), view.button3.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithTop_ShouldKeepTopCorners()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 10, 0, 0), view.button4.CornerRadius);
    }

    [AvaloniaTheory]
    [InlineData(20)]
    [InlineData(100)]
    public void CornerRadiusConverter_WithNone_ShouldClearAllCorners(double value)
    {
        var view = new CornerRadiusMixerConverterView
        {
            border = { CornerRadius = new CornerRadius(value) }
        };
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(0), view.button5.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithBottomLeft_ShouldIsolateSingleCorner()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(0, 0, 0, 10), view.button6.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithTopAndLeft_ShouldCombineFlags()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 10, 0, 10), view.button7.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithScale2_ShouldDoubleAllCorners()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new CornerRadius(20, 20, 20, 20), view.button8.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithTopAndScaleHalf_ShouldHalveTopCorners()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new CornerRadius(5, 5, 0, 0), view.button9.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_WithBottomLeftAndScaleMinus1_ShouldNegateBottomLeft()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();
        Assert.Equal(new CornerRadius(0, 0, 0, -10), view.button10.CornerRadius);
    }

    [AvaloniaFact]
    public void CornerRadiusConverter_ShouldReactToRuntimeChanges()
    {
        var view = new CornerRadiusMixerConverterView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(new CornerRadius(10, 0, 10, 0), view.button3.CornerRadius);

        view.border.CornerRadius = new CornerRadius(50);
        Assert.Equal(new CornerRadius(50, 0, 50, 0), view.button3.CornerRadius);
    }
}