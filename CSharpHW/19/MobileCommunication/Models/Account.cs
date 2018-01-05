using System;

namespace MobileCommunication.Models
{
	public class Account
    {
		public Account() { }

        public Account(int number)
        {
            Number = number;
			DateBirth = DateTime.Now;
		}

        public int Number { get; }

	    public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateBirth { get; set; }
    }
}