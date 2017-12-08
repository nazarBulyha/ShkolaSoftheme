using System;
using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;
using System.Collections.Generic;

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
        public string Name { get; set; } = "Ivan";
        public string Surname { get; set; } = "Ivanovych";
        public string Email { get; set; } = "";
        public DateTime DateBirth { get; set; } = DateTime.Now;
        public AddressBook AddressBook { get; set; }

        public MobileAccount(IMobileOperator mobileOperator)
        {
            Operator = mobileOperator;
            Number = mobileOperator.CreateNumber();

            // define standard numbers in account address book
            var standartAccount1 = mobileOperator.SetAccountParametres(this, "standartName1", "standartSurname1", "", DateTime.Now);
            var standartAccount2 = mobileOperator.SetAccountParametres(this, "standartName2", "standartSurname2", "", DateTime.Now);
            var standartAccount3 = mobileOperator.SetAccountParametres(this, "standartName3", "standartSurname3", "", DateTime.Now);
            var standartAccount4 = mobileOperator.SetAccountParametres(this, "standartName4", "standartSurname4", "", DateTime.Now);
                                                                    
            AddressBook = new AddressBook(new List<IMobileAccount>() { standartAccount1, standartAccount2, standartAccount3, standartAccount4 });
        }

        public void MakeCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Number,
                ReceiverNumber = number
            };

            Console.ForegroundColor = ConsoleColor.White;
            // TODO: logic with Address book

            Console.WriteLine($"Trying to deal {AddressBook.GetAccountNameByNumber(number)}.");

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

            OnEndSmsHandler?.Invoke(this, numberEventArgs);
        }
    }
}