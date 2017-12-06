using System.Collections.Generic;
using MobileCommunication.Interfaces;

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
            _callMaker = (IMobileAccount)sender;
            _callReceiver = new MobileAccount(this);

            //check if account exists. If not end call for sender, else get receiver from accounts list
            foreach (var account in MobileAccounts)
            {
                if (e.ReceiverNumber.Equals(account.Number))
                {
                    _callReceiver = account;
                    break;
                }
            }
            if(_callReceiver.Number == 2219320)
            {
                //say user that number doesn't exists
                System.Console.WriteLine($"Number {e.ReceiverNumber} you are trying to deal is not available.");

                EndCall(this, e);
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

        public void TryReceiveSMS(object sender, AccountEventArgs e)
        {

        }

        public int CreateNumber()
        {
            return Number++;
        }
    }
}