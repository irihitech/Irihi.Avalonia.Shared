using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Irihi.Avalonia.Shared.Helpers;
using Irihi.Avalonia.Shared.Reactive;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class BindingExtensionTest
{
    [Fact]
    public void SetBinding_Success()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Blue);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        var binding = t.GetObservable(TextBlock.BackgroundProperty).ToBinding();
        var result = t.TryBind(TextBlock.ForegroundProperty, t[!TextBlock.BackgroundProperty]);
        Assert.True(result.Result);
        // Binding init success
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.White).ToString());
        // Changing binding source success
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Yellow).ToString());
        // Dispose binding
        result.Dispose();
        // Foreground fall back to default value, as local value level binding is disposed. 
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
        // Change binding source no effect because binding is disposed. 
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Black).ToString());
    }
    
    [Fact]
    public void SetNullBinding_Fail()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Blue);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        var result = t.TryBind(TextBlock.ForegroundProperty, null);
        Assert.False(result.Result);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Blue).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Blue).ToString());
        
        // Dispose binding won't throw null exception. . 
        result.Dispose();
    }
    
    [Fact]
    public void SetBindingIfUnset_NotFunctional_If_Value_IsSet()
    {
        TextBlock t = new TextBlock();
        t.Foreground = new SolidColorBrush(Colors.Blue);
        t.Background = new SolidColorBrush(Colors.White);
        Assert.True(t.IsSet(TextBlock.ForegroundProperty));
        Assert.NotEqual(t.Foreground.ToString(), t.Background.ToString());
        var result = t.TryBindIfUnset(TextBlock.ForegroundProperty, t[!TextBlock.BackgroundProperty]);
        Assert.False(result.Result);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Blue).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Blue).ToString());
        
        result.Dispose();

        Assert.Equal(t.Foreground.ToString(), new SolidColorBrush(Colors.Blue).ToString());
    }
    
    [Fact]
    public void SetBindingIfUnset_Success_If_Value_IsNotSet()
    {
        TextBlock t = new TextBlock();
        t.Background = new SolidColorBrush(Colors.White);
        Assert.False(t.IsSet(TextBlock.ForegroundProperty));
        var result = t.TryBindIfUnset(TextBlock.ForegroundProperty, t[!TextBlock.BackgroundProperty]);
        Assert.True(result.Result);
        Assert.Equal(t.Foreground?.ToString(), new SolidColorBrush(Colors.White).ToString());
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground?.ToString(), new SolidColorBrush(Colors.Yellow).ToString());
        
        // Dispose binding
        result.Dispose();
        
        // Foreground fall back to own value
        Assert.Equal(t.Foreground?.ToString(), new SolidColorBrush(Colors.Black).ToString());
        // Change binding source no effect because binding is disposed. 
        t.Background = new SolidColorBrush(Colors.Yellow);
        Assert.Equal(t.Foreground?.ToString(), new SolidColorBrush(Colors.Black).ToString());
    }
}