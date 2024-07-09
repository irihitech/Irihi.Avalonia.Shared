using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Irihi.Avalonia.Shared.Common;

/// <summary>
/// This is a ICommand implementation for internal use, to avoid irrelevant dependencies.
/// This should not be used by your application.
/// </summary>
public class IRIHI_CommandBase<T>: ICommand
{
    private readonly Action<T?> _execute;
    private readonly Predicate<T?>? _canExecute;
    public event EventHandler? CanExecuteChanged;

    public IRIHI_CommandBase(Action<T?> execute)
    {
        _execute = execute;
    }

    public IRIHI_CommandBase(Action<T?> execute, Predicate<T?> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CanExecute(T? parameter)
    {
        return _canExecute is null || _canExecute.Invoke(parameter);
    }
    
    public bool CanExecute(object parameter)
    {
        if (!TryGetCommandArgument(parameter, out var result))
        {
            throw new ArgumentException();
        }
        return CanExecute(result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(T? parameter)
    {
        _execute(parameter);
    }

    public void Execute(object parameter)
    {
        if (!TryGetCommandArgument(parameter, out var result))
        {
            throw new ArgumentException();
        }
        Execute(result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool TryGetCommandArgument(object? parameter, out T? result)
    {
        if (parameter is null && default(T) is null)
        {
            result = default(T);
            return true;
        }
        if (parameter is T obj)
        {
            result = obj;
            return true;
        }
        result = default (T);
        return false;
    }
    
    public void NotifyCanExecuteChanged()
    {
        EventHandler canExecuteChanged = this.CanExecuteChanged;
        if (canExecuteChanged is null)
            return;
        canExecuteChanged((object) this, EventArgs.Empty);
    }
}