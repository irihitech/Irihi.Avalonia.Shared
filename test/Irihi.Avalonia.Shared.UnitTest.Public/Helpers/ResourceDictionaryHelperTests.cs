using Avalonia.Controls;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class ResourceDictionaryHelperTests
{
    [Fact]
    public void SetResources_CopiesAllEntries()
    {
        var target = new ResourceDictionary();
        var content = new ResourceDictionary
        {
            ["Key1"] = "Value1",
            ["Key2"] = "Value2",
            ["Key3"] = "Value3"
        };

        Button b = new Button
        {
            Resources = target
        };

        var count = 0;

        b.ResourcesChanged += (_, _) => count++;
        
        target.BulkSetResources(content);

        Assert.Equal("Value1", target["Key1"]);
        Assert.Equal("Value2", target["Key2"]);
        Assert.Equal("Value3", target["Key3"]);
        
        Assert.Equal(1, count);
    }

    [Fact]
    public void SetResources_OverwritesExistingEntries()
    {
        var target = new ResourceDictionary
        {
            ["Key1"] = "OldValue"
        };
        var content = new ResourceDictionary
        {
            ["Key1"] = "NewValue"
        };

        target.BulkSetResources(content);

        Assert.Equal("NewValue", target["Key1"]);
    }

    [Fact]
    public void SetResources_EmptyContent_LeavesTargetUnchanged()
    {
        var target = new ResourceDictionary
        {
            ["Key1"] = "Value1"
        };
        var content = new ResourceDictionary();

        target.BulkSetResources(content);

        Assert.Equal("Value1", target["Key1"]);
        Assert.Single(target);
    }

    [Fact]
    public void SetResources_EmptyTarget_PopulatesFromContent()
    {
        var target = new ResourceDictionary();
        var content = new ResourceDictionary
        {
            ["Key1"] = "Value1"
        };

        target.BulkSetResources(content);

        Assert.Single(target);
        Assert.Equal("Value1", target["Key1"]);
    }

    [Fact]
    public void SetTwice_NotifyTwice()
    {
        Button button = new Button();
        var count = 0;
        button.ResourcesChanged += (_, _) => count++;
        var resource1 = new ResourceDictionary
        {
            ["Key1"] = "Value1",
            ["Key2"] = "Value2"
        };
        var resource2 = new ResourceDictionary
        {
            ["Key3"] = "Value3",
            ["Key4"] = "Value4"
        };
        button.Resources.BulkSetResources(resource1);
        button.Resources.BulkSetResources(resource2);
        Assert.Equal(2, count);
    }
}
