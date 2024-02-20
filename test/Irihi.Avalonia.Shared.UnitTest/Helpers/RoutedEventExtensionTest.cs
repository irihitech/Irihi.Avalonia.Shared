using Avalonia.Controls;
using Avalonia.Interactivity;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class RoutedEventExtensionTest
{
    [Fact]
    public void EventHandler_Add_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, button);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(1, count);
    }
    
    [Fact]
    public void EventHandler_Add_Null_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, button, null);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(1, count);
    }
    
    [Fact]
    public void EventHandler_Multiple_Add_Success()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_Remove_Without_SideEffect()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.RemoveHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(0, count);
    }
    
    [Fact]
    public void EventHandler_Remove_Success()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
        Button.ClickEvent.RemoveHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_AddDisposable_Success()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        var _ = Button.ClickEvent.AddDisposableHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_AddDisposable_Dispose_Success()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        var disposable = Button.ClickEvent.AddDisposableHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
        disposable.Dispose();
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
    }
}