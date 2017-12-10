using System;
using System.Collections.Generic;
using System.Linq;
using MobileCommunication.Extensions;

using MobileCommunication.Interfaces;

namespace MobileCommunication
{
    internal class MobileOperator : IMobileOperator
    {
        private IMobileAccount Sender;
        private IMobileAccount Receiver;
        private int number = 2219320;

        public List<IMobileAccount> MobileAccounts { get; set; }
        public AccountEventArgs NumberEventArgs { get; private set; }
        public ILog CallLogger { get; set; } = new CallLog();

        public IMobileAccount CreateAccount(IMobileOperator mobileOperator)
        {
            var account = new MobileAccount(this);
            MobileAccounts.Add(account);

            account.OnStartCallHandler += TryMakeCall;
            account.OnEndCallHandler += EndCall;
            account.OnStartSmsHandler += TrySendSms;
            account.OnEndSmsHandler += ReceiveSms;

            return account;
        }

        public IMobileAccount SetAccountParametres(IMobileAccount mobileAccount, string name, string surname, string email, DateTime dateTime)
        {
            mobileAccount.Account.Name = name;
            mobileAccount.Account.Surname = surname;
            mobileAccount.Account.Email = email;
            mobileAccount.Account.DateBirth = dateTime;

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
                Sender = sender as IMobileAccount;
                Receiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (Sender.Account.Number == 2219320 || Sender.Account.Number == 0)
                    throw new NullReferenceException();

                if (Receiver.Account.Number == 2219320 || Receiver.Account.Number == 0)
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
                Sender = (IMobileAccount)sender;
                Receiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (Receiver == null)
                    throw new NullReferenceException();

                if (Sender.Account.Number == 2219320 || Sender.Account.Number == 0)
                    throw new NullReferenceException();

                if (Receiver.Account.Number == 2219320 || Receiver.Account.Number == 0)
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
            CallLogger.Log(e.SenderNumber, e.ReceiverNumber, message, isError);
        }

        private void LogSmsEvent(string message, AccountEventArgs e, bool isError = false)
        {
            CallLogger.Log(e.SenderNumber, e.ReceiverNumber, message, isError);
        }
    }
}