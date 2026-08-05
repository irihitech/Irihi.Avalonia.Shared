using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Irihi.Avalonia.Shared.Helpers;

public class DataTemplateCollection : DataTemplates, IDataTemplate
{
    public Control? Build(object? param) => this.FirstOrDefault(template => template.Match(param))?.Build(param);

    public bool Match(object? data) => this.Any(template => template.Match(data));
}