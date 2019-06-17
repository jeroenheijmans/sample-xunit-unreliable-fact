using System;
using SampleXunitUnreliableFact.XunitExtensions;
using Xunit;

namespace SampleXunitUnreliableFact.MyLibrary
{
    // Here, we use [UnreliableFact], and presume we cannot provide
    // a random seed, as the failures of the System Under Test is
    // in fact... unreliable!

    public class ThirdPartyProxyUnreliableFacts : IClassFixture<ThirdPartyProxyFixture>
    {
        // Ensure third party gets reset once for all runs of the tests in this class:
        public ThirdPartyProxyUnreliableFacts(ThirdPartyProxyFixture _)
        { }

        [UnreliableFact(RetryableException = "System.InvalidOperationException")]
        public void GetNextData_WhenThirdPartyBecomesNotReliable_ThrowsWhenItDoes()
        {
            var unit = new ThirdPartyProxy();

            // UnreliableFact keeps retrying until it succeeds:
            Assert.Equal("dummy data", unit.GetNextData());
        }
    }
}
