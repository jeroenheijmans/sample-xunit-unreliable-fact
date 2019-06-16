using System;
using System.ComponentModel;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SampleXunitUnreliableFact.XunitExtensions
{
    public class UnreliableTestCase : XunitTestCase
    {
        private readonly string[] retryableExceptionTypeNames;

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
            string[] retryableExceptionTypeNames)
            : base(
                  diagnosticMessageSink,
                  defaultMethodDisplay,
                  defaultMethodDisplayOptions,
                  testMethod,
                  testMethodArguments: null)
        {
            this.retryableExceptionTypeNames = retryableExceptionTypeNames;
        }
    }
}
