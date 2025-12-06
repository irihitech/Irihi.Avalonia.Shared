using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;

namespace Irihi.Avalonia.Shared.Contracts;

public interface IDataContainerTemplateHost
{
    //IDataTemplateHost
    ContentPresenter ContentPresenter { get; }
}