using System.Collections.Frozen;

namespace Irihi.Avalonia.Shared.Shapes;

public partial class IrihiBand
{
    private static readonly byte[,] Irihi_Logo_Bitmap = new byte[,]
    {
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 1 },
    };

    private static readonly byte[,] Character_A = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
    };

    private static readonly byte[,] Character_B = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_C = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_D = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_E = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_F = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 }
    };

    private static readonly byte[,] Character_G = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 0, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_H = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 }
    };

    private static readonly byte[,] Character_I = new byte[,]
    {
        { 1, 1, 1 },
        { 0, 1, 0 },
        { 0, 1, 0 },
        { 0, 1, 0 },
        { 1, 1, 1 },
    };

    private static readonly byte[,] Character_J = new byte[,]
    {
        { 0, 1, 1, 1 },
        { 0, 0, 1, 0 },
        { 0, 0, 1, 0 },
        { 1, 0, 1, 0 },
        { 1, 1, 1, 0 }
    };

    private static readonly byte[,] Character_K = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 1, 0 },
        { 1, 1, 0, 0 },
        { 1, 0, 1, 0 },
        { 1, 0, 0, 1 }
    };

    private static readonly byte[,] Character_L = new byte[,]
    {
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
    };

    private static readonly byte[,] Character_M = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 1, 0, 1, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] Character_N = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1 },
        { 1, 0, 0, 0, 1 }
    };

    private static readonly byte[,] Character_O = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_P = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 1, 0, 1 },
        { 0, 1, 1, 1 },
        { 0, 1, 0, 0 },
        { 0, 1, 0, 0 },
    };

    private static readonly byte[,] Character_Q = new byte[,]
    {
        { 1, 1, 1, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 0, 0, 1, 0 },
        { 1, 1, 1, 1, 1 },
    };

    private static readonly byte[,] Character_R = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
        { 1, 0, 1, 0 },
        { 1, 0, 0, 1 },
    };

    private static readonly byte[,] Character_S = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 1, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 0, 0, 0, 1 },
        { 1, 1, 1, 1 }
    };

    private static readonly byte[,] Character_T = new byte[,]
    {
        { 1, 1, 1, 1, 1 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] Character_U = new byte[,]
    {
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 0, 0, 1 },
        { 1, 1, 1, 1 },
    };

    private static readonly byte[,] Character_V = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] Character_W = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1 },
        { 1, 1, 0, 1, 1 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] Character_X = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 1, 0, 0, 0, 1 },
    };

    private static readonly byte[,] Character_Y = new byte[,]
    {
        { 1, 0, 0, 0, 1 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0 },
    };

    private static readonly byte[,] Character_Z = new byte[,]
    {
        { 1, 1, 1, 1 },
        { 0, 0, 0, 1 },
        { 0, 0, 1, 0 },
        { 0, 1, 0, 0 },
        { 1, 1, 1, 1 }
    };

    private static readonly FrozenDictionary<char, byte[,]> GlyphMappings = new Dictionary<char, byte[,]>()
    {
        ['A'] = Character_A,
        ['B'] = Character_B,
        ['C'] = Character_C,
        ['D'] = Character_D,
        ['E'] = Character_E,
        ['F'] = Character_F,
        ['G'] = Character_G,
        ['H'] = Character_H,
        ['I'] = Character_I,
        ['J'] = Character_J,
        ['K'] = Character_K,
        ['L'] = Character_L,
        ['M'] = Character_M,
        ['N'] = Character_N,
        ['O'] = Character_O,
        ['P'] = Character_P,
        ['Q'] = Character_Q,
        ['R'] = Character_R,
        ['S'] = Character_S,
        ['T'] = Character_T,
        ['U'] = Character_U,
        ['V'] = Character_V,
        ['W'] = Character_W,
        ['X'] = Character_X,
        ['Y'] = Character_Y,
        ['Z'] = Character_Z,
    }.ToFrozenDictionary();

    private static readonly FrozenDictionary<char, int> GlyphWidthMapping =
        GlyphMappings.ToFrozenDictionary(kv => kv.Key, kv => kv.Value.GetLength(1));
}
