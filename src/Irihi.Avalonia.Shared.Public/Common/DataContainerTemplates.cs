using Avalonia.Collections;
using Irihi.Avalonia.Shared.Contracts;

namespace Irihi.Avalonia.Shared.Common;

public class DataContainerTemplates: AvaloniaList<IDataContainerTemplate>
{
    public DataContainerTemplates()
    {
        ResetBehavior = ResetBehavior.Remove;
    }
}