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
            MobileAccounts.Add(account);

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
                callMaker = sender as IMobileAccount;
                callReceiver = MobileAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.Number));

                if (callMaker.Number == 2219320 || callMaker.Number == 0)
                    throw new NullReferenceException();

                if (callReceiver.Number == 2219320 || callReceiver.Number == 0)
                    throw new ArgumentException();
            }
            catch (NullReferenceException)
            {
                callMaker.OnEndCallHandler -= EndCall;
                LogCallEvent(e, "Call crashed.", true);

                return;
            }
            catch (ArgumentException)
            {
                callMaker.OnEndCallHandler -= EndCall;
                LogCallEvent(e, "Call crashed.", true);

                return;
            }
            catch (Exception exceptionStandart)
            {
                Console.WriteLine(exceptionStandart.Message);
                callMaker.OnEndCallHandler -= EndCall;
                LogCallEvent(e, "Call crashed.", true);

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = callMaker.Number,
                ReceiverNumber = callReceiver.Number
            };

            LogCallEvent(e, "Try to call");

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
                LogSmsEvent(e, "Sms wasn't send.", true);

                return;
            }
            catch (ArgumentException)
            {
                callMaker.OnEndSmsHandler -= ReceiveSms;
                LogSmsEvent(e, "Sms wasn't send.", true);


                return;
            }
            catch (Exception standartException)
            {
                Console.WriteLine(standartException.Message + Environment.NewLine);
                callMaker.OnEndSmsHandler -= ReceiveSms;
                LogSmsEvent(e, "Sms wasn't send.", true);

                return;
            }

            NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = callMaker.Number,
                ReceiverNumber = callReceiver.Number
            };

            LogSmsEvent(e, "Try to send sms");

            callReceiver.ReceiveSms(callMaker.Number);
        }

        // TODO: Implement logic after receiving Call
        private void EndCall(object sender, AccountEventArgs e)
        {
            // end call for both users
            // if number doesn't exists, end call for one user
            LogSmsEvent(e, "Call ended.");
        }

        private void ReceiveSms(object sender, AccountEventArgs e)
        {
            // if sender number isn't in blocked numbers than receive sms
            LogSmsEvent(e, "Sms received.");
        }

        private void LogCallEvent(AccountEventArgs e, string message, bool isError = false)
        {
            CallLogger.WriteToFile(message, e.SenderNumber, e.ReceiverNumber, isError);
        }

        private void LogSmsEvent(AccountEventArgs e, string message, bool isError = false)
        {
            CallLogger.WriteToFile(message, e.SenderNumber, e.ReceiverNumber, isError);
        }
    }
}