using Avalonia;
using Avalonia.Controls;
using Avalonia.Reactive;

namespace Irihi.Avalonia.Shared.Helpers;

public static class ObservableExtension
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> action)
    {
        return observable.Subscribe(new AnonymousObserver<T>(action));
    }
}