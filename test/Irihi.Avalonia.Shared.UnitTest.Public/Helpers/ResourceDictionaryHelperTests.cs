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
            ["Key2"] = "Value2"
        };

        ResourceDictionaryHelper.SetResources(target, content);

        Assert.Equal("Value1", target["Key1"]);
        Assert.Equal("Value2", target["Key2"]);
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
        Assert.Equal(1, target.Count);
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

        Assert.Equal(1, target.Count);
        Assert.Equal("Value1", target["Key1"]);
    }

    [Fact]
    public void SetResources_NullTarget_ThrowsArgumentNullException()
    {
        var content = new ResourceDictionary();
        Assert.Throws<ArgumentNullException>(() => ResourceDictionaryHelper.SetResources(null!, content));
    }

    [Fact]
    public void SetResources_NullContent_ThrowsArgumentNullException()
    {
        var target = new ResourceDictionary();
        Assert.Throws<ArgumentNullException>(() => ResourceDictionaryHelper.SetResources(target, null!));
    }
}
