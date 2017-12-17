namespace MobileCommunication.Models
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	[DataContract]
	public class AddressBook
    {
        // make address book unique for each user
		[DataMember]
        protected List<Account> NumberList { get; set; } = new List<Account>();

        public string GetAccountNameByNumber(int number)
        {
            string accountName = NumberList.SingleOrDefault(mobileAccount => mobileAccount.Number == number) != null ?
								 NumberList.Where(mobileAccount => mobileAccount.Number == number).Select(account => account.Name).SingleOrDefault() :
                                 number.ToString();

            return accountName;
        }

        public void SetAccount(Account mobileAccount)
	    {
			NumberList.Add( mobileAccount );
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