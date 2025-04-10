﻿using Avalonia;

namespace Irihi.Avalonia.Shared.MarkupExtensions;

public class CornerRadiusBridgeExtension : IMarkupExtension<CornerRadius>
{
    public double TopLeft { get; set; }
    public double TopRight { get; set; }
    public double BottomRight { get; set; }
    public double BottomLeft { get; set; }

    public CornerRadiusBridgeExtension()
    {
    }

    public CornerRadiusBridgeExtension(double uniformRadius)
    {
        TopLeft = TopRight = BottomLeft = BottomRight = uniformRadius;
    }

    public CornerRadiusBridgeExtension(double topLeft, double topRight, double bottomRight, double bottomLeft)
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