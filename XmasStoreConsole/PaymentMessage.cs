using System;

namespace XmasStoreConsole
{
    public class PaymentMessage
    {
        public PaymentMessage(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
        public DateTime? LastPeakedTime { get; set; }

        public bool Visible
        {
            get
            {
                // Message is visible (or available) for a Payment Processor to pick it up if it's never been 
                // peaked or has been peaked more that 5 secs ago
                return LastPeakedTime == null || (DateTime.Now - LastPeakedTime > new TimeSpan(0, 0, 5));
            }
        }
    }
}
