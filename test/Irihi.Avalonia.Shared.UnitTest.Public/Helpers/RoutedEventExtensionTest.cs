using Avalonia.Controls;
using Avalonia.Input;
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
        button.RaiseEvent(new RoutedEventArgs { Source = button, RoutedEvent = Button.ClickEvent});
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
        Button.ClickEvent.AddHandler(Handler, null, null);
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
    public void EventHandler_Add_With_Strategy_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, RoutingStrategies.Bubble, false, button);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(1, count);
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
    public void EventHandler_AddDisposable_Null_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        var _ = Button.ClickEvent.AddDisposableHandler(Handler, null, null);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(0, count);
    }
    
    [Fact]
    public void EventHandler_AddDisposable_Multiple_Success()
    {
        var button = new Button();
        var button2 = new Button();
        var button3 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        var result = Button.ClickEvent.AddDisposableHandler(Handler, button, button2, button3);
        result.Dispose();
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(0, count);
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
    
    [Fact]
    public void EventHandler_AddDisposable_With_Strategy_Success()
    {
        var button = new Button();
        var button2 = new Panel();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }

        var _ = Button.ClickEvent.AddDisposableHandler(Handler, 
            RoutingStrategies.Bubble, 
            false, 
            button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_AddList_Success()
    {
        var button = new Button();
        var button2 = new Button();
        var button3 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, new List<Button>{button, button2, button3});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
        
        Button.ClickEvent.RemoveHandler(Handler, new List<Button>{button, button2, button3});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_AddArray_Success()
    {
        var button = new Button();
        var button2 = new Button();
        var button3 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, new Button[]{button, button2, button3});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
        
        Button.ClickEvent.RemoveHandler(Handler, new Button[]{button, button2, button3});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_AddArray_Null_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, new Button?[]{button, null});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(1, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_AddList_Null_Success()
    {
        var button = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, new List<Button?>{button, null});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        Assert.Equal(1, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_With_Strategy_AddList_Success()
    {
        var button = new Button();
        var button2 = new Button();
        var button3 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddHandler(Handler, new List<Button>{button, button2, button3}, RoutingStrategies.Bubble, true);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
        
        Button.ClickEvent.RemoveHandler(Handler, new List<Button>{button, button2, button3});
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
    }
    
    [Fact]
    public void EventHandler_AddDisposableHandler_With_Strategy_AddList_Success()
    {
        var button = new Button();
        var button2 = new Button();
        var button3 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        var disposables = Button.ClickEvent.AddDisposableHandler(Handler, new[]{button, button2, button3}, RoutingStrategies.Bubble, true);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
        
        disposables.Dispose();
        
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        button2.RaiseEvent(new RoutedEventArgs(){ Source = button2, RoutedEvent = Button.ClickEvent});
        button3.RaiseEvent(new RoutedEventArgs(){ Source = button3, RoutedEvent = Button.ClickEvent});
        Assert.Equal(3, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_Multi_Type_Success()
    {
        var button = new Button();
        var panel = new Panel();
        int count = 0;
        void Handler(object? sender, GotFocusEventArgs args)
        {
            count++;
        }
        InputElement.GotFocusEvent.AddHandler(Handler, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
        
        InputElement.GotFocusEvent.RemoveHandler(Handler, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
    }

    [Fact]
    public void EventHandler_AddDisposableHandler_Multi_Type_Success()
    {
        var button = new Button();
        var panel = new Panel();
        int count = 0;
        void Handler(object? sender, GotFocusEventArgs args)
        {
            count++;
        }
        var disposable = InputElement.GotFocusEvent.AddDisposableHandler(Handler, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
        
        disposable.Dispose();
        
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_Multi_With_Strategy_Type_Success()
    {
        var button = new Button();
        var panel = new Panel();
        int count = 0;
        void Handler(object? sender, GotFocusEventArgs args)
        {
            count++;
        }
        InputElement.GotFocusEvent.AddHandler(Handler, RoutingStrategies.Bubble, false, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
        
        InputElement.GotFocusEvent.RemoveHandler(Handler, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
    }

    [Fact]
    public void EventHandler_AddDisposableHandler_With_Strategy_Multi_Type_Success()
    {
        var button = new Button();
        var panel = new Panel();
        int count = 0;
        void Handler(object? sender, GotFocusEventArgs args)
        {
            count++;
        }
        var disposable = InputElement.GotFocusEvent.AddDisposableHandler(Handler, RoutingStrategies.Bubble, false, button, panel);
        button.RaiseEvent(new GotFocusEventArgs(){ Source = button, RoutedEvent = InputElement.GotFocusEvent});
        panel.RaiseEvent(new GotFocusEventArgs(){ Source = panel, RoutedEvent = InputElement.GotFocusEvent});
        
        Assert.Equal(2, count);
        
        disposable.Dispose();
        
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void EventHandler_AddHandler_Multi_Type_List_Success()
    {
        var button = new Button();
        var button2 = new Button();
        int count = 0;
        void Handler(object? sender, RoutedEventArgs args)
        {
            count++;
        }
        Button.ClickEvent.AddDisposableHandler(Handler, RoutingStrategies.Bubble, false, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        
        Assert.Equal(1, count);
        
        Button.ClickEvent.RemoveHandler(Handler, button, button2);
        button.RaiseEvent(new RoutedEventArgs(){ Source = button, RoutedEvent = Button.ClickEvent});
        
        Assert.Equal(1, count);
    }
}