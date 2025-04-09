using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Irihi.Avalonia.Shared.MarkupExtensions;

namespace Irihi.Avalonia.Shared.Converters;

public class ThicknessConverter : IMarkupExtension<IValueConverter>, IValueConverter
{
    private readonly ThicknessPosition _position = ThicknessPosition.All;

    public ThicknessConverter()
    {
    }

    public ThicknessConverter(ThicknessPosition position)
    {
        _position = position;
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Thickness t)
        {
            var left = _position.HasFlag(ThicknessPosition.Left) ? t.Left : 0d;
            var top = _position.HasFlag(ThicknessPosition.Top) ? t.Top : 0d;
            var right = _position.HasFlag(ThicknessPosition.Right) ? t.Right : 0d;
            var bottom = _position.HasFlag(ThicknessPosition.Bottom) ? t.Bottom : 0d;
            return new Thickness(left, top, right, bottom);
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
public enum ThicknessPosition
{
    None = 0,
    Left = 1,
    Top = 2,
    Right = 4,
    Bottom = 8,
    TopLeft = Top | Left,
    TopRight = Top | Right,
    BottomRight = Bottom | Right,
    BottomLeft = Bottom | Left,
    Horizontal = Left | Right,
    Vertical = Top | Bottom,
    All = Left | Top | Right | Bottom,
}