using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
    internal interface IMobileOperator
    {
        List<IMobileAccount> MobileAccounts { get; set; }
        CallLog CallLogger { get; set; }

        IMobileAccount CreateAccount(IMobileOperator mobileOperator);

        IMobileAccount SetAccountParametres(IMobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}