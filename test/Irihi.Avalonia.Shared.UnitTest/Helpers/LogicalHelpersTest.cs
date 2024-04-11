using Avalonia.Controls;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class LogicalHelpersTest
{

    [Fact]
    public void CalculateDistanceFromLogicalParent()
    {
        var parent = new StackPanel();
        var child = new Button();
        parent.Children.Add(child);
        Assert.Equal(0, child.CalculateDistanceFromLogicalParent<StackPanel>());
        Assert.Equal(-1, child.CalculateDistanceFromLogicalParent<Grid>());
    }
}