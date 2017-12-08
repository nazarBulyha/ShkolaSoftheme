using System;
using System.Collections.Generic;
using System.Linq;
using MobileCommunication.Extensions;

using MobileCommunication.Interfaces;

namespace MobileCommunication
{
    internal class MobileOperator : IMobileOperator
    {
        private IMobileAccount callMaker;
        private IMobileAccount callReceiver;
        private int number = 2219320;

        public List<IMobileAccount> MobileAccounts { get; set; }
        public CallLog CallLogger { get; set; } = new CallLog();
        public AccountEventArgs NumberEventArgs { get; private set; }

        public IMobileAccount CreateAccount(IMobileOperator mobileOperator)
        {
            var account = new MobileAccount(this);

            account.OnStartCallHandler += TryMakeCall;
            account.OnEndCallHandler += EndCall;
            account.OnStartSmsHandler += TrySendSms;
            account.OnEndSmsHandler += ReceiveSms;

            return account;
        }

        public IMobileAccount SetAccountParametres(IMobileAccount account, string name, string surname, string email, DateTime dateTime)
        {
            account.Name = name;
            account.Surname = surname;
            account.Email = email;
            account.DateBirth = dateTime;

            return account;
        }

        public int CreateNumber()
        {
            return number++;
        }

        private void TryMakeCall(object sender, AccountEventArgs e)
        {
            try
            {
<<<<<<< HEAD
                callMaker = (IMobileAccount)sender;
                callReceiver = MobileAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.Number));

                if (callReceiver == null)
                    throw new NullReferenceException();
=======
                _callMaker = sender as IMobileAccount;
                _callReceiver = MobileAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.Number));
>>>>>>> 4c76b679f43b1dfa16701e5fe9a70264c829b7cd

                if (callMaker.Number == 2219320 || callMaker.Number == 0)
                    throw new NullReferenceException();

                if (callReceiver.Number == 2219320 || callReceiver.Number == 0)
                    throw new ArgumentException();
            }
            catch (NullReferenceException)
            {
                callMaker.OnEndCallHandler -= EndCall;
                CallCrashed(e);

                return;
            }
            catch (ArgumentException)
            {
                callMaker.OnEndCallHandler -= EndCall;
                CallCrashed(e);

                return;
            }
            catch (Exception exceptionStandart)
            {
                Console.WriteLine(exceptionStandart.Message);
                callMaker.OnEndCallHandler -= EndCall;
                CallCrashed(e);

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = callMaker.Number,
                ReceiverNumber = callReceiver.Number
            };

            callReceiver.ReceiveCall(callMaker.Number);
        }

        private void TrySendSms(object sender, AccountEventArgs e)
        {
            try
            {
                callMaker = (IMobileAccount)sender;
                callReceiver = MobileAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.Number));

                if (callReceiver == null)
                    throw new NullReferenceException();

                if (callMaker.Number == 2219320 || callMaker.Number == 0)
                    throw new NullReferenceException();

                if (callReceiver.Number == 2219320 || callReceiver.Number == 0)
                    throw new ArgumentException();
            }
            catch (NullReferenceException)
            {
                callMaker.OnEndSmsHandler -= ReceiveSms;
                SmsCrashed(e);

                return;
            }
            catch (ArgumentException)
            {
                callMaker.OnEndSmsHandler -= ReceiveSms;
                SmsCrashed(e);

                return;
            }
            catch (Exception standartException)
            {
                Console.WriteLine(standartException.Message + Environment.NewLine);
                callMaker.OnEndSmsHandler -= ReceiveSms;
                SmsCrashed(e);

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = callMaker.Number,
                ReceiverNumber = callReceiver.Number
            };

            callReceiver.ReceiveSms(callMaker.Number);
        }

        // TODO: Implement logic after receiving Call
        private static void EndCall(object sender, AccountEventArgs e)
        {
            // end call for both users
            // if number doesn't exists, end call for one user

        }

        // TODO: Implement logic after receiving SMS
        private static void ReceiveSms(object sender, AccountEventArgs e)
        {
            // if sender number isn't in blocked numbers than receive sms

        }

        private void CallCrashed(AccountEventArgs e)
        {
            CallLogger.WriteToFile("Call crashed", e.SenderNumber, e.ReceiverNumber, isError : true);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Call to {e.ReceiverNumber} hasn't started.");
            Console.WriteLine($"Number {e.ReceiverNumber} you are trying to deal is not exists.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"For more details open call log. {Environment.NewLine}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SmsCrashed(AccountEventArgs e)
        {
            CallLogger.WriteToFile("Sms wasn't sent. Call receiver isn't exists.", e.SenderNumber, e.ReceiverNumber, isError: true);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Number {e.ReceiverNumber} you are trying to send SMS is not available.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"For more details open call log. {Environment.NewLine}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}