using Avalonia;
using Avalonia.Headless;
using Avalonia.Markup.Xaml;
using Irihi.Avalonia.Shared.HeadlessTest;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}