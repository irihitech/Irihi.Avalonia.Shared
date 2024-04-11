using Avalonia.Controls;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class LogicalHelpersTest
{
    [Fact]
    public void CalculateDistanceFromLogicalParent_Null()
    {
        Assert.Equal(-1, (null as Button)!.CalculateDistanceFromLogicalParent<StackPanel>());
    }
    
    [Fact]
    public void CalculateDistanceFromLogicalParent_Default()
    {
        var parent = new StackPanel();
        var child = new Button();
        parent.Children.Add(child);
        Assert.Equal(1, child.CalculateDistanceFromLogicalParent<StackPanel>(-1));
        Assert.Equal(-1, child.CalculateDistanceFromLogicalParent<Grid>(-1));
    }
    
    [Fact]
    public void CalculateDistanceFromLogicalParent()
    {
        var parent = new StackPanel();
        var child = new Grid();
        var grandChild = new Button();
        parent.Children.Add(child);
        child.Children.Add(grandChild);
        Assert.Equal(2, grandChild.CalculateDistanceFromLogicalParent<StackPanel>());
        Assert.Equal(1, grandChild.CalculateDistanceFromLogicalParent<Grid>());
        Assert.Equal(-1, grandChild.CalculateDistanceFromLogicalParent<Canvas>());
    }
    
    [Fact]
    public void CalculateDistanceFromLogicalParent_Self()
    {
        var parent = new StackPanel();
        var child = new Grid();
        var grandChild = new Button();
        parent.Children.Add(child);
        child.Children.Add(grandChild);
        Assert.Equal(2, grandChild.CalculateDistanceFromLogicalParent<StackPanel>());
        Assert.Equal(1, grandChild.CalculateDistanceFromLogicalParent<Grid>());
        Assert.Equal(0, grandChild.CalculateDistanceFromLogicalParent<Button>());
    }
}