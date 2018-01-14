namespace MobileCommunication.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using MobileCommunication.Extensions;

	[Serializable]
	[UserValidation]
	public class User
	{
		public User() { }

		public User(int number)
		{
			Number = number;
			DateBirth = DateTime.Now;
		}

		public int Number { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public DateTime DateBirth { get; set; }
	}
}