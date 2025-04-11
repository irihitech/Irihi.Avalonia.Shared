using Avalonia;

namespace Irihi.Avalonia.Shared.MarkupExtensions;

public class CornerRadiusMixerExtension : IMarkupExtension<CornerRadius>
{
    public double TopLeft { get; set; }
    public double TopRight { get; set; }
    public double BottomRight { get; set; }
    public double BottomLeft { get; set; }

    public CornerRadiusMixerExtension()
    {
    }

    public CornerRadiusMixerExtension(double uniformRadius)
    {
        TopLeft = TopRight = BottomLeft = BottomRight = uniformRadius;
    }

    public CornerRadiusMixerExtension(double topLeft, double topRight, double bottomRight, double bottomLeft)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomRight = bottomRight;
        BottomLeft = bottomLeft;
    }

    public CornerRadius ProvideValue(IServiceProvider serviceProvider)
    {
        return new CornerRadius(TopLeft, TopRight, BottomRight, BottomLeft);
    }
}