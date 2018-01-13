namespace MobileCommunication.Controllers
{
	using System;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	[Serializable]
	public class Account : IAccount
	{
		public User User { get; set; }

		public AddressBook AddressBook { get; set; }

		[field: NonSerialized]
		public event EventHandler<AccountEventArgs> OnCallHandler;

		[field: NonSerialized]
		public event EventHandler<AccountEventArgs> OnSmsHandler;

		private AccountEventArgs numberEventArgs;

		public Account()
		{
			AddressBook = new AddressBook();
		}

		public void Call(int number)
		{
			numberEventArgs = new AccountEventArgs
			{
				SenderNumber = User.Number,
				ReceiverNumber = number
			};

			OnCallHandler?.Invoke(this, numberEventArgs);
		}

		public void Sms(int number)
		{
			numberEventArgs = new AccountEventArgs
			{
				SenderNumber = User.Number,
				ReceiverNumber = number
			};

			OnSmsHandler?.Invoke(this, numberEventArgs);
		}

		// TODO: speak
		public void ReceiveCall(int number)
		{
		}

		// TODO: show sms
		public void ReceiveSms(int number)
		{
		}
	}
}