using System;
using System.Threading;

namespace XmasStoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the number of payment processors:");
            var paymentProcessors = ReadConsoleInput();

            Console.WriteLine("Please enter the number of delivery processors:");
            var deliveryProcessors = ReadConsoleInput();
                     
            Console.WriteLine("Starting orders...");

            Initialize(paymentProcessors, deliveryProcessors);

            Thread.Sleep(Timeout.Infinite);
        }

        private static void Initialize(int paymentProcessors, int deliveryProcessors)
        {
            var paymentsQueue = new PaymentsQueue();
            var deliverQueue = new DeliveryQueue();
            var sender = new OrderSender(paymentsQueue);

            for (int i = 1; i <= paymentProcessors; i++)
            {
                var paymentProcessor = new PaymentProcessor(i, paymentsQueue, deliverQueue);
            }

            for (int i = 1; i <= deliveryProcessors; i++)
            {
                var deliveryProcessor = new DeliveryProcessor(i, deliverQueue);
            }
        }

        private static int ReadConsoleInput()
        {
            int result;
            bool success;
            do
            {
                success = int.TryParse(Console.ReadLine(), out result);

                if (!success)
                    Console.WriteLine("Please enter numeric value");

            } while (!success);

            return result;
        }       
    }
}
