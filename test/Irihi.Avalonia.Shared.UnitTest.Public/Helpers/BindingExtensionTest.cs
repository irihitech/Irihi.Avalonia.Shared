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
        var result = t.TryBind(TextBlock.ForegroundProperty, t[!TextBlock.BackgroundProperty]);
        Assert.True(result.Result);
        // Binding init success
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.White).ToString());
        // Changing binding source success
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Yellow).ToString());
        
        // Dispose binding
        result.Dispose();
        
        // Foreground fall back to own value
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
        // Change binding source no effect because binding is disposed. 
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
    }
    
    [Fact]
    public void SetNullBinding_Fail()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Black);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        var result = t.TryBind(TextBlock.ForegroundProperty, null);
        Assert.False(result.Result);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
        
        // Dispose binding won't throw null exception. . 
        result.Dispose();
    }
}