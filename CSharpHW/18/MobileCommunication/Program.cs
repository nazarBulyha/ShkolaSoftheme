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
            myOperator.SetAccountParametres(vasyl, "Vasyl", "Vasylovych", "vasyl.vasylovych@gmail.com", new DateTime(1987, 10, 24));
            vasyl.Name = "Vasyl";
            vasyl.Surname = "Vasylovych";
            vasyl.Email = "vasyl.vasylovych@gmail.com";
            vasyl.DateBirth = new DateTime(1987, 10, 24);   

            var petro = myOperator.CreateAccount(myOperator);
            petro.Name = "Petro";
            petro.Surname = "Petrovych";
            petro.Email = "petro.petrovych@gmail.com";
            petro.DateBirth = new DateTime(1988, 01, 15);

            var taras = myOperator.CreateAccount(myOperator);
            taras.Name = "Taras";
            taras.Surname = "Tarasovych";
            taras.Email = "taras.tarasovych@gmail.com";
            taras.DateBirth = new DateTime(1991, 01, 28);

            var nazar = myOperator.CreateAccount(myOperator);
            nazar.Name = "Nazar";
            nazar.Surname = "Nazarovych";
            nazar.Email = "nazar.nazarovych@gmail.com";
            nazar.DateBirth = new DateTime(1997, 06, 10);

            var igor = myOperator.CreateAccount(myOperator);
            igor.Name = "Igor";
            igor.Surname = "Igorovych";
            igor.Email = "igor.igorovych@gmail.com";
            igor.DateBirth = new DateTime(1997, 01, 28);

            var andriy = myOperator.CreateAccount(myOperator);
            andriy.Name = "Andriy";
            andriy.Surname = "Andriyovych";
            andriy.Email = "andriy.andriyovych@gmail.com";
            andriy.DateBirth = new DateTime(1997, 10, 16);


            myOperator.MobileAccounts.AddMany(vasyl, petro, taras, nazar, igor, andriy);

            vasyl.MakeCall(adminAccount.Number);
            vasyl.MakeCall(tempAccount.Number);
            vasyl.MakeCall(petro.Number);
            taras.MakeCall(nazar.Number);
            igor.MakeCall(andriy.Number);

            vasyl.SendSms(tempAccount.Number);
            tempAccount.SendSms(vasyl.Number);

            vasyl.SendSms(petro.Number);
            taras.SendSms(nazar.Number);
            igor.SendSms(andriy.Number);

            Console.ReadKey();
        }
    }
}