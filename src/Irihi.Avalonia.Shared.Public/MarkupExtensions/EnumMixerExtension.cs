using System.Collections;
using Avalonia.Metadata;

namespace Irihi.Avalonia.Shared.MarkupExtensions;

public class EnumMixerExtension(Type type) : IMarkupExtension<IList>
{
    [Content] public Type Type { get; set; } = type;

    public IList ProvideValue(IServiceProvider _)
    {
        if (Type.IsEnum)
        {
#if NET
            return Enum.GetValuesAsUnderlyingType(Type);
#else
            return Enum.GetValues(Type);
#endif
        }

        return new ArrayList();
    }
}