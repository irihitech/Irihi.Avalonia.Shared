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
        
        ResourceDictionaryHelper.SetResources(target, content);

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

        ResourceDictionaryHelper.SetResources(target, content);

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

        ResourceDictionaryHelper.SetResources(target, content);

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

        ResourceDictionaryHelper.SetResources(target, content);

        Assert.Single(target);
        Assert.Equal("Value1", target["Key1"]);
    }
}
