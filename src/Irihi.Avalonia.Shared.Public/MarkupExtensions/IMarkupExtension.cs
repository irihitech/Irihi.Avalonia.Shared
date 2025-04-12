namespace Irihi.Avalonia.Shared.MarkupExtensions;

public interface IMarkupExtension<out TReturn>
{
    public TReturn ProvideValue(IServiceProvider serviceProvider);
}

public interface IMarkupExtension : IMarkupExtension<object>;