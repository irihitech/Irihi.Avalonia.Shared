namespace Irihi.Avalonia.Shared.Contracts;

public interface IUndoRedoControl
{
    int CountOfHistoricalRecord { get; set; }
    public void Undo();
    public void Redo();
}