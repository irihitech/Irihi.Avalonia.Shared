using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class SelectionDataTemplateSelectorTests
{
    private class TestData
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    private class TestTemplate : IDataTemplate
    {
        public string Name { get; }
        public Func<object?, bool> MatchPredicate { get; }

        public TestTemplate(string name, Func<object?, bool> matchPredicate)
        {
            Name = name;
            MatchPredicate = matchPredicate;
        }

        public bool Match(object? data) => MatchPredicate(data);

        public Control? Build(object? data)
        {
            var textBlock = new TextBlock { Text = $"{Name}: {data}" };
            return textBlock;
        }
    }

    [Fact]
    public void Constructor_Should_Initialize_Properties()
    {
        var selector = new SelectionDataTemplateSelector();
        
        Assert.Null(selector.SelectedItemTemplate);
        Assert.Null(selector.ItemTemplate);
    }

    [Fact]
    public void Match_Should_Return_False_When_No_Templates_Set()
    {
        var selector = new SelectionDataTemplateSelector();
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Match(data);
        
        Assert.False(result);
    }

    [Fact]
    public void Match_Should_Return_True_When_SelectedItemTemplate_Matches()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Match(data);
        
        Assert.True(result);
    }

    [Fact]
    public void Match_Should_Return_True_When_ItemTemplate_Matches()
    {
        var itemTemplate = new TestTemplate("Item", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Match(data);
        
        Assert.True(result);
    }

    [Fact]
    public void Match_Should_Return_True_When_Either_Template_Matches()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => false);
        var itemTemplate = new TestTemplate("Item", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate,
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Match(data);
        
        Assert.True(result);
    }

    [Fact]
    public void Match_Should_Return_False_When_Neither_Template_Matches()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => false);
        var itemTemplate = new TestTemplate("Item", _ => false);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate,
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Match(data);
        
        Assert.False(result);
    }

    [Fact]
    public void Build_Should_Return_Null_When_No_Templates_Set()
    {
        var selector = new SelectionDataTemplateSelector();
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Build(data);
        
        Assert.Null(result);
    }

    [Fact]
    public void Build_Should_Use_SelectedItemTemplate_When_Available_And_Matches()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => true);
        var itemTemplate = new TestTemplate("Item", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate,
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Build(data) as TextBlock;
        
        Assert.NotNull(result);
        Assert.StartsWith("Selected:", result.Text);
    }

    [Fact]
    public void Build_Should_Fallback_To_ItemTemplate_When_SelectedItemTemplate_Does_Not_Match()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => false);
        var itemTemplate = new TestTemplate("Item", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate,
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Build(data) as TextBlock;
        
        Assert.NotNull(result);
        Assert.StartsWith("Item:", result.Text);
    }

    [Fact]
    public void Build_Should_Use_ItemTemplate_When_Only_ItemTemplate_Available()
    {
        var itemTemplate = new TestTemplate("Item", _ => true);
        var selector = new SelectionDataTemplateSelector
        {
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Build(data) as TextBlock;
        
        Assert.NotNull(result);
        Assert.StartsWith("Item:", result.Text);
    }

    [Fact]
    public void Build_Should_Return_Null_When_Neither_Template_Matches()
    {
        var selectedTemplate = new TestTemplate("Selected", _ => false);
        var itemTemplate = new TestTemplate("Item", _ => false);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate,
            ItemTemplate = itemTemplate
        };
        var data = new TestData { Name = "Test", Value = 42 };
        
        var result = selector.Build(data);
        
        Assert.Null(result);
    }

    [Fact]
    public void Build_Should_Handle_Null_Data()
    {
        var selectedTemplate = new TestTemplate("Selected", data => data == null);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate
        };
        
        var result = selector.Build(null) as TextBlock;
        
        Assert.NotNull(result);
        Assert.StartsWith("Selected:", result.Text);
    }

    [Fact]
    public void Match_Should_Handle_Null_Data()
    {
        var selectedTemplate = new TestTemplate("Selected", data => data == null);
        var selector = new SelectionDataTemplateSelector
        {
            SelectedItemTemplate = selectedTemplate
        };
        
        var result = selector.Match(null);
        
        Assert.True(result);
    }
}