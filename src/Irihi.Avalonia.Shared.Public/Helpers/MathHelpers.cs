using Avalonia.Controls.Platform;
using Avalonia.Utilities;

namespace Irihi.Avalonia.Shared.Helpers;

public static class MathHelpers
{
    private static (double min, double max) GetMinMax(double a, double b)
    {
        return a >= b ? (b, a) : (a, b);
    }
    
    private static (float min, float max) GetMinMax(float a, float b)
    {
        return a >= b ? (b, a) : (a, b);
    }
    
    private static (decimal min, decimal max) GetMinMax(decimal a, decimal b)
    {
        return a >= b ? (b, a) : (a, b);
    }
    
    private static (int min, int max) GetMinMax(int a, int b)
    {
        return a >= b ? (b, a) : (a, b);
    }
    
    public static double SafeClamp(double value, double min, double max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }

    public static decimal SafeClamp(decimal value, decimal min, decimal max)
    {
        var minimum = Math.Min(min, max);
        var maximum = Math.Max(min, max);
        return Math.Min(Math.Max(value, minimum), maximum);
    }

    public static int SafeClamp(int value, int min, int max)
    {
        var minimum = Math.Min(min, max);
        var maximum = Math.Max(min, max);
        return Math.Min(Math.Max(value, minimum), maximum);
    }
    
    public static float SafeClamp(float value, float min, float max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }
}