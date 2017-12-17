using System;
using MobileCommunication.Extensions;
using MobileCommunication.Models;

namespace MobileCommunication.Interfaces
{
	public interface IMobileAccount
    {
        Account Account { get; set; }

		AddressBook AddressBook { get; set; }

		event EventHandler<AccountEventArgs> OnStartCallHandler;
        event EventHandler<AccountEventArgs> OnEndCallHandler;
        event EventHandler<AccountEventArgs> OnStartSmsHandler;
        event EventHandler<AccountEventArgs> OnEndSmsHandler;

        void SendSms(int number);

        void ReceiveSms(int number);

        void MakeCall(int number);

        void ReceiveCall(int number);
    }
}