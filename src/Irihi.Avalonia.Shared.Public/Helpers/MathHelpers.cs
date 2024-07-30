/*
This is a port of https://github.com/AvaloniaUI/Avalonia/blob/fc70228dc9847ed70e9cb3f3904a776017f17667/src/Avalonia.Base/Utilities/MathUtilities.cs
*/

using Avalonia;

namespace Irihi.Avalonia.Shared.Helpers;

public static class MathHelpers
{
    internal const double DoubleEpsilon = 2.220446049250313E-16;
    internal const float FloatEpsilon = 1.1920929E-07f;

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

    /// <summary>
    ///     Clamps a value between a minimum and maximum value.
    /// </summary>
    /// <param name="value"> The value to clamp. </param>
    /// <param name="min"> The minimum value. </param>
    /// <param name="max"> The maximum value. </param>
    /// <returns></returns>
    public static double SafeClamp(double value, double min, double max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }

    /// <summary>
    ///     Clamps a value between a minimum and maximum value.
    /// </summary>
    /// <param name="value"> The value to clamp. </param>
    /// <param name="min"> The minimum value. </param>
    /// <param name="max"> The maximum value. </param>
    /// <returns></returns>
    public static decimal SafeClamp(decimal value, decimal min, decimal max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }

    /// <summary>
    ///     Clamps a value between a minimum and maximum value.
    /// </summary>
    /// <param name="value"> The value to clamp. </param>
    /// <param name="min"> The minimum value. </param>
    /// <param name="max"> The maximum value. </param>
    /// <returns></returns>
    public static int SafeClamp(int value, int min, int max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }

    /// <summary>
    ///     Clamps a value between a minimum and maximum value.
    /// </summary>
    /// <param name="value"> The value to clamp. </param>
    /// <param name="min"> The minimum value. </param>
    /// <param name="max"> The maximum value. </param>
    /// <returns></returns>
    public static float SafeClamp(float value, float min, float max)
    {
        (min, max) = GetMinMax(min, max);
        if (value < min) return min;
        return value > max ? max : value;
    }

    /// <summary>
    ///     AreClose - Returns whether or not two doubles are "close".  That is, whether or
    ///     not they are within epsilon of each other. Note that this epsilon is proportional
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool AreClose(double value1, double value2)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (value1 == value2) return true;
        var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * DoubleEpsilon;
        var delta = value1 - value2;
        return -eps < delta && eps > delta;
    }

    /// <summary>
    /// AreClose - Returns whether or not two doubles are "close".  That is, whether or
    /// not they are within epsilon of each other.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    /// <param name="eps"> The fixed epsilon value used to compare.</param>
    public static bool AreClose(double value1, double value2, double eps)
    {
        //in case they are Infinities (then epsilon check does not work)
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (value1 == value2) return true;
        var delta = value1 - value2;
        return -eps < delta && eps > delta;
    }

    /// <summary>
    ///     AreClose - Returns whether or not two doubles are "close".  That is, whether or
    ///     not they are within epsilon of each other. Note that this epsilon is proportional
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public static bool AreClose(float value1, float value2)
    {
        //in case they are Infinities (then epsilon check does not work)
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (value1 == value2) return true;
        var eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0f) * FloatEpsilon;
        var delta = value1 - value2;
        return -eps < delta && eps > delta;
    }

    /// <summary>
    /// LessThan - Returns whether or not the first double is less than the second double.
    /// That is, whether or not the first is strictly less than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool LessThan(double value1, double value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// LessThan - Returns whether or not the first float is less than the second float.
    /// That is, whether or not the first is strictly less than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first single float to compare. </param>
    /// <param name="value2"> The second single float to compare. </param>
    public static bool LessThan(float value1, float value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThan - Returns whether or not the first double is greater than the second double.
    /// That is, whether or not the first is strictly greater than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool GreaterThan(double value1, double value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThan - Returns whether or not the first float is greater than the second float.
    /// That is, whether or not the first is strictly greater than *and* not within epsilon of
    /// the other number.
    /// </summary>
    /// <param name="value1"> The first float to compare. </param>
    /// <param name="value2"> The second float to compare. </param>
    public static bool GreaterThan(float value1, float value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// LessThanOrClose - Returns whether or not the first double is less than or close to
    /// the second double.  That is, whether or not the first is strictly less than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool LessThanOrClose(double value1, double value2)
    {
        return value1 < value2 || AreClose(value1, value2);
    }

    /// <summary>
    /// LessThanOrClose - Returns whether or not the first float is less than or close to
    /// the second float.  That is, whether or not the first is strictly less than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first float to compare. </param>
    /// <param name="value2"> The second float to compare. </param>
    public static bool LessThanOrClose(float value1, float value2)
    {
        return value1 < value2 || AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThanOrClose - Returns whether or not the first double is greater than or close to
    /// the second double.  That is, whether or not the first is strictly greater than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first double to compare. </param>
    /// <param name="value2"> The second double to compare. </param>
    public static bool GreaterThanOrClose(double value1, double value2)
    {
        return value1 > value2 || AreClose(value1, value2);
    }

    /// <summary>
    /// GreaterThanOrClose - Returns whether or not the first float is greater than or close to
    /// the second float.  That is, whether or not the first is strictly greater than or within
    /// epsilon of the other number.
    /// </summary>
    /// <param name="value1"> The first float to compare. </param>
    /// <param name="value2"> The second float to compare. </param>
    public static bool GreaterThanOrClose(float value1, float value2)
    {
        return value1 > value2 || AreClose(value1, value2);
    }

    /// <summary>
    /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
    /// but this is faster.
    /// </summary>
    /// <param name="value"> The double to compare to 1. </param>
    public static bool IsOne(double value)
    {
        return Math.Abs(value - 1.0) < 10.0 * DoubleEpsilon;
    }

    /// <summary>
    /// IsOne - Returns whether or not the float is "close" to 1.  Same as AreClose(float, 1),
    /// but this is faster.
    /// </summary>
    /// <param name="value"> The float to compare to 1. </param>
    public static bool IsOne(float value)
    {
        return Math.Abs(value - 1.0f) < 10.0f * FloatEpsilon;
    }

    /// <summary>
    /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
    /// but this is faster.
    /// </summary>
    /// <param name="value"> The double to compare to 0. </param>
    public static bool IsZero(double value)
    {
        return Math.Abs(value) < 10.0 * DoubleEpsilon;
    }

    /// <summary>
    /// IsZero - Returns whether or not the float is "close" to 0.  Same as AreClose(float, 0),
    /// but this is faster.
    /// </summary>
    /// <param name="value"> The float to compare to 0. </param>
    public static bool IsZero(float value)
    {
        return Math.Abs(value) < 10.0f * FloatEpsilon;
    }

    /// <summary>
    /// Converts an angle in degrees to radians.
    /// </summary>
    /// <param name="angle">The angle in degrees.</param>
    /// <returns>The angle in radians.</returns>
    public static double DegreeToRadians(double angle)
    {
        return angle * (Math.PI / 180d);
    }

    /// <summary>
    /// Converts an angle in gradians to radians.
    /// </summary>
    /// <param name="angle">The angle in gradians.</param>
    /// <returns>The angle in radians.</returns>
    public static double GradiansToRadians(double angle)
    {
        return angle * (Math.PI / 200d);
    }

    /// <summary>
    /// Converts an angle in turns to radians.
    /// </summary>
    /// <param name="angle">The angle in turns.</param>
    /// <returns>The angle in radians.</returns>
    public static double TurnToRadians(double angle)
    {
        return angle * 2 * Math.PI;
    }

    /// <summary>
    /// Calculates the point of an angle on an ellipse.
    /// </summary>
    /// <param name="centre">The centre point of the ellipse.</param>
    /// <param name="radiusX">The x radius of the ellipse.</param>
    /// <param name="radiusY">The y radius of the ellipse.</param>
    /// <param name="angle">The angle in radians.</param>
    /// <returns>A point on the ellipse.</returns>
    public static Point GetEllipsePoint(Point centre, double radiusX, double radiusY, double angle)
    {
        return new Point(radiusX * Math.Cos(angle) + centre.X, radiusY * Math.Sin(angle) + centre.Y);
    }
}