using Avalonia;
using Avalonia.Controls;

namespace Irihi.Avalonia.Shared.Property;

public class ControlSlot
{
    public static readonly AttachedProperty<object?> PrefixProperty =
        AvaloniaProperty.RegisterAttached<ControlSlot, Control, object?>("Prefix");

    public static void SetPrefix(Control obj, object? value) => obj.SetValue(PrefixProperty, value);
    public static object? GetPrefix(Control obj) => obj.GetValue(PrefixProperty);
    
}