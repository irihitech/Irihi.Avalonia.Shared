using System.Globalization;
using Avalonia;

namespace Irihi.Avalonia.Shared.Converters;

public class CornerRadiusMixerConverter : MarkupValueConverter
{
    public CornerRadiusPosition Position { get; set; } = CornerRadiusPosition.All;

    public double Scale { get; set; } = 1;

    public CornerRadiusMixerConverter()
    {
    }

    public CornerRadiusMixerConverter(CornerRadiusPosition position)
    {
        Position = position;
    }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is CornerRadius r)
        {
            var topLeft = Position.HasFlag(CornerRadiusPosition.TopLeft) ? r.TopLeft * Scale : 0;
            var topRight = Position.HasFlag(CornerRadiusPosition.TopRight) ? r.TopRight * Scale : 0;
            var bottomLeft = Position.HasFlag(CornerRadiusPosition.BottomLeft) ? r.BottomLeft * Scale : 0;
            var bottomRight = Position.HasFlag(CornerRadiusPosition.BottomRight) ? r.BottomRight * Scale : 0;
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