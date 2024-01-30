namespace Irihi.Avalonia.Shared.Attributes;

public class DesignerItemTypeAttribute: Attribute
{
    public DesignerItemType Type { get; }
    
    public DesignerItemTypeAttribute(DesignerItemType type)
    {
        Type = type;
    }
}