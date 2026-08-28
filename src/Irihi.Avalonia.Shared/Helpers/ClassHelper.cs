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
            old.Dispose();
            _subscriptions.Remove(arg1);
        }

        if (arg2.NewValue is not StyledElement styledElement) return;
        arg1.Classes.Clear();
        var nonPseudoClasses = styledElement.Classes.Where(c => !c.StartsWith(':'));
        arg1.Classes.AddRange(nonPseudoClasses);

        var subscription = new SourceSubscription(styledElement, arg1);
        _subscriptions.Add(arg1, subscription);
    }

    /// <summary>
    /// Holds the event subscription between a source and a target.
    /// The target is held weakly so that it can be GC'd independently of the source.
    /// When the target is GC'd, the subscription unregisters itself on the next source
    /// class-change event via its finalizer cleanup path.
    /// </summary>
    private sealed class SourceSubscription : IDisposable
    {
        private readonly WeakReference<StyledElement> _weakTarget;
        private readonly NotifyCollectionChangedEventHandler _handler;
        public INotifyCollectionChanged Source { get; }

        public SourceSubscription(StyledElement source, StyledElement target)
        {
            Source = source.Classes;
            _weakTarget = new WeakReference<StyledElement>(target);
            _handler = OnSourceClassesChanged;
            source.Classes.CollectionChanged += _handler;
        }

        private void OnSourceClassesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (_weakTarget.TryGetTarget(out var target))
            {
                ClassHelper.OnSourceClassesChanged(sender, e, target);
            }
            else
            {
                // Target has been GC'd; clean up this dangling subscription
                Dispose();
            }
        }

        public void Dispose()
        {
            Source.CollectionChanged -= _handler;
        }
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