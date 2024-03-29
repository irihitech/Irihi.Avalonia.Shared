﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class PropertyPseudoClassHelperTest
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
            RoutedEvent.Register<Sample, RoutedEventArgs>("", RoutingStrategies.Bubble);
        
        public event EventHandler<RoutedEventArgs> TestChanged
        {
            add => AddHandler(TestChangedEvent, value);
            remove => RemoveHandler(TestChangedEvent, value);
        }
    }

    private class ValidSample : Sample
    {
        static ValidSample()
        {
            TestProperty.AffectsPseudoClass<ValidSample>(":test"); 
        }
    }

    private class ValidEventSample : Sample
    {
        static ValidEventSample()
        {
            TestProperty.AffectsPseudoClass<ValidEventSample>(":test", TestChangedEvent); 
        }
    }
    
    [Fact]
    public void Mixin_Attached_PseudoClass_Success()
    {
        ValidSample sample = new();
        sample.Test = true;
        Assert.Contains(sample.Classes, x => x == ":test");
    }
    
    [Fact]
    public void Mixin_Unattached_PseudoClass_Success()
    {
        Sample sample = new();
        sample.Test = true;
        Assert.DoesNotContain(sample.Classes, x => x == ":test");
    }

    [Fact]
    public void Mixin_Attached_EventInvoke_Success()
    {
        ValidEventSample sample = new();
        bool flag = false;
        sample.AddHandler(Sample.TestChangedEvent, (_, _) => flag = true);
        sample.Test = true;
        Assert.True(flag);
    }
    
    [Fact]
    public void Mixin_Unattached_EventInvoke_Success()
    {
        Sample sample = new();
        bool flag = false;
        sample.AddHandler(Sample.TestChangedEvent, (_, _) => flag = true);
        sample.Test = true;
        Assert.False(flag);
        ValidSample sample2 = new();
        flag = false;
        sample2.AddHandler(Sample.TestChangedEvent, (_, _) => flag = true);
        sample2.Test = true;
        Assert.False(flag);
    }
}