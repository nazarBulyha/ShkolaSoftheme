﻿using MobileCommunication.Interfaces;
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
            myOperator.SetAccountParametres(vasyl, "Petro", "Petrovych", "petro.petrovych@gmail.com", new DateTime(1988, 01, 15));

            var taras = myOperator.CreateAccount(myOperator);
            myOperator.SetAccountParametres(vasyl, "Taras", "Tarasovych", "taras.tarasovych@gmail.com", new DateTime(1991, 01, 28));

            var nazar = myOperator.CreateAccount(myOperator);
            myOperator.SetAccountParametres(vasyl, "Nazar", "Nazarovych", "nazar.nazarovych@gmail.com", new DateTime(1997, 06, 10));

            var igor = myOperator.CreateAccount(myOperator);
            myOperator.SetAccountParametres(vasyl, "Igor", "Igorovych", "igor.igorovych@gmail.com", new DateTime(1997, 01, 28));

            var andriy = myOperator.CreateAccount(myOperator);
            myOperator.SetAccountParametres(vasyl, "Andriy", "Andriyovych", "andriy.andriyovych@gmail.com", new DateTime(1997, 10, 16));

            //myOperator.MobileAccounts.AddMany(vasyl, petro, taras, nazar, igor, andriy);

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

            // TODO: CallLog ReadFromFile and write to console

            Console.ReadKey();
        }
    }
}