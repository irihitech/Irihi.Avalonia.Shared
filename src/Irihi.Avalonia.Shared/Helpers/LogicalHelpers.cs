using Avalonia.LogicalTree;

namespace Irihi.Avalonia.Shared.Helpers;

public static class LogicalHelpers
{
    public static int CalculateDistanceFromLogicalParent<T>(this ILogical logical, int @default = -1) where T: ILogical
    {
        int distance = @default;
        ILogical? parent = logical;
        while (parent is not null)
        {
            if (parent is T) return distance;
            parent = parent.LogicalParent;
            distance++;
        }
        return @default;
    }
}