using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
    internal interface IMobileOperator
    {
        List<IMobileAccount> MobileAccounts { get; set; }
	    List<IMobileAccount> StandardMobileAccounts { get; set; }

		ILog CallLogger { get; set; }

        IMobileAccount CreateMobileAccount(IMobileOperator mobileOperator);

        IMobileAccount SetAccountParametres(IMobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}