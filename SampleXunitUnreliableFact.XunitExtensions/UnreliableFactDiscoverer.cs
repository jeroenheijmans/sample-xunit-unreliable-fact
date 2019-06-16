using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SampleXunitUnreliableFact.XunitExtensions
{
    public class UnreliableFactDiscoverer : IXunitTestCaseDiscoverer
    {
        private readonly IMessageSink diagnosticMessageSink;

        public UnreliableFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var retryableException = factAttribute.GetNamedArgument<string>("RetryableException");
            
            yield return new UnreliableTestCase(
                diagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(),
                discoveryOptions.MethodDisplayOptionsOrDefault(),
                testMethod,
                retryableException
            );
        }
    }
}
