namespace MobileCommunication.Migrations
{
    using MobileCommunication.Extensions;
    using MobileCommunication.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MobileCommunicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MobileCommunicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            MobileOperator myOperator = new MobileOperator();

            #region Define standard accounts for address book
            MobileAccount standartAccount1 = new MobileAccount(myOperator);
            MobileAccount standartAccount2 = new MobileAccount(myOperator);
            MobileAccount standartAccount3 = new MobileAccount(myOperator);
            MobileAccount standartAccount4 = new MobileAccount(myOperator);

            standartAccount1.Account = new AccocuntDetails(myOperator.CreateNumber())
            {
                Name = "standartName1",
                Surname = "standartSurname1",
                DateBirth = DateTime.Now
            };
            standartAccount2.Account = new AccocuntDetails(myOperator.CreateNumber())
            {
                Name = "standartName2",
                Surname = "standartSurname2",
                DateBirth = DateTime.Now
            };
            standartAccount3.Account = new AccocuntDetails(myOperator.CreateNumber())
            {
                Name = "standartName3",
                Surname = "standartSurname3",
                DateBirth = DateTime.Now
            };
            standartAccount4.Account = new AccocuntDetails(myOperator.CreateNumber())
            {
                Name = "standartName4",
                Surname = "standartSurname4",
                DateBirth = DateTime.Now
            };

            // here we add standard accounts to Operator
            myOperator.MobileAccounts.AddMany(standartAccount1, standartAccount2, standartAccount3, standartAccount4);
            #endregion

            #region Initializing mobile accounts
            var tempAccount = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(tempAccount, "tempAccount", "tempAccount", "", new DateTime(1988, 01, 15));

            var adminAccount = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(adminAccount, "adminAccount", "adminAccount", "", new DateTime(1977, 01, 15));

            var vasyl = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: vasyl,
                                            name: "Vasyl",
                                            surname: "Vasylovych",
                                            email: "vasyl.vasylovych@gmail.com",
                                            dateTime: new DateTime(1987, 10, 24));

            var petro = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: petro,
                                            name: "Petro",
                                            surname: "Petrovych",
                                            email: "petro.petrovych@gmail.com",
                                            dateTime: new DateTime(1988, 01, 15));

            var taras = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: taras,
                                            name: "Taras",
                                            surname: "Tarasovych",
                                            email: "taras.tarasovych@gmail.com",
                                            dateTime: new DateTime(1991, 01, 28));

            var nazar = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: nazar,
                                            name: "Nazar",
                                            surname: "Nazarovych",
                                            email: "nazar.nazarovych@gmail.com",
                                            dateTime: new DateTime(1997, 06, 10));

            var igor = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: igor,
                                            name: "Igor",
                                            surname: "Igorovych",
                                            email: "igor.igorovych@gmail.com",
                                            dateTime: new DateTime(1997, 01, 28));

            var andriy = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(mobileAccount: andriy,
                                            name: "Andriy",
                                            surname: "Andriyovych",
                                            email: "andriy.andriyovych@gmail.com",
                                            dateTime: new DateTime(1997, 10, 16));
            #endregion

            // Add to the context
            context.MobileOperator.AddOrUpdate(myOperator);

            foreach (var mobileAccount in myOperator.MobileAccounts)
            {
                context.Accounts.AddOrUpdate(mobileAccount.Account);
            }

            foreach (var mobileAccount in myOperator.MobileAccounts)
            {
                context.MobileAccounts.AddOrUpdate(mobileAccount);
            }

            foreach (var mobileAccount in myOperator.MobileAccounts)
            {
                context.AddressBook.AddOrUpdate(mobileAccount.AddressBook);
            }
        }
    }
}