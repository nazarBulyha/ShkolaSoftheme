using System;
using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;
using System.Collections.Generic;
using MobileCommunication.Models;

namespace MobileCommunication
{
    internal class MobileAccount : IMobileAccount
    {
        public IMobileOperator Operator { get; }

        public Account Account { get; set; }

        public event EventHandler<AccountEventArgs> OnStartCallHandler;
        public event EventHandler<AccountEventArgs> OnEndCallHandler;
        public event EventHandler<AccountEventArgs> OnStartSmsHandler;
        public event EventHandler<AccountEventArgs> OnEndSmsHandler;

        private AccountEventArgs numberEventArgs;

        public MobileAccount(IMobileOperator mobileOperator)
        {
            // define standard numbers in account address book
            var standartAccount1 = mobileOperator.SetAccountParametres(this, "standartName1", "standartSurname1", "", DateTime.Now);
            var standartAccount2 = mobileOperator.SetAccountParametres(this, "standartName2", "standartSurname2", "", DateTime.Now);
            var standartAccount3 = mobileOperator.SetAccountParametres(this, "standartName3", "standartSurname3", "", DateTime.Now);
            var standartAccount4 = mobileOperator.SetAccountParametres(this, "standartName4", "standartSurname4", "", DateTime.Now);

            Operator = mobileOperator;
            Account.Number = mobileOperator.CreateNumber();
            Account.AddressBook.SetAccounts(new List<IMobileAccount>() { standartAccount1, standartAccount2, standartAccount3, standartAccount4 });
        }

        public void MakeCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Try call to {Account.AddressBook.GetAccountNameByNumber(number)}.");

            OnStartCallHandler?.Invoke(this, numberEventArgs);
        }

        public void SendSms(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Try send SMS to {Account.AddressBook.GetAccountNameByNumber(number)}.");

            OnStartSmsHandler?.Invoke(this, numberEventArgs);
        }

        public void ReceiveCall(int number)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Call");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" from ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{Account.AddressBook.GetAccountNameByNumber(number)}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(", established.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

            // if account want to receive a call
            if (true)
            {
                OnEndCallHandler?.Invoke(this, numberEventArgs);
            }
        }

        public void ReceiveSms(int number)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Sms");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" received from ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Account.AddressBook.GetAccountNameByNumber(number)}.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            OnEndSmsHandler?.Invoke(this, numberEventArgs);
        }
    }
}