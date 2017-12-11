using System;
using System.Collections.Generic;
using System.Linq;
using MobileCommunication.Extensions;

using MobileCommunication.Interfaces;
using MobileCommunication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileCommunication
{
    public class MobileOperator : IMobileOperator
    {
        private IMobileAccount Sender;
        private IMobileAccount Receiver;
        private int number = 1000000;

        [Key]
        public int OperatorId { get; set; }

        public List<MobileAccount> MobileAccounts { get; set; } = new List<MobileAccount>();

        [NotMapped]
        public AccountEventArgs NumberEventArgs { get; private set; }
        [NotMapped]
        public ILog CallLogger { get; set; } = new Logger();

        public MobileAccount CreateMobileAccount(MobileOperator mobileOperator)
        {
            var mobileAccount = new MobileAccount(this);

            mobileAccount.OnStartCallHandler += TryMakeCall;
            mobileAccount.OnEndCallHandler += EndCall;
            mobileAccount.OnStartSmsHandler += TrySendSms;
            mobileAccount.OnEndSmsHandler += ReceiveSms;

            return mobileAccount;
        }

        public MobileAccount SetAccountParametres(MobileAccount mobileAccount, string name, string surname, string email, DateTime dateTime)
        {
            var account = new AccocuntDetails(CreateNumber())
            {
                Name = name,
                Surname = surname,
                Email = email,
                DateBirth = dateTime
            };
            mobileAccount.AddressBook.SetAccounts(mobileAccount.Account);

            mobileAccount.Account = account;
            MobileAccounts.Add(mobileAccount);

            return mobileAccount;
        }

        public int CreateNumber()
        {
            return number++;
        }

        private void TryMakeCall(object sender, AccountEventArgs e)
        {
            try
            {
                // TODO: Fix: sender <=> receiver bag
                Sender = sender as MobileAccount;
                Receiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (Receiver.Account.Number == 0 || Sender.Account.Number == 0 || Sender == null || Receiver == null || Sender.Account == null || Receiver.Account == null)
                    throw new NullReferenceException();

                if (Sender.Account.Number == 2219324 || Sender.Account.Number == 2219325 || Receiver.Account.Number == 2219324 || Receiver.Account.Number == 2219325)
                    throw new ArgumentException();
            }
            catch (NullReferenceException)
            {
                Sender.OnEndCallHandler -= EndCall;
                LogCallEvent("Call crashed.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call crashed");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }
            catch (ArgumentException)
            {
                Sender.OnEndCallHandler -= EndCall;
                LogCallEvent("Call crashed.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call crashed");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }
            catch (Exception exceptionStandart)
            {
                Console.WriteLine(exceptionStandart.Message);
                Sender.OnEndCallHandler -= EndCall;
                LogCallEvent("Call crashed.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call crashed");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = Sender.Account.Number,
                ReceiverNumber = Receiver.Account.Number
            };

            LogCallEvent("Try to call", e);

            Receiver.ReceiveCall(Sender.Account.Number);
        }

        private void TrySendSms(object sender, AccountEventArgs e)
        {
            try
            {
                Sender = (MobileAccount)sender;
                Receiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (Receiver.Account.Number == 0 || Sender.Account.Number == 0 || Sender == null || Receiver == null || Sender.Account == null || Receiver.Account == null)
                    throw new NullReferenceException();

                if (Sender.Account.Number == 2219324 || Sender.Account.Number == 2219325 || Receiver.Account.Number == 2219324 || Receiver.Account.Number == 2219325)
                    throw new ArgumentException();
            }
            catch (NullReferenceException)
            {
                Sender.OnEndSmsHandler -= ReceiveSms;
                LogSmsEvent("Sms wasn't send.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sms wasn't send.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }
            catch (ArgumentException)
            {
                Sender.OnEndSmsHandler -= ReceiveSms;
                LogSmsEvent("Sms wasn't send.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sms wasn't send.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }
            catch (Exception standartException)
            {
                Console.WriteLine(standartException.Message + Environment.NewLine);
                Sender.OnEndSmsHandler -= ReceiveSms;
                LogSmsEvent("Sms wasn't send.", e, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sms wasn't send.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = Sender.Account.Number,
                ReceiverNumber = Receiver.Account.Number
            };

            LogSmsEvent("Try to send sms", e);

            Receiver.ReceiveSms(Sender.Account.Number);
        }

        // TODO: Implement logic after receiving Call
        private void EndCall(object sender, AccountEventArgs e)
        {
            // end call for both users
            // if number doesn't exists, end call for one user
            LogSmsEvent("Call ended.", e);
        }

        private void ReceiveSms(object sender, AccountEventArgs e)
        {
            // if sender number isn't in blocked numbers than receive sms
            LogSmsEvent("Sms received.", e);
        }

        private void LogCallEvent(string message, AccountEventArgs e, bool isError = false)
        {
            Console.WriteLine($"Sender {e.SenderNumber} sent a message - {message} to {e.ReceiverNumber}.");
            CallLogger.Log(e.SenderNumber, e.ReceiverNumber, message, isError);
        }

        private void LogSmsEvent(string message, AccountEventArgs e, bool isError = false)
        {
            CallLogger.Log(e.SenderNumber, e.ReceiverNumber, message, isError);
        }
    }
}