using System;

namespace Irihi.Avalonia.Shared.Contracts;

public interface IDialogContext
{
    public void Close();
    public event EventHandler<object?>? RequestClose;
}