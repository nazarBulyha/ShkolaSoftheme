using System;
using MobileCommunication.Extensions;
using MobileCommunication.Models;

namespace MobileCommunication.Interfaces
{
	public interface IAccount
	{
		User User { get; set; }

		AddressBook AddressBook { get; set; }

		event EventHandler<AccountEventArgs> OnCallHandler;
		event EventHandler<AccountEventArgs> OnSmsHandler;

		void Call(int number);

		void ReceiveCall(int number);

		void Sms(int number);

		void ReceiveSms(int number);
	}
}