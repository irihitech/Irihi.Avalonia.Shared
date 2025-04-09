using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Irihi.Avalonia.Shared.MarkupExtensions;

namespace Irihi.Avalonia.Shared.Converters;

public class CornerRadiusConverter : IMarkupExtension<IValueConverter>, IValueConverter
{
    private readonly CornerRadiusPosition _position = CornerRadiusPosition.All;

    public CornerRadiusConverter()
    {
    }

    public CornerRadiusConverter(CornerRadiusPosition position)
    {
        _position = position;
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is CornerRadius r)
        {
            double topLeft = _position.HasFlag(CornerRadiusPosition.TopLeft) ? r.TopLeft : 0;
            double topRight = _position.HasFlag(CornerRadiusPosition.TopRight) ? r.TopRight : 0;
            double bottomLeft = _position.HasFlag(CornerRadiusPosition.BottomLeft) ? r.BottomLeft : 0;
            double bottomRight = _position.HasFlag(CornerRadiusPosition.BottomRight) ? r.BottomRight : 0;
            return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
        }

        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public IValueConverter ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}

[Flags]
public enum CornerRadiusPosition
{
    None = 0,
    TopLeft = 1,
    TopRight = 2,
    BottomRight = 4,
    BottomLeft = 8,
    Left = TopLeft | BottomLeft,
    Top = TopLeft | TopRight,
    Right = TopRight | BottomRight,
    Bottom = BottomLeft | BottomRight,
    All = TopLeft | TopRight | BottomLeft | BottomRight,
}