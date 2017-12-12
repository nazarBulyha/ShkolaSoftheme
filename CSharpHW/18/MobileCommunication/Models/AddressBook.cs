namespace MobileCommunication.Models
{
	using System.Collections.Generic;
	using System.Linq;

	using MobileCommunication.Interfaces;

    internal class AddressBook
    {
        // make address book unique for each user
        protected List<IMobileAccount> NumberList { get; set; } = new List<IMobileAccount>();

        public string GetAccountNameByNumber(int number)
        {
            string accountName = NumberList.SingleOrDefault(mobileAccount => mobileAccount.Account.Number == number) != null ?
								 NumberList.Where(mobileAccount => mobileAccount.Account.Number == number).Select(account => account.Account.Name).SingleOrDefault() :
                                 number.ToString();

            return accountName;
        }

        public void SetAccount(IMobileAccount mobileAccount)
	    {
			NumberList.Add( mobileAccount );
	    }

	    public void SetAccounts(params IMobileAccount[] mobileAccounts)
	    {
			NumberList.AddRange( mobileAccounts );
	    }

	    public void SetAccounts(List<IMobileAccount> mobileAccounts)
	    {
			NumberList.AddRange( mobileAccounts );
	    }
    }
}