using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SampleXunitUnreliableFact.XunitExtensions
{
    public class UnreliableTestCase : XunitTestCase
    {
        private const int MaxNrOfRetries = 3;
        private string retryableException;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public UnreliableTestCase()
            : base()
        { }

        public UnreliableTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay,
            TestMethodDisplayOptions defaultMethodDisplayOptions,
            ITestMethod testMethod,
            string retryableException)
            : base(
                  diagnosticMessageSink,
                  defaultMethodDisplay,
                  defaultMethodDisplayOptions,
                  testMethod,
                  testMethodArguments: null)
        {
            this.retryableException = retryableException;
        }

        public override async Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var nrOfTries = 0;

            while (true)
            {
                nrOfTries++;

                var decoratedMessageBus = new ExceptionTrackingMessageBus(messageBus, retryableException);

                var summary = await base.RunAsync(
                    diagnosticMessageSink,
                    decoratedMessageBus,
                    constructorArguments,
                    aggregator,
                    cancellationTokenSource);

                var hasNormalException = aggregator.HasExceptions && !decoratedMessageBus.HasSeenRetryableException;

                if (nrOfTries >= MaxNrOfRetries || summary.Failed == 0 || hasNormalException)
                {
                    decoratedMessageBus.Flush();
                    return summary;
                }

                diagnosticMessageSink.OnMessage(new DiagnosticMessage(
                    "Test execution of {0} failed with retryable exception. Tried {1} times so far.",
                    DisplayName,
                    nrOfTries
                ));
            }
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);
            data.AddValue("RetryableException", retryableException);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);
            retryableException = data.GetValue<string>("RetryableException");
        }
    }
}
