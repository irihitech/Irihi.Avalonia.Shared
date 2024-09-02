using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Irihi.Avalonia.Shared.Converters;

public class ResourceConverter : ResourceDictionary, IValueConverter
{
    public virtual object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return AvaloniaProperty.UnsetValue;
        return TryGetResource(value, null, out var resource) ? resource : AvaloniaProperty.UnsetValue;
    }

    public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}