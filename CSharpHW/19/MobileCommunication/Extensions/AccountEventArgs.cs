namespace MobileCommunication.Extensions
{
	public class AccountEventArgs
    {
        public int SenderNumber { get; set; }
        public int ReceiverNumber { get; set; }

		public bool IsHandled { get; set; }
    }
}