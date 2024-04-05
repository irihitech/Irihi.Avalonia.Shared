using System.ComponentModel;
using System.Net.Http.Headers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Irihi.Avalonia.Shared.Shapes;

namespace Irihi.Avalonia.Shared.UnitTest.Shapes;

public class PureRingTest
{
    [Fact]
    public void Ring_Bounds_Should_Equals_To_Diameter()
    {
        var ring = new PureRing() { Diameter = 50, InnerDiameter = 20, Width = 100, Height = 200 };
        ring.Measure(new Size(100, 200));
        ring.Arrange(new Rect(0, 0, 100, 200));
        Assert.Equal(50, ring.Bounds.Width);
        Assert.Equal(50, ring.Bounds.Height);
    }
    
    [Fact]
    public void Ring_InnerDiameter_Should_Be_Less_Than_Diameter()
    {
        var ring = new PureRing() { Diameter = 50, InnerDiameter = 20, Width = 100, Height = 200 };
        ring.Measure(new Size(100, 200));
        ring.Arrange(new Rect(0, 0, 100, 200));
        Assert.True(ring.InnerDiameter < ring.Diameter);
    }
}