using System;
using Xunit;

namespace SampleXunitUnreliableFact.MyLibrary
{
    // These Facts demonstrate the limits of regular [Fact] tests.

    public class ThirdPartyProxyFacts : IClassFixture<ThirdPartyProxyFixture>
    {
        // Ensure third party gets reset once for all runs of the tests in this class:
        public ThirdPartyProxyFacts(ThirdPartyProxyFixture _)
        { }

        [Fact]
        public void GetNextData_WhenThirdPartyBecomesNotReliable_ThrowsWhenItDoes()
        {
            var unit = new ThirdPartyProxy();

            // This perfectly displays the problem we're trying to solve in this example, as
            // you have to keep calling the unit until it succeeds in the test itself. Instead
            // the UnreliableFact will make this better, see the companion test classes.
            //
            Assert.Throws<InvalidOperationException>(() => { var _ = unit.GetNextData(); });
            Assert.Throws<InvalidOperationException>(() => { var _ = unit.GetNextData(); });
            Assert.Equal("dummy data", unit.GetNextData());
        }
    }
}
