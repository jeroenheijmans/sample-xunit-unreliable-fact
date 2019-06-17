namespace SampleXunitUnreliableFact.MyLibrary
{
    public class ThirdPartyProxyFixture
    {
        public ThirdPartyProxyFixture()
        {
            ThirdPartyProxy.__ResetThirdPartyReliability(nrOfCallsBeforeSuccess: 2);
        }
    }
}
