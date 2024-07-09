using System;
using Xunit;

namespace Irihi.Avalonia.Shared.Common.Tests;

public class IRIHI_CommandBaseGenericTests
{
    [Fact]
    public void ExecuteInvokesActionWithParameter()
    {
        int executedParam = 0;
        var command = new IRIHI_CommandBase<int>(param => executedParam = param);

        command.Execute(5);

        Assert.Equal(5, executedParam);
    }

    [Fact]
    public void CanExecuteReturnsTrueByDefault()
    {
        var command = new IRIHI_CommandBase<object>(_ => { });

        bool canExecute = command.CanExecute(null);

        Assert.True(canExecute);
    }

    [Fact]
    public void CanExecuteReturnsFalseWhenPredicateSpecifiedAndReturnsFalse()
    {
        var command = new IRIHI_CommandBase<int>(_ => { }, _ => false);

        bool canExecute = command.CanExecute(0);

        Assert.False(canExecute);
    }

    [Fact]
    public void CanExecuteThrowsArgumentExceptionForInvalidParameterType()
    {
        var command = new IRIHI_CommandBase<int>(_ => { });

        Assert.Throws<ArgumentException>(() => command.CanExecute("invalid"));
    }

    [Fact]
    public void ExecuteThrowsArgumentExceptionForInvalidParameterType()
    {
        var command = new IRIHI_CommandBase<int>(_ => { });

        Assert.Throws<ArgumentException>(() => command.Execute("invalid"));
    }

    [Fact]
    public void NotifyCanExecuteChangedRaisesEvent()
    {
        var command = new IRIHI_CommandBase<object>(_ => { });
        bool eventRaised = false;
        command.CanExecuteChanged += (sender, e) => eventRaised = true;

        command.NotifyCanExecuteChanged();

        Assert.True(eventRaised);
    }
    
    [Fact]
    public void CanExecuteWithValidParameterTypeReturnsTrue()
    {
        var command = new IRIHI_CommandBase<int>(_ => { }, _ => true);
        bool canExecute = command.CanExecute(1);
        Assert.True(canExecute);
    }

    [Fact]
    public void ExecuteWithValidParameterInvokesAction()
    {
        int executedParam = 0;
        var command = new IRIHI_CommandBase<int>(param => executedParam = param);
        command.Execute(10);
        Assert.Equal(10, executedParam);
    }

    [Fact]
    public void UnsubscribingFromCanExecuteChangedEventWorks()
    {
        var command = new IRIHI_CommandBase<object>(_ => { });
        bool eventRaised = false;

        EventHandler handler = (sender, e) => eventRaised = true;
        command.CanExecuteChanged += handler;
        command.CanExecuteChanged -= handler;

        command.NotifyCanExecuteChanged();
        Assert.False(eventRaised);
    }
    
    [Fact]
    public void CanExecuteWithRightTypeReturnsTrue()
    {
        object o = 1;
        var command = new IRIHI_CommandBase<int>(_ => { });
        bool canExecute = command.CanExecute(o);
        Assert.True(canExecute);
    }
    
    [Fact]
    public void ExecuteWithRightTypeInvokesAction()
    {
        object o = 1;
        int executedParam = 0;
        var command = new IRIHI_CommandBase<int>(param => executedParam = param);
        command.Execute(o);
        Assert.Equal(1, executedParam);
    }
}