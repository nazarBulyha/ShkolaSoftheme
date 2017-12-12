namespace MobileCommunication.Controllers
{
	using System;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	internal class MobileAccount : IMobileAccount
    {
        public IMobileOperator Operator { get; }

        public Account Account { get; set; }

		public AddressBook AddressBook { get; set; }

        public event EventHandler<AccountEventArgs> OnStartCallHandler;
        public event EventHandler<AccountEventArgs> OnEndCallHandler;
        public event EventHandler<AccountEventArgs> OnStartSmsHandler;
        public event EventHandler<AccountEventArgs> OnEndSmsHandler;

        private AccountEventArgs numberEventArgs;

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