using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class MathHelperTests
{
    [Theory]
    [InlineData(0, 1, 2, 1)]
    [InlineData(1, 1, 2, 1)]
    [InlineData(2, 1, 2, 2)]
    [InlineData(3, 1, 2, 2)]
    [InlineData(0, 2, 1, 1)]
    [InlineData(1, 2, 1, 1)]
    [InlineData(2, 2, 1, 2)]
    [InlineData(3, 2, 1, 2)]
    [InlineData(3, 1, 1, 1)]
    public void SafeClamp_Decimal_Success(decimal value, decimal min, decimal max, decimal expected)
    {
        Assert.Equal(expected, MathHelpers.SafeClamp(value, min, max));
    }
    
    [Theory]
    [InlineData(0, 1, 2, 1)]
    [InlineData(1, 1, 2, 1)]
    [InlineData(2, 1, 2, 2)]
    [InlineData(3, 1, 2, 2)]
    [InlineData(0, 2, 1, 1)]
    [InlineData(1, 2, 1, 1)]
    [InlineData(2, 2, 1, 2)]
    [InlineData(3, 2, 1, 2)]
    [InlineData(3, 1, 1, 1)]
    public void SafeClamp_Double_Success(double value, double min, double max, double expected)
    {
        Assert.Equal(expected, MathHelpers.SafeClamp(value, min, max));
    }
    
    [Theory]
    [InlineData(0, 1, 2, 1)]
    [InlineData(1, 1, 2, 1)]
    [InlineData(2, 1, 2, 2)]
    [InlineData(3, 1, 2, 2)]
    [InlineData(0, 2, 1, 1)]
    [InlineData(1, 2, 1, 1)]
    [InlineData(2, 2, 1, 2)]
    [InlineData(3, 2, 1, 2)]
    [InlineData(3, 1, 1, 1)]
    public void SafeClamp_Float_Success(float value, float min, float max, float expected)
    {
        Assert.Equal(expected, MathHelpers.SafeClamp(value, min, max));
    }
    
    [Theory]
    [InlineData(0, 1, 2, 1)]
    [InlineData(1, 1, 2, 1)]
    [InlineData(2, 1, 2, 2)]
    [InlineData(3, 1, 2, 2)]
    [InlineData(0, 2, 1, 1)]
    [InlineData(1, 2, 1, 1)]
    [InlineData(2, 2, 1, 2)]
    [InlineData(3, 2, 1, 2)]
    [InlineData(3, 1, 1, 1)]
    public void SafeClamp_Int_Success(int value, int min, int max, int expected)
    {
        Assert.Equal(expected, MathHelpers.SafeClamp(value, min, max));
    }
    
    
}