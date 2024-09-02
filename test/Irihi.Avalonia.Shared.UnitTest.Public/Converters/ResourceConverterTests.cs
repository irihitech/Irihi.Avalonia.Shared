using System.Globalization;
using Avalonia;
using Avalonia.Media;
using Irihi.Avalonia.Shared.Converters;
using Irihi.Avalonia.Shared.UnitTest.Mocks;

// Ensure this namespace matches your actual namespace
namespace Irihi.Avalonia.Shared.UnitTest.Converters;

public class ResourceConverterTests
{
    private readonly ResourceConverter _converter = new();

    [Fact]
    public void Convert_WithNullValue_ReturnsAvaloniaPropertyUnsetValue()
    {
        // Arrange
        object? value = null;
        var targetType = typeof(object); // Or a specific type if applicable
        object? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void Convert_WithExistingResource_ReturnsResource()
    {
        // Arrange
        // Assuming you would add a known resource to your converter for testing purposes
        _converter["TestResource"] = "TestValue";
        object value = "TestResource";
        var targetType = typeof(string); // Assuming the target type you expect
        object? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.Equal("TestValue", result);
    }

    [Fact]
    public void Convert_WithNonExistentResource_ReturnsAvaloniaPropertyUnsetValue()
    {
        // Arrange
        object value = "NonExistentResource";
        var targetType = typeof(string); // Or the expected type
        object? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = _converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    // Note: The ConvertBack method is not tested since it's not implemented.
    // If it gets implemented in the future, similar test cases should be written for it.
    
    [Fact]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        // Arrange
        object? value = null;
        var targetType = typeof(object); // Or a specific type if applicable
        object? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(value, targetType, parameter, culture));
    }
    
    [Fact]
    public void ConvertFromDerivedClass_Success()
    {
        // Arrange
        var derivedConverter = new MockResourceConverter();
        object value = "Red";
        var targetType = typeof(IBrush); // Assuming the target type you expect
        object? parameter = null;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = derivedConverter.Convert(value, targetType, parameter, culture);
    
        // Assert
        Assert.Equal(Brushes.Red.Color, (result as ISolidColorBrush)?.Color);
    }
}