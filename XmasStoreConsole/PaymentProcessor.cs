using System;
using System.Threading.Tasks;
using System.Timers;

namespace XmasStoreConsole
{
    public class PaymentProcessor
    {
        int _index;
        Timer _timer = new Timer(100);
        IPaymentsQueue _paymentsQueue;
        IDeliveryQueue _deliveryQueue;
        int _paymentIndex = 0;

        public PaymentProcessor(int index, IPaymentsQueue paymentsQueue, IDeliveryQueue deliveryQueue)
        {
            _index = index;
            _paymentsQueue = paymentsQueue;
            _deliveryQueue = deliveryQueue;

            _timer.Elapsed += _timer_Elapsed;
            ActivateTimer(true);
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Read message from the queue
            var message = _paymentsQueue.Peak();

            if (message != null)
            {
                ActivateTimer(false);                

                await ProcessMessage(message);

                ActivateTimer(true);
            }
        }

        private async Task ProcessMessage(PaymentMessage message)
        {
            Console.WriteLine("Payment Processor# " + _index + ": " + DateTime.Now.ToString("h:mm:ss tt") + " Order #" + message.Number + ". Processing Payment.");

            _paymentIndex++;

            // Fail every 5th message recieved by the Payment Processor
            if (_paymentIndex % 5 == 0)
                Console.WriteLine("Payment Processor# " + _index + ": " + DateTime.Now.ToString("h:mm:ss tt") + " Order #" + message.Number + ". Payment Failed.");
            else
            {
                await Task.Delay(2000);
                // After successful processing delete the message from the Payment Queue
                _paymentsQueue.Delete(message);
                // Push the message to the Delivery Queue
                _deliveryQueue.Push(message);

                Console.WriteLine("Payment Processor# " + _index + ": " + DateTime.Now.ToString("h:mm:ss tt") + " Order #" + message.Number + ". Payment Processed.");
            }
        }

        private void ActivateTimer(bool activate)
        {
            _timer.Enabled = activate;
        }
    }
}
