namespace MobileCommunication.Controllers
{
	using System;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	[Serializable]
	public class MobileAccount : IMobileAccount
    {
        public Account Account { get; set; }

		public AddressBook AddressBook { get; set; }

		public event EventHandler<AccountEventArgs> OnCallHandler;
		public event EventHandler<AccountEventArgs> OnSmsHandler;

        private AccountEventArgs numberEventArgs;

        public MobileAccount()
        {
	        AddressBook = new AddressBook();
        }

        public void MakeCall(int number)
        {
			numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

			OnCallHandler?.Invoke(this, numberEventArgs);
        }

        public void SendSms(int number)
        {
			numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

			OnSmsHandler?.Invoke(this, numberEventArgs);
        }

		// TODO: make smth
        public void ReceiveCall(int number)
        {
			numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = Account.Number
            };
        }

	    // TODO: make smth
		public void ReceiveSms(int number)
        {
			numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = Account.Number
            };
        }
    }
}