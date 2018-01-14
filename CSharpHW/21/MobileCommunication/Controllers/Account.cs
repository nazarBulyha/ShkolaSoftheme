namespace MobileCommunication.Controllers
{
	using System;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	using ProtoBuf;

	[ProtoContract]
	public class Account : IAccount
	{
		[ProtoMember(1)]
		public User User { get; set; }

		[ProtoMember(2)]
		public AddressBook AddressBook { get; set; }

		[field: ProtoIgnore]
		public event EventHandler<AccountEventArgs> OnCallHandler;

		[field: ProtoIgnore]
		public event EventHandler<AccountEventArgs> OnSmsHandler;

		[ProtoIgnore]
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