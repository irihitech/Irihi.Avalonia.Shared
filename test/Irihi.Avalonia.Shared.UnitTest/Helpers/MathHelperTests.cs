using Avalonia;
using Irihi.Avalonia.Shared.Helpers;

namespace Irihi.Avalonia.Shared.UnitTest.Helpers;

public class MathHelperTests
{
    private const double AnyValue = 42.42;
    private readonly double _calculatedAnyValue;
    private readonly double _one;
    private readonly double _zero;
    
    public MathHelperTests()
    {
        _calculatedAnyValue = 0.0;
        _one = 0.0;
        _zero = 1.0;

        const int N = 10;
        var dxAny = AnyValue / N;
        var dxOne = 1.0 / N;
        var dxZero = _zero / N;

        for (var i = 0; i < N; ++i)
        {
            _calculatedAnyValue += dxAny;
            _one += dxOne;
            _zero -= dxZero;
        }
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

    [Fact]
        public void Two_Equivalent_Double_Values_Are_Close()
        {
            var actual = MathHelpers.AreClose(AnyValue, _calculatedAnyValue);

            Assert.True(actual);
            Assert.Equal(AnyValue, Math.Round(_calculatedAnyValue, 14));
        }

        [Fact]
        public void Two_Equivalent_Single_Values_Are_Close()
        {
            var expectedValue = (float)AnyValue;
            var actualValue = (float)_calculatedAnyValue;
            
            var actual = MathHelpers.AreClose(expectedValue, actualValue);

            Assert.True(actual);
            Assert.Equal((float) Math.Round(expectedValue, 5), (float) Math.Round(actualValue, 4));
        }

        [Fact]
        public void Calculated_Double_One_Is_One()
        {
            var actual = MathHelpers.IsOne(_one);

            Assert.True(actual);
            Assert.Equal(1.0, Math.Round(_one, 15));
        }

        [Fact]
        public void Calculated_Single_One_Is_One()
        {
            var actualValue = (float)_one;
            
            var actual = MathHelpers.IsOne(actualValue);

            Assert.True(actual);
            Assert.Equal(1.0f, (float) Math.Round(actualValue, 7));
        }

        [Fact]
        public void Calculated_Double_Zero_Is_Zero()
        {
            var actual = MathHelpers.IsZero(_zero);

            Assert.True(actual);
            Assert.Equal(0.0, Math.Round(_zero, 15));
        }

        [Fact]
        public void Calculated_Single_Zero_Is_Zero()
        {
            var actualValue = (float)_zero;

            var actual = MathHelpers.IsZero(actualValue);

            Assert.True(actual);
            Assert.Equal(0.0f, (float) Math.Round(actualValue, 7));
        }

        [Fact]
        public void Float_Clamp_Input_NaN_Return_NaN()
        {
            var clamp = MathHelpers.SafeClamp(double.NaN, 0.0, 1.0);
            Assert.True(double.IsNaN(clamp));
        }

        [Fact]
        public void Float_Clamp_Input_NegativeInfinity_Return_Min()
        {
            const double min = 0.0;
            const double max = 1.0;
            var actual = MathHelpers.SafeClamp(double.NegativeInfinity, min, max);
            Assert.Equal(min, actual);
        }

        [Fact]
        public void Float_Clamp_Input_PositiveInfinity_Return_Max()
        {
            const double min = 0.0;
            const double max = 1.0;
            var actual = MathHelpers.SafeClamp(double.PositiveInfinity, min, max);
            Assert.Equal(max, actual);
        }

        [Fact]
        public void Double_Float_Zero_Less_Than_One()
        {
            var actual = MathHelpers.LessThan(0d, 1d);
            Assert.True(actual);
        }

        [Fact]
        public void Single_Float_Zero_Less_Than_One()
        {
            var actual = MathHelpers.LessThan(0f, 1f);
            Assert.True(actual);
        }

        [Fact]
        public void Double_Float_One_Not_Less_Than_Zero()
        {
            var actual = MathHelpers.LessThan(1d, 0d);
            Assert.False(actual);
        }

        [Fact]
        public void Single_Float_One_Not_Less_Than_Zero()
        {
            var actual = MathHelpers.LessThan(1f, 0f);
            Assert.False(actual);
        }

        [Fact]
        public void Double_Float_Zero_Not_Greater_Than_One()
        {
            var actual = MathHelpers.GreaterThan(0d, 1d);
            Assert.False(actual);
        }

        [Fact]
        public void Single_Float_Zero_Not_Greater_Than_One()
        {
            var actual = MathHelpers.GreaterThan(0f, 1f);
            Assert.False(actual);
        }

        [Fact]
        public void Double_Float_One_Greater_Than_Zero()
        {
            var actual = MathHelpers.GreaterThan(1d, 0d);
            Assert.True(actual);
        }

        [Fact]
        public void Single_Float_One_Greater_Than_Zero()
        {
            var actual = MathHelpers.GreaterThan(1f, 0f);
            Assert.True(actual);
        }

        [Fact]
        public void Double_Float_One_Less_Than_Or_Close_One()
        {
            var actual = MathHelpers.LessThanOrClose(1d, 1d);
            Assert.True(actual);
        }

        [Fact]
        public void Single_Float_One_Less_Than_Or_Close_One()
        {
            var actual = MathHelpers.LessThanOrClose(1f, 1f);
            Assert.True(actual);
        }

        [Fact]
        public void Double_Float_One_Greater_Than_Or_Close_One()
        {
            var actual = MathHelpers.GreaterThanOrClose(1d, 1d);
            Assert.True(actual);
        }

        [Fact]
        public void Single_Float_One_Greater_Than_Or_Close_One()
        {
            var actual = MathHelpers.GreaterThanOrClose(1f, 1f);
            Assert.True(actual);
        }
        
        [Fact]
        public void AreClose_DoubleValuesWithinCustomEpsilon_ReturnsTrue()
        {
            double value1 = 1.0;
            double value2 = 1.0 + 0.5 * MathHelpers.DoubleEpsilon;
            double epsilon = 1.0 * MathHelpers.DoubleEpsilon;
            bool result = MathHelpers.AreClose(value1, value2, epsilon);
            Assert.True(result);
        }

        [Fact]
        public void AreClose_DoubleValuesOutsideCustomEpsilon_ReturnsFalse()
        {
            double value1 = 1.0;
            double value2 = 1.0 + 2.0 * MathHelpers.DoubleEpsilon;
            double epsilon = 1.0 * MathHelpers.DoubleEpsilon;
            bool result = MathHelpers.AreClose(value1, value2, epsilon);
            Assert.False(result);
        }
}