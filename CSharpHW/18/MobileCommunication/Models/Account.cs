using System;

namespace MobileCommunication.Models
{
	[Serializable]
	public class Account
    {
        public Account(int number)
        {
            Number = number;
        }

        public int Number { get; }

	    public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateBirth { get; set; } = DateTime.Now;
    }
}
