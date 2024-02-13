using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Reactive;

namespace Irihi.Avalonia.Shared.Helpers;

public static class BooleanPropertyMixin
{
    public static void Attach<TControl>(AvaloniaProperty<bool> property, string pseudoClass, RoutedEvent<RoutedEventArgs>? routedEvent = null) 
        where TControl: Control
    {
        property.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>((args) =>
        {
            OnPropertyChanged<TControl, RoutedEventArgs>(args, pseudoClass, routedEvent);
        }));   
    }

    private static void OnPropertyChanged<TControl, TArgs>(AvaloniaPropertyChangedEventArgs<bool> args, string pseudoClass, RoutedEvent<TArgs>? routedEvent) 
        where TControl: Control
        where TArgs: RoutedEventArgs, new()
    {
        if(args.Sender is TControl control)
        {
            PseudolassesExtensions.Set(control.Classes, pseudoClass, args.NewValue.Value);
            if (routedEvent is not null)
            {
                control.RaiseEvent(new TArgs() { RoutedEvent = routedEvent });
            }
        }
    }

    public static void Attach<TControl, TArgs>(AvaloniaProperty<bool> property, string pseudoClass,
        RoutedEvent<TArgs>? routedEvent = null)
        where TControl: Control
        where TArgs: RoutedEventArgs, new()
    {
        property.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>((args) =>
        {
            OnPropertyChanged<TControl, TArgs>(args, pseudoClass, routedEvent);
        }));
    }
}