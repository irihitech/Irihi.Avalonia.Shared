using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Irihi.Avalonia.Shared.Helpers;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class DataTemplateCollectionTests
{
    private static Window CreateWindow(out DataTemplateCollectionView view)
    {
        view = new DataTemplateCollectionView();
        var window = new Window { Content = view, Width = 800, Height = 600 };
        window.Show();
        return window;
    }

    [AvaloniaFact]
    public void Xaml_DeclaredCollection_InstantiatesAndHoldsTemplates()
    {
        CreateWindow(out var view);

        var templates = Assert.IsType<DataTemplateCollection>(view.Resources["StringAndIntTemplates"]);
        Assert.Equal(2, templates.Count);
        Assert.True(templates.Match("Hello"));
        Assert.True(templates.Match(42));
        Assert.False(templates.Match(3.14));
        Assert.False(templates.Match(null));
        Assert.NotNull(templates.Build("Hello"));
        Assert.Null(templates.Build(3.14));
    }

    [AvaloniaFact]
    public void ContentTemplate_MatchingStringData_ShowsTextBlock()
    {
        CreateWindow(out var view);

        var textBlock = view.StringContent.GetVisualDescendants().OfType<TextBlock>().FirstOrDefault();
        Assert.NotNull(textBlock);
        Assert.Equal("Hello from XAML", textBlock.Text);
    }

    [AvaloniaFact]
    public void ContentTemplate_SwitchingContent_ReappliesMatchingTemplate()
    {
        CreateWindow(out var view);

        var initialTextBlock = view.IntContent.GetVisualDescendants().OfType<TextBlock>().FirstOrDefault();
        Assert.NotNull(initialTextBlock);
        Assert.Equal("42", initialTextBlock.Text);

        view.IntContent.Content = 42;
        var button = view.IntContent.GetVisualDescendants().OfType<Button>().FirstOrDefault();
        Assert.NotNull(button);
        Assert.Equal(42, button!.Content);
    }

    [AvaloniaFact]
    public void ContentTemplate_NonMatchingData_DoesNotApplyTemplate()
    {
        CreateWindow(out var view);

        view.NoMatchContent.Content = 3.14;

        Assert.Empty(view.NoMatchContent.GetVisualDescendants().OfType<Button>());

        view.NoMatchContent.Content = "Hello";
        var textBlock = view.NoMatchContent.GetVisualDescendants().OfType<TextBlock>()
            .FirstOrDefault(t => t.Text == "Hello");
        Assert.NotNull(textBlock);
    }

    [AvaloniaFact]
    public void ItemTemplate_MixedDataTypes_AppliesMatchingTemplatePerItem()
    {
        CreateWindow(out var view);

        view.MixedItems.ItemsSource = new object[] { "Text item", 42 };
        Dispatcher.UIThread.RunJobs();

        var textBlock = view.MixedItems.GetVisualDescendants().OfType<TextBlock>()
            .FirstOrDefault(t => t.Text == "Text item");
        Assert.NotNull(textBlock);

        var button = view.MixedItems.GetVisualDescendants().OfType<Button>().FirstOrDefault();
        Assert.NotNull(button);
        Assert.Equal(42, button!.Content);
    }

    [AvaloniaFact]
    public void ContentTemplate_InlineXamlDeclaration_Works()
    {
        CreateWindow(out var view);

        view.InlineContent.Content = "Hello";
        var border = view.InlineContent.GetVisualDescendants().OfType<Border>().FirstOrDefault();
        Assert.NotNull(border);
    }
}
