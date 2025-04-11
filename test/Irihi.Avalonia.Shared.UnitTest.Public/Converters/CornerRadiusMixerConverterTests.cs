using System.Globalization;
using Avalonia;
using Irihi.Avalonia.Shared.Converters;

namespace Irihi.Avalonia.Shared.UnitTest.Converters;

public class CornerRadiusMixerConverterTests
{
    private readonly CornerRadiusMixerConverter _converter = new();

    public static TheoryData<CornerRadiusPosition, CornerRadius, CornerRadius> PositionTestCases => new()
    {
        {
            CornerRadiusPosition.All,
            new CornerRadius(1, 2, 3, 4),
            new CornerRadius(1, 2, 3, 4)
        },
        {
            CornerRadiusPosition.Bottom | CornerRadiusPosition.TopRight,
            new CornerRadius(10, 20, 30, 40),
            new CornerRadius(0, 20, 30, 40)
        },
        {
            CornerRadiusPosition.TopLeft | CornerRadiusPosition.BottomRight,
            new CornerRadius(5, 6, 7, 8),
            new CornerRadius(5, 0, 7, 0)
        },
        {
            CornerRadiusPosition.Top,
            new CornerRadius(2, 3, 4, 5),
            new CornerRadius(2, 3, 0, 0)
        },
        {
            CornerRadiusPosition.None,
            new CornerRadius(10, 20, 30, 40),
            new CornerRadius(0)
        },
        {
            CornerRadiusPosition.BottomLeft,
            new CornerRadius(100, 200, 300, 400),
            new CornerRadius(0, 0, 0, 400)
        },
        {
            CornerRadiusPosition.Top | CornerRadiusPosition.Left,
            new CornerRadius(6, 7, 8, 9),
            new CornerRadius(6, 7, 0, 9)
        }
    };

    [Theory]
    [MemberData(nameof(PositionTestCases))]
    public void Convert_WithPositionFlags_ReturnsFilteredCornerRadius(
        CornerRadiusPosition position,
        CornerRadius input,
        CornerRadius expected)
    {
        // Arrange
        var converter = new CornerRadiusMixerConverter(position);

        // Act
        var result = converter.Convert(input, typeof(CornerRadius), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(expected, (CornerRadius)result);
    }

    [Fact]
    public void Convert_WithNullValue_ReturnsUnsetValue()
    {
        var result = _converter.Convert(null, typeof(CornerRadius), null, CultureInfo.InvariantCulture);
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void Convert_WithInvalidType_ReturnsUnsetValue()
    {
        var result = _converter.Convert("invalid", typeof(CornerRadius), null, CultureInfo.InvariantCulture);
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