using Avalonia;
using Irihi.Avalonia.Shared.Shapes;

namespace Irihi.Avalonia.Shared.UnitTest.Shapes;

public class PureCircleTest
{
    [Fact]
    public void Circle_Bounds_Should_Equals_To_Diameter()
    {
        var circle = new PureCircle() { Diameter = 50, Width = 100, Height = 200 };
        circle.Measure(new Size(100, 200));
        circle.Arrange(new Rect(0, 0, 100, 200));
        Assert.Equal(50, circle.Bounds.Width);
        Assert.Equal(50, circle.Bounds.Height);
    }
    
}