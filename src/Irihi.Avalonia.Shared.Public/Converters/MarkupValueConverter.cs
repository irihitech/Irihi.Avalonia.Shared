using System.Globalization;
using Avalonia.Data.Converters;
using Irihi.Avalonia.Shared.MarkupExtensions;

namespace Irihi.Avalonia.Shared.Converters;

public abstract class MarkupValueConverter : IMarkupExtension<IValueConverter>, IValueConverter
{
    public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

    public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public IValueConverter ProvideValue(IServiceProvider _) => this;
}
