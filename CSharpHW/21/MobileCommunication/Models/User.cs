namespace MobileCommunication.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using MobileCommunication.Extensions;

	using ProtoBuf;

	[UserValidation]
	[ProtoContract]
	public class User
	{
		public User() { }

		public User(int number)
		{
			Number = number;
			DateBirth = DateTime.Now;
		}

		[ProtoMember(1)]
		public int Number { get; set; }

		[Required]
		[ProtoMember(2)]
		public string Name { get; set; }

		[Required]
		[ProtoMember(3)]
		public string Surname { get; set; }

		[Required]
		[ProtoMember(4)]
		public string Email { get; set; }

		[Required]
		[ProtoMember(5)]
		public DateTime DateBirth { get; set; }
	}
}