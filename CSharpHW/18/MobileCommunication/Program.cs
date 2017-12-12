using MobileCommunication.Interfaces;
using System;

namespace MobileCommunication
{
	using MobileCommunication.Controllers;

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
            myOperator.SetAccountParametres(vasyl,
                                            "Vasyl",
                                            "Vasylovych",
                                            "vasyl.vasylovych@gmail.com",
                                            new DateTime(1987, 10, 24));

            var petro = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(petro, 
                                            "Petro",
                                            "Petrovych", 
                                            "petro.petrovych@gmail.com", 
                                            new DateTime(1988, 01, 15));

            var taras = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(taras, 
                                            "Taras", 
                                            "Tarasovych", 
                                            "taras.tarasovych@gmail.com", 
                                            new DateTime(1991, 01, 28));

            var nazar = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(nazar, 
                                            "Nazar", 
                                            "Nazarovych", 
                                            "nazar.nazarovych@gmail.com", 
                                            new DateTime(1997, 06, 10));

            var igor = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(igor, 
                                            "Igor", 
                                            "Igorovych", 
                                            "igor.igorovych@gmail.com", 
                                            new DateTime(1997, 01, 28));

            var andriy = myOperator.CreateMobileAccount(myOperator);
            myOperator.SetAccountParametres(andriy, 
                                            "Andriy", 
                                            "Andriyovych", 
                                            "andriy.andriyovych@gmail.com", 
                                            new DateTime(1997, 10, 16));

            #endregion

            #region Set AddressBook contacts
            vasyl.AddressBook.SetAccounts(petro, taras, nazar, igor, andriy);
            petro.AddressBook.SetAccounts(vasyl, taras, nazar, igor, andriy);
            taras.AddressBook.SetAccounts(vasyl, petro, nazar, igor, andriy);
            nazar.AddressBook.SetAccounts(vasyl, petro, taras, igor, andriy);
            igor.AddressBook.SetAccounts(vasyl, petro, taras, nazar, andriy);
            andriy.AddressBook.SetAccounts(vasyl, petro, taras, nazar, igor);
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

            Logger callLog = new Logger();
            callLog.ShowAllLog();

            Console.ReadKey();
        }
    }
}