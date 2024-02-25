namespace Irihi.Avalonia.Shared.Reactive;

public class ResultDisposable(IDisposable? disposable, bool result) : IDisposable
{
    public bool Result { get; } = result;

    public void Dispose()
    {
        disposable?.Dispose();
    }
}

internal class EmptyDisposable : IDisposable
{
    public void Dispose()
    {
        
    }
}