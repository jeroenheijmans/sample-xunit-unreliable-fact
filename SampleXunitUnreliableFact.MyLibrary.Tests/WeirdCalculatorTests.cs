using System;
using Xunit;

namespace SampleXunitUnreliableFact.MyLibrary
{
    public class WeirdCalculatorTests
    {
        [Fact]
        public void Fact_asserts_SquareInputReliably_works()
        {
            var unit = new WeirdCalculator(randomSeed: 42);
            var result = unit.SquareInputReliably(5);
            Assert.Equal(25, result);
        }

        [Fact]
        public void Fact_asserts_SquareInputAfterTwoTries_somewhat_works()
        {
            var unit = new WeirdCalculator(randomSeed: 42);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputAfterTwoTries(5));
            var result = unit.SquareInputAfterTwoTries(5);
            Assert.Equal(25, result);
        }

        [Fact]
        public void Fact_asserts_SquareInputUnreliably_fails_on_specific_seed()
        {
            var unit = new WeirdCalculator(randomSeed: 3);
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputUnreliably(5));
            Assert.Throws<InvalidOperationException>(() => unit.SquareInputUnreliably(5));
            var result = unit.SquareInputUnreliably(5);
            Assert.Equal(25, result);
        }
    }
}
