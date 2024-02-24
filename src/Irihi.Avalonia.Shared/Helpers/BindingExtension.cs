using Avalonia;
using Avalonia.Data;

namespace Irihi.Avalonia.Shared.Helpers;

public static class BindingExtension
{
    public static bool TryBind(this AvaloniaObject obj, AvaloniaProperty property, IBinding? binding)
    {
        if (binding is null) return false;
        obj.Bind(property, binding);
        return true;
    }
}