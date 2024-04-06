using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class AffectsPseudoClassTests
{
    [Fact]
    public void TestAffectsPseudoClass()
    {
        // Arrange
        var button = new Button();
        var property = AvaloniaProperty.Register<Button, bool>("TestProperty");
        bool flag = false;
        button.Click += (sender, e) => flag = true;

        // Act
        property.AffectsPseudoClass<Button, RoutedEventArgs>(pseudoClass: "test", Button.ClickEvent);
        property.SetValue(true, button);
        
        // Assert
        Assert.Contains("test", button.Classes);
        Assert.True(flag);
    }
}