using Avalonia.Controls;
using Avalonia.Interactivity;
using Irihi.Avalonia.Shared.Reactive;

namespace Irihi.Avalonia.Shared.Helpers;

public static class RoutedEventExtension
{
    public static void AddHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        foreach (var t in controls)
        {
            t?.AddHandler(routedEvent, handler);
        }
    }
    
    public static void AddHandler<TArgs, TControl>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params TControl?[] controls) where TControl: Interactive
        where TArgs : RoutedEventArgs
    {
        foreach (var t in controls)
        {
            t?.AddHandler(routedEvent, handler);
        }
    }
    
    public static void AddHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, 
        EventHandler<TArgs> handler, 
        RoutingStrategies strategies = RoutingStrategies.Bubble | RoutingStrategies.Direct, 
        bool handledEventsToo = false, 
        params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        foreach (var t in controls)
        {
            t?.AddHandler(routedEvent, handler, strategies, handledEventsToo);
        }
    }
    
    public static void AddHandler<TArgs, TControl>(this RoutedEvent<TArgs> routedEvent, 
        EventHandler<TArgs> handler, 
        RoutingStrategies strategies = RoutingStrategies.Bubble | RoutingStrategies.Direct, 
        bool handledEventsToo = false, 
        params TControl?[] controls)
        where TArgs : RoutedEventArgs
        where TControl: Interactive
    {
        foreach (var t in controls)
        {
            t?.AddHandler(routedEvent, handler, strategies, handledEventsToo);
        }
    }
    
    public static void RemoveHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        foreach (var t in controls)
        {
            t?.RemoveHandler(routedEvent, handler);
        }
    }
    
    public static void RemoveHandler<TArgs, TControl>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params TControl?[] controls)
        where TArgs : RoutedEventArgs
        where TControl: Interactive
    {
        foreach (var t in controls)
        {
            t?.RemoveHandler(routedEvent, handler);
        }
    }
    
    public static IDisposable AddDisposableHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        List<IDisposable?> list = new List<IDisposable?>(controls.Length);
        foreach (var t in controls)
        {
            var disposable = t?.AddDisposableHandler(routedEvent, handler);
            if (disposable != null)
            {
                list.Add(disposable);
            }
        }
        var result = new ReadonlyDisposableCollection(list);
        return result;
    }
    
    public static IDisposable AddDisposableHandler<TArgs, TControl>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params TControl?[] controls)
        where TArgs : RoutedEventArgs
        where TControl: Interactive
    {
        List<IDisposable?> list = new List<IDisposable?>(controls.Length);
        foreach (var t in controls)
        {
            var disposable = t?.AddDisposableHandler(routedEvent, handler);
            if (disposable != null)
            {
                list.Add(disposable);
            }
        }
        var result = new ReadonlyDisposableCollection(list);
        return result;
    }
    
    public static IDisposable AddDisposableHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, 
        EventHandler<TArgs> handler, 
        RoutingStrategies strategies = RoutingStrategies.Bubble | RoutingStrategies.Direct, 
        bool handledEventsToo = false, 
        params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        List<IDisposable?> list = new List<IDisposable?>(controls.Length);
        foreach (var t in controls)
        {
            var disposable = t?.AddDisposableHandler(routedEvent, handler, strategies, handledEventsToo);
            if (disposable != null)
            {
                list.Add(disposable);
            }
        }
        var result = new ReadonlyDisposableCollection(list);
        return result;
    }
    
    public static IDisposable AddDisposableHandler<TArgs, TControl>(this RoutedEvent<TArgs> routedEvent, 
        EventHandler<TArgs> handler, 
        RoutingStrategies strategies = RoutingStrategies.Bubble | RoutingStrategies.Direct, 
        bool handledEventsToo = false, 
        params TControl?[] controls)
        where TArgs : RoutedEventArgs
        where TControl: Interactive
    {
        List<IDisposable?> list = new List<IDisposable?>(controls.Length);
        foreach (var t in controls)
        {
            var disposable = t?.AddDisposableHandler(routedEvent, handler, strategies, handledEventsToo);
            if (disposable != null)
            {
                list.Add(disposable);
            }
        }
        var result = new ReadonlyDisposableCollection(list);
        return result;
    }
}