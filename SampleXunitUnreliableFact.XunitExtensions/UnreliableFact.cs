using Xunit;
using Xunit.Sdk;

namespace SampleXunitUnreliableFact.XunitExtensions
{
    [XunitTestCaseDiscoverer(
        typeName: "SampleXunitUnreliableFact.XunitExtensions.UnreliableFactDiscoverer",
        assemblyName: "SampleXunitUnreliableFact.XunitExtensions"
    )]
    public class UnreliableFact : FactAttribute
    {
        public string RetryableException { get; set; }
    }
}
