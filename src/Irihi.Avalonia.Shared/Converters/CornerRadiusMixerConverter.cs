using System.Globalization;
using Avalonia;

namespace Irihi.Avalonia.Shared.Converters;

public class CornerRadiusMixerConverter : MarkupValueConverter
{
    private readonly CornerRadiusPosition _position = CornerRadiusPosition.All;

    public CornerRadiusMixerConverter()
    {
    }

    public CornerRadiusMixerConverter(CornerRadiusPosition position)
    {
        _position = position;
    }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
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