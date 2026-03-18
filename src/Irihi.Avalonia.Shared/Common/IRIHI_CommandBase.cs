using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Irihi.Avalonia.Shared.Common;

/// <summary>
/// This is a ICommand implementation for internal use, to avoid irrelevant dependencies.
/// This should not be used by your application.
/// </summary>
public class IRIHI_CommandBase: ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;
    public event EventHandler? CanExecuteChanged;

    public IRIHI_CommandBase(Action execute)
    {
        _execute = execute;
    }
    
    public IRIHI_CommandBase(Action execute, Func<bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanExecute(object? parameter)
    {
        return _canExecute is null || _canExecute.Invoke();
    }

    public void Execute(object? parameter)
    {
        _execute.Invoke();
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}