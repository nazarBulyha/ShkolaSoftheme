using MobileCommunication.Model;
using System.Data.Entity;

namespace MobileCommunication.Models
{
    public class MobileCommunicationContext : DbContext
    {
        public MobileCommunicationContext() : base("MobileCommunication")
        {
        }

        public DbSet<AddressBook> AddressBook { get; set; }
        public DbSet<AccocuntDetails> Accounts { get; set; }
        public DbSet<MobileAccount> MobileAccounts { get; set; }
        public DbSet<MobileOperator> MobileOperator { get; set; }
        public DbSet<LogMessage> LogMessage { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}
