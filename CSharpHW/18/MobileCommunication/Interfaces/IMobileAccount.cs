using System;
using MobileCommunication.Extensions;
using MobileCommunication.Models;

namespace MobileCommunication.Interfaces
{
	public interface IMobileAccount
    {
        Account Account { get; set; }

		AddressBook AddressBook { get; set; }

		event EventHandler<AccountEventArgs> OnCallHandler;
        event EventHandler<AccountEventArgs> OnSmsHandler;

        void SendSms(int number);

        void ReceiveSms(int number);

        void MakeCall(int number);

        void ReceiveCall(int number);
    }
}