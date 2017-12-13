using System;

namespace MobileCommunication.Models
{
    public class LogMessage
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public  string Message { get; set; }

        public bool IsError { get; set; } = false;
    }
}