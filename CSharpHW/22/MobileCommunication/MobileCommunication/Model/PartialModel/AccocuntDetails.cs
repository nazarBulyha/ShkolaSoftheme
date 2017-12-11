using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileCommunication.Models
{
    [Table("AccocuntDetails")]
    public class AccocuntDetails
    {
        public AccocuntDetails() { }

        public AccocuntDetails(int number)
        {
            Number = number;
        }

        [Key]
        public int AccountId { get; set; }

        public int Number { get; private set; } = 0;

        public string Name { get; set; } = "Ivan";

        public string Surname { get; set; } = "Ivanovych";

        public string Email { get; set; } = "";

        public DateTime DateBirth { get; set; } = DateTime.Now;
    }
}