using System;
using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;

namespace MobileCommunication
{
    internal class MobileAccount : IMobileAccount
    {
        public IMobileOperator Operator { get; }

        public event EventHandler<AccountEventArgs> OnStartCallHandler;
        public event EventHandler<AccountEventArgs> OnEndCallHandler;
        public event EventHandler<AccountEventArgs> OnStartSmsHandler;
        public event EventHandler<AccountEventArgs> OnEndSmsHandler;

        private AccountEventArgs numberEventArgs;

        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public AddressBook AddressBook { get; set; }

        public MobileAccount(IMobileOperator mobileOperator)
        {
            Operator = mobileOperator;

            Number = mobileOperator.CreateNumber();
            Name = "Ivan";
            Surname = "Ivanovych";
            Email = "";
            DateBirth = DateTime.Now;
            // define standard numbers in account address book 
            //AddressBook = new AddressBook( new MobileAccount(this), { "", 2219322 } );
        }

        public void MakeCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Number,
                ReceiverNumber = number
            };

            // TODO: logic with Address book


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Trying to deal {number}.");

            OnStartCallHandler?.Invoke(this, numberEventArgs);
        }

        public void SendSms(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Number,
                ReceiverNumber = number
            };

            // TODO: logic with Address book
            Console.WriteLine($"Trying to send SMS to {number}.");

            OnStartSmsHandler?.Invoke(this, numberEventArgs);
        }

        public void ReceiveCall(int number)
        {
            // TODO: make logic with address book

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Call");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" received from ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{number}.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // if account want to receive a call
            if (true)
            {
                OnEndCallHandler?.Invoke(this, numberEventArgs);
            }
        }

        public void ReceiveSms(int number)
        {
            // TODO: make logic with address book

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Sms");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" received from ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{number}.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // if account want to receive a call
            if (true)
            {
                OnEndSmsHandler?.Invoke(this, numberEventArgs);
            }
        }
    }
}