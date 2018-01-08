namespace MobileCommunication.Models
{
	using System;

	using MobileCommunication.Enums;

	public class LogMessage
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public  string Message { get; set; }

	    public int Sender { get; set; }

	    public int Receiver { get; set; }

		public MessageType MessageType { get; set; } = MessageType.Error;
    }
}