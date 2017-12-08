using System;
using MobileCommunication.Extensions;

namespace MobileCommunication.Interfaces
{
    internal interface IMobileAccount
    {
        int Number { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        DateTime DateBirth { get; set; }
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