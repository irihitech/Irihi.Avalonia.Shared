using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class AffectsPseudoClassTests
{
    private class Sample : Control
    {
        public static readonly StyledProperty<bool> TestProperty = AvaloniaProperty.Register<Sample, bool>(
            nameof(Test));

        public bool Test
        {
            get => GetValue(TestProperty);
            set => SetValue(TestProperty, value);
        }

        public static readonly RoutedEvent<RoutedEventArgs> TestChangedEvent =
            RoutedEvent.Register<Sample, RoutedEventArgs>(nameof(TestChanged), RoutingStrategies.Bubble);
        
        public event EventHandler<RoutedEventArgs> TestChanged
        {
            add => AddHandler(TestChangedEvent, value);
            remove => RemoveHandler(TestChangedEvent, value);
        }
        
        static Sample()
        {
            TestProperty.AffectsPseudoClass<Sample, RoutedEventArgs>(":test", TestChangedEvent); 
        }
    }
    
    [Fact]
    public void TestAffectsPseudoClass()
    {
        Sample sample = new();
        bool flag = false;
        sample.TestChanged += (_, _) => flag = true;
        sample.Test = true;
        Assert.Contains(sample.Classes, x => x == ":test");
        Assert.True(flag);
    }
}