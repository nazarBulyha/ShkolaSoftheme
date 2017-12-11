using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileCommunication.Models
{
    [Table("Logger")]
    public class LogMessage
    {
        [Key]
        public int LogMessageId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public string Message { get; set; } = "";

        public int Sender { get; set; } = 0;

        public int Receiver { get; set; } = 0;

        [NotMapped]
        public bool IsError { get; set; } = false;
    }
}