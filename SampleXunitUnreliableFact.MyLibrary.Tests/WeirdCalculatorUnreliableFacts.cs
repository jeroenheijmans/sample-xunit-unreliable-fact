using System;
using SampleXunitUnreliableFact.XunitExtensions;
using Xunit;

namespace SampleXunitUnreliableFact.MyLibrary
{
    // Here, we use [UnreliableFact], and presume we cannot provide
    // a random seed, as the failures of the System Under Test is
    // in fact... unreliable!

    public class WeirdCalculatorUnreliableFacts
    {
        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void SquareInputReliably_WhenGivenInput_ReturnsSquare()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputReliably(5);
            Assert.Equal(25, result);
        }

        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void SquareInputAfterTwoTries_WhenGivenInput_ReturnsSquare()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputAfterTwoTries(5);
            Assert.Equal(25, result);
        }

        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void SquareInputUnreliably_WhenCalled_ReturnsSquare()
        {
            // This seed will cause 2 failures, so within the 'grasp' of [UnreliableFact] by default:
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputUnreliably(5);
            Assert.Equal(25, result);
        }

        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void SquareInputOnlyForVeryLowNumbers_WhenGivenLowNumber_ReturnsSquare()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            var result = unit.SquareInputOnlyForVeryLowNumbers(3);
            Assert.Equal(9, result);
        }

        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void SquareInputOnlyForVeryLowNumbers_WhenGivenHighNumber_ThrowsEvenForUnreliableFact()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputOnlyForVeryLowNumbers(5));
        }
    }
}
