using Avalonia;
using Avalonia.Controls;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class ObservableExtensionTest
{
    [Fact]
    public void Subscribe_Action_Success()
    {
        bool called = false;
        var textBlock = new TextBlock();
        IObservable<string?> observable = textBlock.GetObservable(TextBlock.TextProperty);
        IDisposable disposable = observable.Subscribe(i => called = true);
        textBlock.Text = "Hello, World!";
        disposable.Dispose();
        Assert.True(called);
    }
}