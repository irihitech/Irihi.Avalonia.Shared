using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xunit;

namespace Irihi.Avalonia.Shared.Common.Tests;

public class IRIHI_ObservableBaseTests
{
    private class TestObservable : IRIHI_ObservableBase
    {
        internal string _testProperty;

        public string TestProperty
        {
            get => _testProperty;
            set => SetProperty(ref _testProperty, value);
        }

        public TestObservable()
        {
            _testProperty = string.Empty;
        }
        
        public new bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            return base.SetProperty(ref storage, value, propertyName);
        }
    }

    [Fact]
    public void PropertyChangedEventFiresOnPropertyChange()
    {
        var testObject = new TestObservable();
        bool eventFired = false;
        testObject.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(TestObservable.TestProperty))
                eventFired = true;
        };

        testObject.TestProperty = "New Value";

        Assert.True(eventFired);
    }

    [Fact]
    public void PropertyChangedEventDoesNotFireWhenValueIsUnchanged()
    {
        var testObject = new TestObservable();
        testObject.TestProperty = "Initial Value";
        bool eventFired = false;
        testObject.PropertyChanged += (sender, e) => eventFired = true;

        testObject.TestProperty = "Initial Value";

        Assert.False(eventFired);
    }

    [Fact]
    public void SetPropertyReturnsTrueWhenValueChanges()
    {
        var testObject = new TestObservable();
        bool result = testObject.SetProperty(ref testObject._testProperty, "New Value");

        Assert.True(result);
    }

    [Fact]
    public void SetPropertyReturnsFalseWhenValueIsUnchanged()
    {
        var testObject = new TestObservable();
        testObject.TestProperty = "Initial Value";
        bool result = testObject.SetProperty(ref testObject._testProperty, "Initial Value");

        Assert.False(result);
    }
}