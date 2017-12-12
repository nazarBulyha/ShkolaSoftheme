using System;

namespace MobileCommunication.Models
{
    internal class LogMessage
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public  string Message { get; set; }

        public bool IsError { get; set; } = false;
    }
}
