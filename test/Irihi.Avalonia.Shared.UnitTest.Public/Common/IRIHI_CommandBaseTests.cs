namespace Irihi.Avalonia.Shared.Common.Tests;

public class IRIHI_CommandBaseTests
{
    [Fact]
    public void ExecuteInvokesAction()
    {
        bool executed = false;
        var command = new IRIHI_CommandBase(() => executed = true);

        command.Execute(null);

        Assert.True(executed);
    }

    [Fact]
    public void CanExecuteReturnsTrueByDefault()
    {
        var command = new IRIHI_CommandBase(() => { });

        bool canExecute = command.CanExecute(null);

        Assert.True(canExecute);
    }

    [Fact]
    public void CanExecuteReturnsFalseWhenSpecified()
    {
        var command = new IRIHI_CommandBase(() => { }, () => false);

        bool canExecute = command.CanExecute(null);

        Assert.False(canExecute);
    }

    [Fact]
    public void NotifyCanExecuteChangedRaisesEvent()
    {
        var command = new IRIHI_CommandBase(() => { });
        bool eventRaised = false;
        command.CanExecuteChanged += (_, _) => eventRaised = true;

        command.NotifyCanExecuteChanged();

        Assert.True(eventRaised);
    }
}