using System;

namespace SampleXunitUnreliableFact.MyLibrary
{
    public class ThirdPartyProxy
    {
        // For simulation purposes
        private static class UnreliableThirdParty
        {
            public static int NrOfCallsBeforeSuccess { get; set; }

            public static string GetSomeData()
            {
                NrOfCallsBeforeSuccess--;
                if (NrOfCallsBeforeSuccess >= 0) throw new InvalidOperationException();
                return "dummy data";
            }
        }

        public static void __ResetThirdPartyReliability(int nrOfCallsBeforeSuccess = 2)
        {
            UnreliableThirdParty.NrOfCallsBeforeSuccess = nrOfCallsBeforeSuccess;
        }

        public string GetNextData()
        {
            return UnreliableThirdParty.GetSomeData();
        }
    }
}
