using System.Globalization;
using Avalonia;
using Irihi.Avalonia.Shared.Converters;

namespace Irihi.Avalonia.Shared.UnitTest.Converters;

public class ThicknessConverterTests
{
    private readonly ThicknessConverter _converter = new();

    public static TheoryData<ThicknessPosition, Thickness, Thickness> PositionTestCases => new()
    {
        {
            ThicknessPosition.All,
            new Thickness(1, 2, 3, 4),
            new Thickness(1, 2, 3, 4)
        },
        {
            ThicknessPosition.Vertical | ThicknessPosition.Right,
            new Thickness(10, 20, 30, 40),
            new Thickness(0, 20, 30, 40)
        },
        {
            ThicknessPosition.Horizontal,
            new Thickness(5, 6, 7, 8),
            new Thickness(5, 0, 7, 0)
        },
        {
            ThicknessPosition.TopLeft,
            new Thickness(2, 3, 4, 5),
            new Thickness(2, 3, 0, 0)
        },
        {
            ThicknessPosition.None,
            new Thickness(10, 20, 30, 40),
            new Thickness(0)
        },
        {
            ThicknessPosition.Bottom,
            new Thickness(100, 200, 300, 400),
            new Thickness(0, 0, 0, 400)
        },
        {
            ThicknessPosition.Vertical | ThicknessPosition.BottomLeft,
            new Thickness(6, 7, 8, 9),
            new Thickness(6, 7, 0, 9)
        }
    };

    [Theory]
    [MemberData(nameof(PositionTestCases))]
    public void Convert_WithPositionFlags_ReturnsFilteredThickness(
        ThicknessPosition position,
        Thickness input,
        Thickness expected)
    {
        // Arrange
        var converter = new ThicknessConverter(position);

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
        Assert.Throws<NotImplementedException>(
            () => _converter.ConvertBack(null, typeof(object), null, CultureInfo.InvariantCulture)
        );
    }
}