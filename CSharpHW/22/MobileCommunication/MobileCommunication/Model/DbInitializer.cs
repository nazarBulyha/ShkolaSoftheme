using MobileCommunication.Extensions;
using MobileCommunication.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MobileCommunication.Model
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<MobileCommunicationContext>
    {
        protected override void Seed(MobileCommunicationContext context)
        {
         
        }
    }
}