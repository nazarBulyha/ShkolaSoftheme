using System;

namespace MobileCommunication.Interfaces
{
    interface IMobileAccount
    {
        int Number { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        DateTime DateBirth { get; set; }
        AddressBook AddressBook { get; set; }

        void SendSMS(int number);

        void ReceiveSMS(int number);

        void MakeCall(int number);

        void ReceiveCall(int number);
    }
}