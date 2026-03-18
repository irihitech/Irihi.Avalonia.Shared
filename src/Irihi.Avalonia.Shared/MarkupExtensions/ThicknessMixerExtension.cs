using Avalonia;

namespace Irihi.Avalonia.Shared.MarkupExtensions;

public class ThicknessMixerExtension : IMarkupExtension<Thickness>
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Right { get; set; }
    public double Bottom { get; set; }

    public ThicknessMixerExtension()
    {
    }

    public ThicknessMixerExtension(double uniformLength)
    {
        Left = Right = Top = Bottom = uniformLength;
    }

    public ThicknessMixerExtension(double horizontal, double vertical)
    {
        Left = Right = horizontal;
        Top = Bottom = vertical;
    }

    public ThicknessMixerExtension(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Thickness ProvideValue(IServiceProvider serviceProvider)
    {
        return new Thickness(Left, Top, Right, Bottom);
    }
}