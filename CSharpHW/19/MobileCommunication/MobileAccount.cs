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
                SenderNumber = this.Number,
                ReceiverNumber = number
            };

            OnStartCallHandler += _mobileOperator.TryMakeCall;

            OnStartCallHandler?.Invoke(this, numberEventArgs);
        }

        public void ReceiveCall(int number)
        {
            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = number,
                ReceiverNumber = this.Number
            };

            //TODO: make logic with address book
            Console.WriteLine($"Call received from {number}");

            OnStartCallHandler -= _mobileOperator.TryMakeCall;
            OnEndCallHandler += _mobileOperator.EndCall;

            //if I want to receive a call
            if (true)
            {
                OnEndCallHandler?.Invoke(this, numberEventArgs);
                OnEndCallHandler -= _mobileOperator.EndCall;
            }
        }

        public void SendSMS(int number)
        {

        }

        public void ReceiveSMS(int number)
        {

        }
    }
}
