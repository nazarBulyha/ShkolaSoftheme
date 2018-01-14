namespace MobileCommunication.Models
{
	using System;

	using MobileCommunication.Enums;

	[Serializable]
	public class LoggerMessage
	{
		public string Message { get; set; }

		public int Sender { get; set; }

		public int Receiver { get; set; }

		public DateTime DateTime { get; set; } = DateTime.Now;

		public MessageType MessageType { get; set; } = MessageType.Error;
	}
}