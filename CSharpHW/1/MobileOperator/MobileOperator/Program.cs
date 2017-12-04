namespace MobileOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            MobileOperator _operator = new MobileOperator();
            MobileOperator _operator1 = new MobileOperator();
            IMobileAccount _mobileAccount = new MobileAccount(1234567, true);
            IMobileAccount _mobileAccount1 = new MobileAccount(7654321, true);
            IMobileAccount _mobileAccount2 = new MobileAccount(2219320, true);
            IMobileAccount _mobileAccount3 = new MobileAccount(0239122, false);
            _operator.AccountList.Add(_mobileAccount);
            _operator.AccountList.Add(_mobileAccount1);
            _operator1.AccountList.Add(_mobileAccount2);
            _operator1.AccountList.Add(_mobileAccount3);

            _mobileAccount.MakeCall(_mobileAccount, _mobileAccount1, _operator);
            _mobileAccount.MakeCall(_mobileAccount2, _mobileAccount3, _operator1);

            _operator.StartCall(_mobileAccount, _mobileAccount1, _operator);
        }
    }
}