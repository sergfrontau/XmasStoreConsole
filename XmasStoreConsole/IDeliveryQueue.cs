
namespace XmasStoreConsole
{
    public interface IDeliveryQueue
    {
        void Push(PaymentMessage message);
        PaymentMessage Pop();
    }
}
