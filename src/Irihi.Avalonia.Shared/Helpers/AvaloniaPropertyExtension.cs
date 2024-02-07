using Avalonia;

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

}