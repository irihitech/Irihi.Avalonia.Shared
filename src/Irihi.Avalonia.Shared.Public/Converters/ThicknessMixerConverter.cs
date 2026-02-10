using System.Globalization;
using Avalonia;

namespace Irihi.Avalonia.Shared.Converters;

public class ThicknessMixerConverter : MarkupValueConverter
{
    public ThicknessPosition Position { get; set; } = ThicknessPosition.All;

    public double Scale { get; set; } = 1;

    public ThicknessMixerConverter()
    {
    }

    public ThicknessMixerConverter(ThicknessPosition position)
    {
        Position = position;
    }

    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Thickness t)
        {
            var left = Position.HasFlag(ThicknessPosition.Left) ? t.Left * Scale : 0;
            var top = Position.HasFlag(ThicknessPosition.Top) ? t.Top * Scale : 0;
            var right = Position.HasFlag(ThicknessPosition.Right) ? t.Right * Scale : 0;
            var bottom = Position.HasFlag(ThicknessPosition.Bottom) ? t.Bottom * Scale : 0;
            return new Thickness(left, top, right, bottom);
        }

        return AvaloniaProperty.UnsetValue;
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