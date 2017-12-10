using MobileCommunication.Interfaces;
using System;
using System.Collections.Generic;

namespace MobileCommunication
{
    internal class Program
    {
        private static void Main()
        {
            IMobileOperator myOperator = new MobileOperator();

            var tempAccount = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(tempAccount, "tempAccount", "tempAccount", "", new DateTime(1988, 01, 15));

            var adminAccount = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(adminAccount, "adminAccount", "adminAccount", "", new DateTime(1977, 01, 15));

            #region Initializing mobile accounts
            var vasyl = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: vasyl,
                                            name: "Vasyl",
                                            surname: "Vasylovych",
                                            email: "vasyl.vasylovych@gmail.com",
                                            dateTime: new DateTime(1987, 10, 24));

            var petro = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: petro, 
                                            name: "Petro",
                                            surname:"Petrovych", 
                                            email:"petro.petrovych@gmail.com", 
                                            dateTime: new DateTime(1988, 01, 15));

            var taras = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: taras, 
                                            name: "Taras", 
                                            surname: "Tarasovych", 
                                            email: "taras.tarasovych@gmail.com", 
                                            dateTime: new DateTime(1991, 01, 28));

            var nazar = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: nazar, 
                                            name: "Nazar", 
                                            surname: "Nazarovych", 
                                            email: "nazar.nazarovych@gmail.com", 
                                            dateTime: new DateTime(1997, 06, 10));

            var igor = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: igor, 
                                            name: "Igor", 
                                            surname: "Igorovych", 
                                            email: "igor.igorovych@gmail.com", 
                                            dateTime: new DateTime(1997, 01, 28));

            var andriy = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(account: andriy, 
                                            name: "Andriy", 
                                            surname: "Andriyovych", 
                                            email: "andriy.andriyovych@gmail.com", 
                                            dateTime: new DateTime(1997, 10, 16));

            List<IMobileAccount> mobileAccountsForAddressBook = new List<IMobileAccount>
            {
                vasyl, petro, taras, nazar, igor, andriy
            };
            #endregion

            #region Set AddressBook contacts
            vasyl.Account.AddressBook.SetAccounts(petro, taras, nazar, igor, andriy);
            petro.Account.AddressBook.SetAccounts(vasyl, taras, nazar, igor, andriy);
            taras.Account.AddressBook.SetAccounts(vasyl, petro, nazar, igor, andriy);
            nazar.Account.AddressBook.SetAccounts(vasyl, petro, taras, igor, andriy);
            igor.Account.AddressBook.SetAccounts(vasyl, petro, taras, nazar, andriy);
            andriy.Account.AddressBook.SetAccounts(vasyl, petro, taras, nazar, igor);
            #endregion

            #region Account actions via Operator
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
            #endregion

            // TODO: CallLog ReadFromFile and write to console
            CallLog callLog = new CallLog();
            callLog.ShowAllLog();

            Console.ReadKey();
        }
    }
}