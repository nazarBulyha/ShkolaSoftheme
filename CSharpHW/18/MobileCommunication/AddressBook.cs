using MobileCommunication.Interfaces;
using System.Collections.Generic;

namespace MobileCommunication
{
    using System.Linq;

    internal class AddressBook
    {
        protected List<IMobileAccount> NumberList { get; set; }

        public AddressBook(params IMobileAccount[] mobileAccounts)
        {
            NumberList.AddRange(mobileAccounts);
        }

        public string GetAccountNameByNumber(int number)
        {
            var accountName = NumberList.Select(account => account.Number == number).ToString();

            return accountName;
        }
    }
}