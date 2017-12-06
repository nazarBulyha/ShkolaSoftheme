using MobileCommunication.Interfaces;
using MobileCommunication.Extensions;
using System;
using System.Collections.Generic;

namespace MobileCommunication
{
    class Program
    {
        static void Main(string[] args)
        {
            IMobileOperator myOperator = new MobileOperator();
            IMobileAccount tempAccount = new MobileAccount(myOperator);
            IMobileAccount adminAccount = new MobileAccount(myOperator);

            IMobileAccount account1 = new MobileAccount(myOperator);
            IMobileAccount account2 = new MobileAccount(myOperator);
            IMobileAccount account3 = new MobileAccount(myOperator);
            IMobileAccount account4 = new MobileAccount(myOperator);
            IMobileAccount account5 = new MobileAccount(myOperator);
            IMobileAccount account6 = new MobileAccount(myOperator);

            myOperator.MobileAccounts = new List<IMobileAccount>();
            myOperator.MobileAccounts.AddMany(account1, account2, account3, account4, account5, account6);

            account1.MakeCall(adminAccount.Number);
            account1.MakeCall(tempAccount.Number);
            account1.MakeCall(account2.Number);
            account3.MakeCall(account4.Number);
            account5.MakeCall(account6.Number);

            Console.ReadKey();
        }
    }
}