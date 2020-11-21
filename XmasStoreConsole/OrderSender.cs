using System;
using System.Timers;

namespace XmasStoreConsole
{
    public class OrderSender
    {
        Timer _timer = new Timer(1000);
        IPaymentsQueue _paymentsQueue;
        int _index = 0;
        DateTime _startTime;

        public OrderSender(IPaymentsQueue paymentsQueue)
        {
            _paymentsQueue = paymentsQueue;
            _startTime = DateTime.Now;

            SendOrder();

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        private void SendOrder()
        {
            _index++;
            _paymentsQueue.Push(new PaymentMessage(_index));

            Console.WriteLine("Order Sender: " + DateTime.Now.ToString("h:mm:ss tt") + " Order #" + _index + ". Sent.");
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            AdjustTimerInterval();
            SendOrder();
        }

        private void AdjustTimerInterval()
        {
            // Gradually increase number of messages sent per second up to 10 per second
            var numberOfFiveSecIntervalsPassed = (int)(DateTime.Now - _startTime).TotalSeconds / 5;
            var numberOfMessagesPerSecond = numberOfFiveSecIntervalsPassed + 1;

            var timerInterval = 1000 / (numberOfMessagesPerSecond <= 10 ? numberOfMessagesPerSecond : 10);
            _timer.Interval = timerInterval;
        }
    }
}
