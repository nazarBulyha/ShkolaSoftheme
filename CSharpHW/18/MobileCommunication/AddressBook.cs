using MobileCommunication.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MobileCommunication
{
    internal class AddressBook
    {
        // make address book unique for each user
        protected List<IMobileAccount> NumberList { get; set; } = new List<IMobileAccount>();

        public string GetAccountNameByNumber(int number)
        {
            string accountName = NumberList.SingleOrDefault(account => account.Number == number) != null ?
                                 NumberList.Where(account => account.Number == number).Select(account => account.Name).SingleOrDefault() :
                                 number.ToString();

            return accountName;
        }

        public void SetAccount(IMobileAccount mobileAccount)
        {
            NumberList.Add(mobileAccount);
        }

        public void SetAccounts(List<IMobileAccount> mobileAccounts)
        {
            NumberList.AddRange(mobileAccounts);
        }
    }
}