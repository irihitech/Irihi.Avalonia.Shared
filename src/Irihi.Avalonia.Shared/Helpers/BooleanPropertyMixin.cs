using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Reactive;

namespace Irihi.Avalonia.Shared.Helpers;

public static class BooleanPropertyMixin
{
    public static void Attach<TControl>(AvaloniaProperty<bool> property, string pseudoClass, RoutedEvent? routedEvent = null) 
        where TControl: Control
    {
        property.Changed.Subscribe(new AnonymousObserver<AvaloniaPropertyChangedEventArgs<bool>>((args) =>
        {
            OnPropertyChanged<TControl>(args, pseudoClass, routedEvent);
        }));   
    }

    private static void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<bool> args, string pseudoClass, RoutedEvent? routedEvent) where T: Control
    {
        if(args.Sender is T control)
        {
            PseudolassesExtensions.Set(control.Classes, pseudoClass, args.NewValue.Value);
            if (routedEvent is not null)
            {
                control.RaiseEvent(new RoutedEventArgs() { RoutedEvent = routedEvent });
            }
        }
    }
}