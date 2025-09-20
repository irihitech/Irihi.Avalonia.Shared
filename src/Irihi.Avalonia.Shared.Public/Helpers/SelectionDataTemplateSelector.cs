using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Irihi.Avalonia.Shared.Helpers;

/// <summary>
/// A data template selector that provides template selection logic for ComboBox-like controls.
/// First tries to use SelectedItemTemplate, then falls back to ItemTemplate, then returns null.
/// </summary>
public class SelectionDataTemplateSelector : IDataTemplate
{
    /// <summary>
    /// The template to use for the selected item when it's displayed in the selection box.
    /// </summary>
    public IDataTemplate? SelectedItemTemplate { get; set; }
    
    /// <summary>
    /// The fallback template to use for items when SelectedItemTemplate is not available or doesn't match.
    /// </summary>
    public IDataTemplate? ItemTemplate { get; set; }

    /// <summary>
    /// Determines whether this template can be used for the specified data object.
    /// Returns true if either SelectedItemTemplate or ItemTemplate can match the data.
    /// </summary>
    /// <param name="data">The data object to check.</param>
    /// <returns>True if a template can be used for the data, false otherwise.</returns>
    public bool Match(object? data)
    {
        return (SelectedItemTemplate?.Match(data) == true) || (ItemTemplate?.Match(data) == true);
    }

    /// <summary>
    /// Builds the control for the specified data object.
    /// First tries SelectedItemTemplate, then ItemTemplate, then returns null.
    /// </summary>
    /// <param name="data">The data object to build the control for.</param>
    /// <returns>The built control, or null if no template matches.</returns>
    public Control? Build(object? data)
    {
        // First try the SelectedItemTemplate
        if (SelectedItemTemplate?.Match(data) == true)
        {
            return SelectedItemTemplate.Build(data);
        }
        
        // Fall back to ItemTemplate
        if (ItemTemplate?.Match(data) == true)
        {
            return ItemTemplate.Build(data);
        }
        
        // No template available
        return null;
    }
}