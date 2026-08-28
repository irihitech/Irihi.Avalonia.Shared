using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Collections;

namespace Irihi.Avalonia.Shared.Helpers;

public class ClassHelper
{
    public static readonly AttachedProperty<string> ClassesProperty =
        AvaloniaProperty.RegisterAttached<ClassHelper, StyledElement, string>("Classes");

    public static readonly AttachedProperty<StyledElement> ClassSourceProperty =
        AvaloniaProperty.RegisterAttached<ClassHelper, StyledElement, StyledElement>("ClassSource");

    private static readonly ConditionalWeakTable<StyledElement, SourceSubscription> _subscriptions = new();

    static ClassHelper()
    {
        ClassesProperty.Changed.AddClassHandler<StyledElement>(OnClassesChanged);
        ClassSourceProperty.Changed.AddClassHandler<StyledElement>(OnClassSourceChanged);
    }

    private static void OnClassSourceChanged(StyledElement arg1, AvaloniaPropertyChangedEventArgs arg2)
    {
        // Unsubscribe from old source
        if (_subscriptions.TryGetValue(arg1, out var old))
        {
            old.Source.Classes.CollectionChanged -= old.Handler;
            _subscriptions.Remove(arg1);
        }

        if (arg2.NewValue is not StyledElement styledElement) return;
        arg1.Classes.Clear();
        var nonPseudoClasses = styledElement.Classes.Where(c => !c.StartsWith(':'));
        arg1.Classes.AddRange(nonPseudoClasses);

        var weakTarget = new WeakReference<StyledElement>(arg1);
        NotifyCollectionChangedEventHandler handler = null!;
        handler = (o, e) =>
        {
            if (weakTarget.TryGetTarget(out var target))
            {
                OnSourceClassesChanged(o, e, target);
            }
            else
            {
                // Target has been GC'd; unsubscribe to prevent further callbacks
                if (o is INotifyCollectionChanged collection)
                    collection.CollectionChanged -= handler;
            }
        };
        styledElement.Classes.CollectionChanged += handler;
        _subscriptions.Add(arg1, new SourceSubscription(styledElement, handler));
    }

    private sealed class SourceSubscription(StyledElement source, NotifyCollectionChangedEventHandler handler)
    {
        public StyledElement Source { get; } = source;
        public NotifyCollectionChangedEventHandler Handler { get; } = handler;
    }

    private static void OnSourceClassesChanged(object? sender, NotifyCollectionChangedEventArgs e, StyledElement target)
    {
        if (sender is not AvaloniaList<string> classes) return;
        target.Classes.Clear();
        var nonPseudoClasses = classes.Where(c => !c.StartsWith(':'));
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