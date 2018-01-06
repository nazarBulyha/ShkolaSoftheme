namespace MobileCommunication.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	[Serializable]
	public class AddressBook
    {
        public List<Account> NumberList { get; } = new List<Account>();

        public string GetAccountNameByNumber(int number)
        {
            var accountName = NumberList.SingleOrDefault(mobileAccount => mobileAccount.Number == number) != null ?
														 NumberList.Where(mobileAccount => mobileAccount.Number == number)
																   .Select(account => account.Name)
																   .SingleOrDefault() :
														 number.ToString();

            return accountName;
        }

	    public void SetAccounts(params Account[] mobileAccounts)
	    {
			NumberList.AddRange( mobileAccounts );
	    }

	    public void SetAccounts(List<Account> mobileAccounts)
	    {
			NumberList.AddRange( mobileAccounts );
	    }
    }
}