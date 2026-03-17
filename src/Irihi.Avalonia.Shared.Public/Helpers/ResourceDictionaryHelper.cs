using Avalonia.Controls;

namespace Irihi.Avalonia.Shared.Helpers;

public static class ResourceDictionaryHelper
{
    /// <summary>
    /// Copies all entries from <paramref name="content"/> into <paramref name="target"/>,
    /// overwriting any existing entries with the same key.
    /// </summary>
    /// <param name="target">The resource dictionary to update.</param>
    /// <param name="content">The resource dictionary whose entries are copied into <paramref name="target"/>.</param>
    public static void SetResources(IResourceDictionary target, IResourceDictionary content)
    {
        if (target is null) throw new ArgumentNullException(nameof(target));
        if (content is null) throw new ArgumentNullException(nameof(content));
        foreach (var kv in content)
        {
            target[kv.Key] = kv.Value;
        }
    }
}
