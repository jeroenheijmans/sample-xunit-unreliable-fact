using System;

namespace SampleXunitUnreliableFact.MyLibrary
{
    public class WeirdCalculator
    {
        private readonly Random random;

        // TODO: Replace with more reliable simulation of intermittent issues.
        // When a TestCase runs a test multiple times, a fresh instance of the class is
        // created each time, thus resetting the counter.
        public int Counter { get; private set; }

        public WeirdCalculator(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public int SquareInputReliably(int input)
        {
            Counter++;
            return input * input;
        }

        public int SquareInputAfterTwoTries(int input)
        {
            Counter++;
            if (Counter <= 1) throw new InvalidOperationException("You haven't tried often enough!");
            return input * input;
        }

        public int SquareInputUnreliably(int input)
        {
            Counter++;
            if (random.NextDouble() < 0.75) throw new InvalidOperationException("Randomly failed!");
            return input * input;
        }

        public int SquareInputOnlyForVeryLowNumbers(int input)
        {
            Counter++;
            if (input >= 5) throw new InvalidOperationException("Can only square numbers under five");
            return input * input;
        }
    }
}
