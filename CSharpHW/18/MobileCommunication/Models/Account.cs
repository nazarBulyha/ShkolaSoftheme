using System;

namespace MobileCommunication.Models
{
    internal class Account
    {
        public Account(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }

	    public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateBirth { get; set; } = DateTime.Now;
    }
}
