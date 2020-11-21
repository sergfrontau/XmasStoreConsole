using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace XmasStoreConsole
{
    public class PaymentsQueue : IPaymentsQueue
    {
        ConcurrentDictionary<int, PaymentMessage> _messagesQueue = new ConcurrentDictionary<int, PaymentMessage>();
        readonly object queueLock = new object();

        public void Push(PaymentMessage message)
        {
            // Payment Orders with a duplicate Numbers will be ignored
            _messagesQueue.TryAdd(message.Number, message);
        }

        public void Delete(PaymentMessage message)
        {            
            _messagesQueue.TryRemove(message.Number, out PaymentMessage removedMessage);
        }

        public PaymentMessage Peak()
        {
            lock (queueLock)
            {
                if (_messagesQueue.Any(m => m.Value.Visible))
                {
                    // Read first visible message from the Payments Queue
                    var message = _messagesQueue.OrderBy(m => m.Value.Number).First(m => m.Value.Visible).Value;
                    // Update Last Peak Time
                    message.LastPeakedTime = DateTime.Now;
                    return message;
                }
                else
                    return null;
            }
        }

        public List<PaymentMessage> Messages;
    }
}
