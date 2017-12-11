using MobileCommunication.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MobileCommunication
{
    public class AddressBook
    {
        [Key]
        public int AddressBookId { get; set; }

        public virtual List<AccocuntDetails> MobileAccounts { get; private set; } = new List<AccocuntDetails>();

        public string GetAccountNameByNumber(int number)
        {
            string accountName = MobileAccounts.SingleOrDefault(mobileAccount => mobileAccount.Number == number) != null ?
                                 MobileAccounts.Where(mobileAccount => mobileAccount.Number == number).Select(account => account.Name).SingleOrDefault() :
                                 number.ToString();

            return accountName;
        }

        public void SetAccount(AccocuntDetails mobileAccount) => MobileAccounts.Add(mobileAccount);

        public void SetAccounts(params AccocuntDetails[] mobileAccounts) => MobileAccounts.AddRange(mobileAccounts);

        public void SetAccounts(List<AccocuntDetails> mobileAccounts) => MobileAccounts.AddRange(mobileAccounts);
    }
}