using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SampleXunitUnreliableFact.XunitExtensions
{
    public class ExceptionTrackingMessageBus : IMessageBus
    {
        private readonly IMessageBus inner;
        private readonly string retryableException;
        private readonly Queue<IMessageSinkMessage> buffer = new Queue<IMessageSinkMessage>();

        public bool HasSeenRetryableException { get; private set; }

        public ExceptionTrackingMessageBus(IMessageBus inner, string retryableExceptions)
        {
            this.inner = inner;
            this.retryableException = retryableExceptions;
        }

        public bool QueueMessage(IMessageSinkMessage message)
        {
            if (message is ITestFailed failureMessage)
            {
                if (failureMessage.ExceptionTypes.Any(t => t == retryableException))
                {
                    HasSeenRetryableException = true;
                }
            }

            // Return statement and locking taken from Samples.Xunit DelayedMessageBus.
            lock (buffer) buffer.Enqueue(message);
            return true;
        }

        public void Flush()
        {
            if (inner != null)
            {
                while (buffer.Any())
                {
                    inner.QueueMessage(buffer.Dequeue());
                }
            }
        }

        public void Dispose()
        {
            Flush();
        }
    }
}
