namespace MobileOperator
{
    class MobileAccount : IMobileAccount
    {
        public MobileAccount(int number, bool isRegister) : base(number, isRegister)
        {
        }

        protected override AddressBook AddressBook { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
