using System;

namespace MobileCommunication.Models
{
	[Serializable]
	public class User
	{
		public User() { }

		public User(int number)
		{
			Number = number;
			DateBirth = DateTime.Now;
		}

		public int Number { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Email { get; set; }

		public DateTime DateBirth { get; set; }
	}
}