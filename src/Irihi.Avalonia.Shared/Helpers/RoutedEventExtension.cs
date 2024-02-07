using Avalonia.Controls;
using Avalonia.Interactivity;

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
    
    public static void RemoveHandler<TArgs>(this RoutedEvent<TArgs> routedEvent, EventHandler<TArgs> handler, params Interactive?[] controls)
        where TArgs : RoutedEventArgs
    {
        foreach (var t in controls)
        {
            t?.RemoveHandler(routedEvent, handler);
        }
    }
}