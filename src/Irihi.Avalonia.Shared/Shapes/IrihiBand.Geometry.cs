using System.Collections.Frozen;

namespace Irihi.Avalonia.Shared.Shapes;

public partial class IrihiBand
{
    private static readonly byte[,] IrihiLogoBitmap = new byte[,]
    {
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 1 },
    };

    private static readonly byte[,] CharacterA = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
    };

    private static readonly byte[,] CharacterB = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterC = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterD = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterE = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterF = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 }
    };

    private static readonly byte[,] CharacterG = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterH = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 }
    };

    private static readonly byte[,] CharacterI = new byte[,]
    {
        { 1, 1, 1 },
        { 0, 1, 0 },
        { 0, 1, 0 },
        { 0, 1, 0 },
        { 1, 1, 1 },
    };

    private static readonly byte[,] CharacterJ = new byte[,]
    {
        { 0, 1, 1, 1 },
        { 0, 0, 1, 0 },
        { 0, 0, 1, 0 },
        { 1, 0, 1, 0 },
        { 1, 1, 1, 0 }
    };

    private static readonly byte[,] CharacterK = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 1, 1, 0, 0 },
        { 1, 0, 1, 0 },
        { 1, 0, 0, 1 }
    };

    private static readonly byte[,] CharacterL = new byte[,]
    {
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
    };

    private static readonly byte[,] CharacterM = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 1, 0, 1, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] CharacterN = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1 },
        { 1, 0, 0, 0, 1 }
    };

    private static readonly byte[,] CharacterO = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterP = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 1, 1 },
        { 0, 1, 0, 0 },
        { 0, 1, 0, 0 },
    };

    private static readonly byte[,] CharacterQ = new byte[,]
    {
        { 1, 1, 1, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 1, 1, 1, 1 },
    };

    private static readonly byte[,] CharacterR = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 1, 0 },
        { 1, 0, 0, 1 },
    };

    private static readonly byte[,] CharacterS = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 0, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterT = new byte[,]
    {
        { 1, 1, 1, 1, 1 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] CharacterU = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
    };

    private static readonly byte[,] CharacterV = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] CharacterW = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 1, 0, 1, 1 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] CharacterX = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] CharacterY = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] CharacterZ = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 0, 0, 1 },
        { 0, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] CharacterSpace = new byte[,]
    {
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 },
        { 0, 0, 0 },
    };

    private static readonly byte[,] CharacterApostrophe = new byte[,]
    {
        { 1 },
        { 1 },
        { 0 },
        { 0 },
        { 0 },
    };

    private static readonly byte[,] CharacterPeriod = new byte[,]
    {
        { 0 },
        { 0 },
        { 0 },
        { 0 },
        { 1 },
    };

    private static readonly FrozenDictionary<char, byte[,]> GlyphMappings = new Dictionary<char, byte[,]>()
    {
        ['A'] = CharacterA,
        ['B'] = CharacterB,
        ['C'] = CharacterC,
        ['D'] = CharacterD,
        ['E'] = CharacterE,
        ['F'] = CharacterF,
        ['G'] = CharacterG,
        ['H'] = CharacterH,
        ['I'] = CharacterI,
        ['J'] = CharacterJ,
        ['K'] = CharacterK,
        ['L'] = CharacterL,
        ['M'] = CharacterM,
        ['N'] = CharacterN,
        ['O'] = CharacterO,
        ['P'] = CharacterP,
        ['Q'] = CharacterQ,
        ['R'] = CharacterR,
        ['S'] = CharacterS,
        ['T'] = CharacterT,
        ['U'] = CharacterU,
        ['V'] = CharacterV,
        ['W'] = CharacterW,
        ['X'] = CharacterX,
        ['Y'] = CharacterY,
        ['Z'] = CharacterZ,
        [' '] = CharacterSpace,
        ['\''] = CharacterApostrophe,
        ['.'] = CharacterPeriod,
    }.ToFrozenDictionary();

    private static readonly FrozenDictionary<char, int> GlyphWidthMapping =
        GlyphMappings.ToFrozenDictionary(kv => kv.Key, kv => kv.Value.GetLength(1));
}
