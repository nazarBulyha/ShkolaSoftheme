using MobileCommunication.Models;
using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
    public interface IMobileOperator
    {
        List<MobileAccount> MobileAccounts { get; set; }
        ILog CallLogger { get; set; }
        int OperatorId { get; set; }

        MobileAccount CreateMobileAccount(MobileOperator mobileOperator);

        MobileAccount SetAccountParametres(MobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}