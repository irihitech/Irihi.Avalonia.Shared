using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class BindingExtensionTest
{
    [Fact]
    public void SetBinding_Success()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Black);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        bool b = t.TryBind(TextBlock.ForegroundProperty, t[!TextBlock.BackgroundProperty]);
        Assert.True(b);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.White).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Yellow).ToString());
    }
    
    [Fact]
    public void SetNullBinding_Fail()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Black);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        bool b = t.TryBind(TextBlock.ForegroundProperty, null);
        Assert.False(b);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
    }
}