using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;
using System;
using System.Collections.Generic;

namespace MobileCommunication
{
    internal class Program
    {
        private static void Main()
        {
            IMobileOperator myOperator = new MobileOperator
            {
                MobileAccounts = new List<IMobileAccount>()
            };

            var tempAccount = myOperator.CreateAccount(myOperator);
            var adminAccount = myOperator.CreateAccount(myOperator);

            var vasyl = myOperator.CreateAccount(myOperator);
            myOperator.SetAccountParametres(account: vasyl, 
                                            name: "Vasyl", 
                                            surname: "Vasylovych", 
                                            email: "vasyl.vasylovych@gmail.com", 
                                            dateTime: new DateTime(1987, 10, 24));

            var petro = myOperator.CreateAccount(myOperator);
            var taras = myOperator.CreateAccount(myOperator);
            var nazar = myOperator.CreateAccount(myOperator);
            var igor = myOperator.CreateAccount(myOperator);
            var andriy = myOperator.CreateAccount(myOperator);

            List<IMobileAccount> mobileAccountsForAddressBook = new List<IMobileAccount>
            {
                vasyl, petro, taras, nazar, igor, andriy
            };

            myOperator.SetAccountParametres(petro, "Petro", "Petrovych", "petro.petrovych@gmail.com", new DateTime(1988, 01, 15));
            myOperator.SetAccountParametres(taras, "Taras", "Tarasovych", "taras.tarasovych@gmail.com", new DateTime(1991, 01, 28));
            myOperator.SetAccountParametres(nazar, "Nazar", "Nazarovych", "nazar.nazarovych@gmail.com", new DateTime(1997, 06, 10));
            myOperator.SetAccountParametres(igor, "Igor", "Igorovych", "igor.igorovych@gmail.com", new DateTime(1997, 01, 28));
            myOperator.SetAccountParametres(andriy, "Andriy", "Andriyovych", "andriy.andriyovych@gmail.com", new DateTime(1997, 10, 16));
            
            // set standard address book contacts
            petro.Account.AddressBook.SetAccounts(mobileAccountsForAddressBook);
            taras.Account.AddressBook.SetAccounts(mobileAccountsForAddressBook);
            nazar.Account.AddressBook.SetAccounts(mobileAccountsForAddressBook);
            igor.Account.AddressBook.SetAccounts(mobileAccountsForAddressBook);
            andriy.Account.AddressBook.SetAccounts(mobileAccountsForAddressBook);

            vasyl.MakeCall(adminAccount.Account.Number);
            vasyl.MakeCall(tempAccount.Account.Number);
            vasyl.MakeCall(petro.Account.Number);
            taras.MakeCall(nazar.Account.Number);
            igor.MakeCall(andriy.Account.Number);

            vasyl.SendSms(tempAccount.Account.Number);
            tempAccount.SendSms(vasyl.Account.Number);

            vasyl.SendSms(petro.Account.Number);
            taras.SendSms(nazar.Account.Number);
            igor.SendSms(andriy.Account.Number);

            // TODO: CallLog ReadFromFile and write to console

            Console.ReadKey();
        }
    }
}