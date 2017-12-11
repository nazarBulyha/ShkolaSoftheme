using System;
using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;
using MobileCommunication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MobileCommunication
{
    public class MobileAccount : IMobileAccount
    {
        public event EventHandler<AccountEventArgs> OnStartCallHandler;
        public event EventHandler<AccountEventArgs> OnEndCallHandler;
        public event EventHandler<AccountEventArgs> OnStartSmsHandler;
        public event EventHandler<AccountEventArgs> OnEndSmsHandler;

        public MobileAccount() { }

        [Key]
        public int MobileAccountId { get; set; }

        public AccocuntDetails Account { get; set; }

        public AddressBook AddressBook { get; set; } = new AddressBook();

        [NotMapped]
        private AccountEventArgs numberEventArgs;
        [NotMapped]
        private IMobileOperator Operator { get; }

        public MobileAccount(IMobileOperator mobileOperator)
        {
            Operator = mobileOperator;
        }

        public void MakeCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = Account.Number,
                ReceiverNumber = number
            };

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Try call to {AddressBook.GetAccountNameByNumber(number)}.");

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
            Console.WriteLine($"Try send SMS to {AddressBook.GetAccountNameByNumber(number)}.");

            OnStartSmsHandler?.Invoke(this, numberEventArgs);
        }

        public void ReceiveCall(int number)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Call");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" from ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{AddressBook.GetAccountNameByNumber(number)}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(", established.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = Account.Number
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
            Console.WriteLine($"{AddressBook.GetAccountNameByNumber(number)}.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = Account.Number
            };

            OnEndSmsHandler?.Invoke(this, numberEventArgs);
        }
    }
}