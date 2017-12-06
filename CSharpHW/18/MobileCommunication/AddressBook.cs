using System.Collections.Generic;

namespace MobileCommunication
{
    class AddressBook
    {
        protected Dictionary<int, string> NumberList { get; set; }

        public AddressBook(params Dictionary<string, int>[] list)
        {
            foreach (var values in list)
            {
                foreach (var element in values)
                {
                    NumberList.Add(element.Value, element.Key);
                }
            }
        }

        public string GetAccountNameByNumber(int number)
        {
            if (NumberList.ContainsKey(number))
            {
                var accountName = "";
                NumberList.TryGetValue(number, out accountName);
                return accountName;
            }
            else
            {
                return number.ToString();
            }
        }
    }
}