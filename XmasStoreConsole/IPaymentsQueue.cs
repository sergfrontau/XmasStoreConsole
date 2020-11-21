
namespace XmasStoreConsole
{
    public interface IPaymentsQueue
    {
        void Push(PaymentMessage message);
        PaymentMessage Peak();
        void Delete(PaymentMessage message);
    }
}
