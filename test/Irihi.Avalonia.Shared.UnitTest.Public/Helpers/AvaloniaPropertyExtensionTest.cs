using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class AvaloniaPropertyExtensionTest
{
    [Fact]
    public void Property_SetValue_AllowNullObject()
    {
        Visual.IsVisibleProperty.SetValue(true, null, null);
        Visual.IsVisibleProperty.SetValue(true, new Button(), null);
        Button? b = null;
        Visual.IsVisibleProperty.SetValue(true, b);
    }

    [Fact]
    public void Property_SetValue_AllowNullValue()
    {
        ToggleButton.IsCheckedProperty.SetValue(null, null, null);
    }

    [Fact]
    public void Property_SetValue_Effective()
    {
        Button b = new Button
        {
            IsVisible = false
        };
        Assert.False(b.IsVisible);
        Visual.IsVisibleProperty.SetValue(true, b);
        Assert.True(b.IsVisible);
    }
    
    [Fact]
    public void Property_MultipleObject_InArray()
    {
        Button b1 = new Button();
        Button b2 = new Button();
        Button[] buttons = {b1, b2};
        Visual.IsVisibleProperty.SetValue<bool, Button>(true, buttons);
        Assert.True(b1.IsVisible);
        Assert.True(b2.IsVisible);
    }
    
    [Fact]
    public void Property_MultipleObject_InList()
    {
        Button b1 = new Button();
        Button b2 = new Button();
        Visual.IsVisibleProperty.SetValue(true, new List<Button> { b1, b2 });
        Assert.True(b1.IsVisible);
        Assert.True(b2.IsVisible);
    }
}