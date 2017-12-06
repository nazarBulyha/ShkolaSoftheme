using System.Collections.Generic;
using MobileCommunication.Interfaces;
using System.Linq;
using System;

namespace MobileCommunication
{
    class MobileOperator : IMobileOperator
    {
        public MobileOperator()
        {
        }

        private int Number = 2219320;

        public List<IMobileAccount> MobileAccounts { get; set; }
        public CallLog CallLogs { get; set; }

        private IMobileAccount _callMaker;
        private IMobileAccount _callReceiver;
        AccountEventArgs numberEventArgs;

        public void TryMakeCall(object sender, AccountEventArgs e)
        {
            try
            {
                _callMaker = (IMobileAccount)sender;
                _callReceiver = MobileAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.Number));

                if (_callReceiver == null)
                    throw new NullReferenceException();

                if (_callReceiver.Number == 2219320 || _callReceiver.Number == 0)
                    throw new ArgumentException();
            }
            catch (NullReferenceException nullException)
            {
                //add error to log
                //Console.WriteLine(nullException.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Number {e.ReceiverNumber} you are trying to deal is not exists.");
                Console.ForegroundColor = ConsoleColor.Gray;

                CallCrashed(e);
                return;
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Number {e.ReceiverNumber} you are trying to deal is not available.");

                CallCrashed(e);
                return;
            }
            catch (Exception exceptionStandart)
            {
                Console.WriteLine(exceptionStandart.Message);
                return;
            }

            numberEventArgs = new AccountEventArgs
            {
                SenderNumber = _callMaker.Number,
                ReceiverNumber = _callReceiver.Number
            };

            _callReceiver.ReceiveCall(_callMaker.Number);
        }

        //TODO: realize method
        public void EndCall(object sender, AccountEventArgs e)
        {
            //end call for both users
            //if number doesn't exists, end call for one user

        }

        private void CallCrashed(AccountEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Call to {e.ReceiverNumber} hasn't started.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"For more details open call log.");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine();
        }

        public void TryReceiveSMS(object sender, AccountEventArgs e)
        {

        }

        public int CreateNumber()
        {
            return Number++;
        }
    }
}