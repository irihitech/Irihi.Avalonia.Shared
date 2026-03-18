using Avalonia;
using Avalonia.Media.Imaging;
using Irihi.Avalonia.Shared.Shapes;

namespace Irihi.Avalonia.Shared.UnitTest.Shapes;

public class IrihiLogoTest
{
    [Fact]
    public void Size_Should_Be_Correct()
    {
        var logo = new IrihiLogo() { Width = 80 };
        logo.Measure(new Size(80, 80));
        logo.Arrange(new Rect(0, 0, 80, 80));
        Assert.Equal(80, logo.Bounds.Width);
        Assert.Equal(60, logo.Bounds.Height);
    }
}