using Avalonia.Controls;
using Avalonia.Styling;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class ClassHelperTests
{
    [Fact]
    public void ClassesProperty_Should_Set_And_Get_Value()
    {
        var control = new Button();
        ClassHelper.SetClasses(control, "test-class");
        Assert.Equal("test-class", ClassHelper.GetClasses(control));
    }

    [Fact]
    public void ClassSourceProperty_Should_Set_And_Get_Value()
    {
        var sourceControl = new Button();
        var targetControl = new Button();
        ClassHelper.SetClassSource(targetControl, sourceControl);
        Assert.Equal(sourceControl, ClassHelper.GetClassSource(targetControl));
    }

    [Fact]
    public void OnClassSourceChanged_Should_Copy_Classes()
    {
        var sourceControl = new Button();
        sourceControl.Classes.Add("source-class");
        var targetControl = new Button();
        ClassHelper.SetClassSource(targetControl, sourceControl);
        Assert.Contains("source-class", targetControl.Classes);
    }

    [Fact]
    public void OnClassSourceChanged_Should_Not_Copy_PseudoClasses()
    {
        var sourceControl = new Button();
        IPseudoClasses pseudoClasses = sourceControl.Classes;
        pseudoClasses.Add(":pseudo-class");
        var targetControl = new Button();
        ClassHelper.SetClassSource(targetControl, sourceControl);
        Assert.DoesNotContain(":pseudo-class", targetControl.Classes);
    }

    [Fact]
    public void OnClassesChanged_Should_Update_Classes()
    {
        var control = new Button();
        ClassHelper.SetClasses(control, "initial-class");
        ClassHelper.SetClasses(control, "updated-class");
        Assert.Contains("updated-class", control.Classes);
        Assert.DoesNotContain("initial-class", control.Classes);
    }

    [Fact]
    public void OnSourceClassesChanged_Should_Update_Target_Classes()
    {
        var sourceControl = new Button();
        var targetControl = new Button();
        ClassHelper.SetClassSource(targetControl, sourceControl);
        sourceControl.Classes.Add("new-class");
        Assert.Contains("new-class", targetControl.Classes);
    }

    [Fact]
    public void OnClassesChanged_Should_Not_Remove_PseudoClasses()
    {
        var control = new Button();
        IPseudoClasses pseudoClasses = control.Classes;
        pseudoClasses.Add(":pseudo-class");
        pseudoClasses.Add(":pseudo-class2");
        ClassHelper.SetClasses(control, "initial-class");
        Assert.Contains(":pseudo-class", control.Classes);
        Assert.Contains(":pseudo-class2", control.Classes);
    }
    
    [Fact]
    public void ClassesProperty_Should_Not_Throw_Exception_When_Null()
    {
        var control = new Button();
        ClassHelper.SetClasses(control, null);
        Assert.Null(ClassHelper.GetClasses(control));
    }
}