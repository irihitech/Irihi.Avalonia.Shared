using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Irihi.Avalonia.Shared.Common;

/// <summary>
/// This is a INotifyPropertyChanged and INotifyPropertyChanging implementation for internal use, to avoid irrelevant dependencies.
/// This should not be used by your application. 
/// </summary>
public class IRIHI_ObservableBase: INotifyPropertyChanged, INotifyPropertyChanging
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;
    
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged ?.Invoke(this, e);
    }
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
            return false;
        field = newValue;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}