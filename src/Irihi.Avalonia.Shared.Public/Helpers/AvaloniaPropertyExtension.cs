using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Irihi.Avalonia.Shared.Helpers;

public static class AvaloniaPropertyExtension
{
    public static void SetValue<T>(this AvaloniaProperty<T> property, T value, params AvaloniaObject?[] objects)
    {
        foreach (var obj in objects)
        {
            obj?.SetValue(property, value);
        }
    }
    
    public static void AffectsPseudoClass<TControl>(this AvaloniaProperty<bool> property, string pseudoClass, RoutedEvent<RoutedEventArgs>? routedEvent = null) 
        where TControl: Control
    {
        property.Changed.AddClassHandler<TControl, bool>((control, args) => {OnPropertyChanged(control, args, pseudoClass, routedEvent); });
    }

    private static void OnPropertyChanged<TControl, TArgs>(TControl control, AvaloniaPropertyChangedEventArgs<bool> args, string pseudoClass, RoutedEvent<TArgs>? routedEvent) 
        where TControl: Control
        where TArgs: RoutedEventArgs, new()
    {
        PseudolassesExtensions.Set(control.Classes, pseudoClass, args.NewValue.Value);
        if (routedEvent is not null)
        {
            control.RaiseEvent(new TArgs() { RoutedEvent = routedEvent });
        }
    }

    public static void AffectsPseudoClass<TControl, TArgs>(this AvaloniaProperty<bool> property, string pseudoClass,
        RoutedEvent<TArgs>? routedEvent = null)
        where TControl: Control
        where TArgs: RoutedEventArgs, new()
    {
        property.Changed.AddClassHandler<TControl, bool>((control, args)=> {OnPropertyChanged(control, args, pseudoClass, routedEvent); });
    }

}