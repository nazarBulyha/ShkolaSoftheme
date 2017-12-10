using System;

namespace MobileCommunication.Models
{
    internal class LogMessage
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public  string Message { get; set; } = "";

        public int Sender { get; set; } = 0;

        public int Receiver { get; set; } = 0;

        public bool IsError { get; set; } = false;
    }
}
