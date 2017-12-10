using System;

namespace MobileCommunication.Models
{
    internal class Account
    {
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            private set
            {

            }
        }

        public string Name { get; set; } = "Ivan";

        public string Surname { get; set; } = "Ivanovych";

        public string Email { get; set; } = "";

        public DateTime DateBirth { get; set; } = DateTime.Now;

        public AddressBook AddressBook { get; set; } = new AddressBook();
    }
}
