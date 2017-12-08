using MobileCommunication.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MobileCommunication
{
    internal class AddressBook
    {
        protected List<IMobileAccount> NumberList { get; set; } = new List<IMobileAccount>();

        public AddressBook(params IMobileAccount[] mobileAccounts)
        {
            NumberList.AddRange(mobileAccounts);
        }

        public AddressBook(List<IMobileAccount> mobileAccounts)
        {
            NumberList.AddRange(mobileAccounts);
        }

        public string GetAccountNameByNumber(int number)
        {
            var accountName = ContainsAccount(number) == true ? 
                              NumberList.Select(account => account.Number == number).ToString() :
                              number.ToString();

            return accountName;
        }

        // return string type null or account Name
        public bool ContainsAccount(int number)
        {
            var contains = NumberList.SingleOrDefault(account => account.Number == number);

            return contains == null ? false : true;
        }
    }
}