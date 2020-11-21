using System;
using System.Timers;

namespace XmasStoreConsole
{
    public class DeliveryProcessor
    {
        Timer _timer = new Timer(100);
        IDeliveryQueue _deliveryQueue;
        int _index;

        public DeliveryProcessor(int index, IDeliveryQueue deliveryQueue)
        {
            _deliveryQueue = deliveryQueue;
            _index = index;

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Retrieve message from Delivery Queue
            var message = _deliveryQueue.Pop();

            if (message != null)            
                Console.WriteLine("Delivery Processor# " + _index + ": " + DateTime.Now.ToString("h:mm:ss tt") + " Order #" + message.Number + ". Ready for Delivery.");
        }
    }
}
