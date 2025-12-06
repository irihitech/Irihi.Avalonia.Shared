using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Irihi.Avalonia.Shared.Contracts;

/// <summary>
/// This interface is designed as a generator for a container to hold data generated view.
/// <para>For IDataContainerTemplate supported controls, data is used twice to generate</para>
/// <para>1. Use IDataTemplate to generate data view. </para>
/// <para>2. Use IDataContainerTemplate to generate container to hold 1. </para>
/// <para>Then set 2 as 1's content. </para>
/// </summary>
public interface IDataContainerTemplate: ITemplate<object?, ContentControl>
{
    /// <summary>
    ///  Determines whether this template matches the provided data.
    /// </summary>
    /// <param name="data"> The data object to evaluate.</param>
    /// <returns> True if the template matches the data; otherwise, false.</returns>
    bool Match(object? data);
}