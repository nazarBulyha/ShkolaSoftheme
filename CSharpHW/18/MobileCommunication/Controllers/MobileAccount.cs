namespace MobileCommunication.Controllers
{
	using System;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class MobileAccount : IMobileAccount
    {
        public Account Account { get; set; }

		public AddressBook AddressBook { get; set; }

        public event EventHandler<AccountEventArgs> OnCallHandler;
        public event EventHandler<AccountEventArgs> OnSmsHandler;

        private AccountEventArgs numberEventArgs;

        public MobileAccount()
        {
	        AddressBook = new AddressBook();
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

			OnCallHandler?.Invoke(this, numberEventArgs);
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

			OnSmsHandler?.Invoke(this, numberEventArgs);
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
				OnCallHandler?.Invoke(this, numberEventArgs);
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

			OnSmsHandler?.Invoke(this, numberEventArgs);
        }
    }
}