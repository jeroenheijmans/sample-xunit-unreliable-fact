using System;
using Xunit;

namespace SampleXunitUnreliableFact.MyLibrary
{
    public class WeirdCalculatorFacts
    {
        [Fact]
        public void SquareInputReliably_WhenGivenInput_ReturnsSquare()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputReliably(5);
            Assert.Equal(25, result);
        }

        [Fact]
        public void SquareInputAfterTwoTries_WhenGivenInputTwice_ReturnsSquareSecondTime()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputAfterTwoTries(5));
            var result = unit.SquareInputAfterTwoTries(5);
            Assert.Equal(25, result);
        }

        [Fact]
        public void SquareInputUnreliably_WhenSeedCausesTwoFailures_ReturnsSquareThirdTime()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputUnreliably(5));
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputUnreliably(5));
            var result = unit.SquareInputUnreliably(5);
            Assert.Equal(25, result);
        }

        [Fact]
        public void SquareInputOnlyForVeryLowNumbers_WhenGivenLowNumber_ReturnsSquare()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputOnlyForVeryLowNumbers(3);
            Assert.Equal(9, result);
        }

        [Fact]
        public void SquareInputOnlyForVeryLowNumbers_WhenGivenHighNumber_Throws()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputOnlyForVeryLowNumbers(5));
        }
    }
}
