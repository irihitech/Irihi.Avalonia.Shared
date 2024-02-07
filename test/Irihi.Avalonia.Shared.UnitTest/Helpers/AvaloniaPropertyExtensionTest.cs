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
}