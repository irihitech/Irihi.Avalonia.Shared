using System.Collections.Specialized;
using Avalonia;
using Avalonia.Collections;

namespace Irihi.Avalonia.Shared.Helpers;

public class ClassHelper
{
    public static readonly AttachedProperty<string> ClassesProperty =
        AvaloniaProperty.RegisterAttached<ClassHelper, StyledElement, string>("Classes");

    public static readonly AttachedProperty<StyledElement> ClassSourceProperty =
        AvaloniaProperty.RegisterAttached<ClassHelper, StyledElement, StyledElement>("ClassSource");

    static ClassHelper()
    {
        ClassesProperty.Changed.AddClassHandler<StyledElement>(OnClassesChanged);
        ClassSourceProperty.Changed.AddClassHandler<StyledElement>(OnClassSourceChanged);
    }

    private static void OnClassSourceChanged(StyledElement arg1, AvaloniaPropertyChangedEventArgs arg2)
    {
        if (arg2.NewValue is not StyledElement styledElement) return;
        arg1.Classes.Clear();
        var nonPseudoClasses = styledElement.Classes.Where(c => !c.StartsWith(":"));
        arg1.Classes.AddRange(nonPseudoClasses);
        styledElement.Classes.WeakSubscribe((o, e) => OnSourceClassesChanged(o, e, arg1));
    }

    private static void OnSourceClassesChanged(object sender, NotifyCollectionChangedEventArgs e, StyledElement target)
    {
        if (sender is not AvaloniaList<string> classes) return;
        target.Classes.Clear();
        var nonPseudoClasses = classes.Where(c => !c.StartsWith(":"));
        target.Classes.AddRange(nonPseudoClasses);
    }

    public static void SetClasses(AvaloniaObject obj, string value)
    {
        obj.SetValue(ClassesProperty, value);
    }

    public static string GetClasses(AvaloniaObject obj)
    {
        return obj.GetValue(ClassesProperty);
    }

    private static void OnClassesChanged(StyledElement sender, AvaloniaPropertyChangedEventArgs value)
    {
        var @class = value.GetNewValue<string?>();
        if (@class is null) return;
        sender.Classes.Clear();
        var classes = @class.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        sender.Classes.AddRange(classes);
    }

    public static void SetClassSource(StyledElement obj, StyledElement value)
    {
        obj.SetValue(ClassSourceProperty, value);
    }

    public static StyledElement GetClassSource(StyledElement obj)
    {
        return obj.GetValue(ClassSourceProperty);
    }
}