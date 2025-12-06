using System.Globalization;
using Avalonia;
using Irihi.Avalonia.Shared.Converters;

namespace Irihi.Avalonia.Shared.UnitTest.Converters;

public class ThicknessMixerConverterTests
{
    private readonly ThicknessMixerConverter _converter = new();

    public static TheoryData<ThicknessPosition, double, Thickness, Thickness> PositionTestCases => new()
    {
        {
            ThicknessPosition.All,
            1,
            new Thickness(1, 2, 3, 4),
            new Thickness(1, 2, 3, 4)
        },
        {
            ThicknessPosition.Vertical | ThicknessPosition.Right,
            1,
            new Thickness(10, 20, 30, 40),
            new Thickness(0, 20, 30, 40)
        },
        {
            ThicknessPosition.Horizontal,
            2,
            new Thickness(5, 6, 7, 8),
            new Thickness(10, 0, 14, 0)
        },
        {
            ThicknessPosition.TopLeft,
            0.5,
            new Thickness(2, 3, 4, 5),
            new Thickness(1, 1.5, 0, 0)
        },
        {
            ThicknessPosition.None,
            1,
            new Thickness(10, 20, 30, 40),
            new Thickness(0)
        },
        {
            ThicknessPosition.Bottom,
            0,
            new Thickness(100, 200, 300, 400),
            new Thickness(0)
        },
        {
            ThicknessPosition.Vertical | ThicknessPosition.BottomLeft,
            -1,
            new Thickness(6, 7, 8, 9),
            new Thickness(-6, -7, 0, -9)
        },
        {
            ThicknessPosition.TopRight,
            20,
            new Thickness(10, 20, 30, 40),
            new Thickness(0, 400, 600, 0)
        },
        {
            ThicknessPosition.BottomRight,
            Math.PI,
            new Thickness(1, 1, 1, 1),
            new Thickness(0, 0, Math.PI, Math.PI)
        }
    };

    [Theory]
    [MemberData(nameof(PositionTestCases))]
    public void Convert_WithPositionFlags_ReturnsFilteredThickness(
        ThicknessPosition position,
        double scale,
        Thickness input,
        Thickness expected)
    {
        // Arrange
        var converter = new ThicknessMixerConverter
        {
            Position = position,
            Scale = scale
        };

        // Act
        var result = converter.Convert(input, typeof(Thickness), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(expected, (Thickness)result);
    }

    [Fact]
    public void Convert_WithNullValue_ReturnsUnsetValue()
    {
        var result = _converter.Convert(null, typeof(Thickness), null, CultureInfo.InvariantCulture);
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void Convert_WithInvalidType_ReturnsUnsetValue()
    {
        var result = _converter.Convert("invalid", typeof(Thickness), null, CultureInfo.InvariantCulture);
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(null, typeof(object), null, CultureInfo.InvariantCulture));
    }
}