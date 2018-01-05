using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
	using MobileCommunication.Controllers;
	using MobileCommunication.Models;

	public interface IMobileOperator
    {
        List<MobileAccount> MobileAccounts { get; set; }

		List<Account> StandardMobileAccounts { get; set; }

		Logger Logger { get; set; }

        MobileAccount CreateMobileAccount();

        MobileAccount SetAccountParametres(MobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}