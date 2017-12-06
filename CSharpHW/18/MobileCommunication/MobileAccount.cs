using System;
using MobileCommunication.Interfaces;

namespace MobileCommunication
{
    class MobileAccount : IMobileAccount
    {
        private IMobileOperator _mobileOperator;
        private event EventHandler<AccountEventArgs> OnStartCallHandler;
        private event EventHandler<AccountEventArgs> OnEndCallHandler;
        AccountEventArgs numberEventArgs;

        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public AddressBook AddressBook { get; set; }

        public MobileAccount(IMobileOperator mobileOperator)
        {
            //define standard numbers in account address book 
            //AddressBook = new AddressBook( {"", 2219321 }, { "", 2219322 } );
            _mobileOperator = mobileOperator;

            if (Number == 2219320 || Number == 0)
            {
                Number = mobileOperator.CreateNumber();
            }

        }

        public void MakeCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Number,
                ReceiverNumber = number
            };

            //TODO: logic with Address book
            Console.WriteLine($"Trying to deal {number}.");

            OnStartCallHandler += _mobileOperator.TryMakeCall;
            OnStartCallHandler?.Invoke(this, numberEventArgs);
            OnStartCallHandler -= _mobileOperator.TryMakeCall;
        }

        public void ReceiveCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = this.Number
            };

            //TODO: make logic with address book
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Call received from {number}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            //if account want to receive a call
            if (true)
            {
                OnEndCallHandler += _mobileOperator.EndCall;
                OnEndCallHandler?.Invoke(this, numberEventArgs);
                OnEndCallHandler -= _mobileOperator.EndCall;
            }
        }

        //TODO: realize method as it is in MakeCall
        public void SendSMS(int number)
        {

        }

        //TODO: realize method as it is in ReceiveCall
        public void ReceiveSMS(int number)
        {

        }
    }
}