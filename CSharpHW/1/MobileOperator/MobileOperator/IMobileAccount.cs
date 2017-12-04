namespace MobileOperator
{
    abstract class IMobileAccount : IMobileOperator
    {
        public int Number { get; set; } = 0000000;
        bool IsRegister { get; set; }
        public bool IsBusy { get; set; } = false;
        protected abstract AddressBook AddressBook { get; set; }

        public IMobileAccount(int number, bool isRegister)
        {
            //add to operator list
            Number = number;
            IsRegister = isRegister;
        }

        public virtual void SendSMS()
        {

        }
        
        public virtual void ReceiveSMS()
        {

        }

        public virtual void MakeCall(IMobileAccount sourceAccount, IMobileAccount destinationAccount, IMobileOperator _operator)
        {
            _operator.CallReceiver += sourceAccount.MakeCall;
            _operator.CallReceiver += destinationAccount.ReceiveCall;
            //_operator.StartCall(sourceAccount, destinationAccount, _operator);
        }

        private void ReceiveCall(IMobileAccount sourceAccount, IMobileAccount destinationAccount, IMobileOperator _operator)
        {
            //DoSmth;
        }
    }
}