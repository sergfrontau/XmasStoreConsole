using System.Collections.Concurrent;

namespace XmasStoreConsole
{
    //This queue holds messages for Delivery Processor
    public class DeliveryQueue : IDeliveryQueue
    {
        ConcurrentQueue<PaymentMessage> _messagesQueue = new ConcurrentQueue<PaymentMessage>();

        public void Push(PaymentMessage message)
        {
            _messagesQueue.Enqueue(message);
        }

        public PaymentMessage Pop()
        {
            _messagesQueue.TryDequeue(out PaymentMessage message);
            return message;
        }
    }
}
