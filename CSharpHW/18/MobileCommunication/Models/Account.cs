using System;

namespace MobileCommunication.Models
{
	using System.Runtime.Serialization;

	[DataContract]
	public class Account
    {
        public Account(int number)
        {
            Number = number;
        }

		[DataMember]
        public int Number { get; private set; }

		[DataMember]
	    public string Name { get; set; }

		[DataMember]
        public string Surname { get; set; }

		[DataMember]
        public string Email { get; set; }

		[DataMember]
        public DateTime DateBirth { get; set; } = DateTime.Now;
    }
}
