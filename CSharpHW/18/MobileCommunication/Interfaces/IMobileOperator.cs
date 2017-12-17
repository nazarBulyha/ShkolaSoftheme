﻿using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
	using MobileCommunication.Models;

	public interface IMobileOperator
    {
        List<IMobileAccount> MobileAccounts { get; set; }

		List<Account> StandardMobileAccounts { get; set; }

		ILog Logger { get; set; }

        IMobileAccount CreateMobileAccount();

        IMobileAccount SetAccountParametres(IMobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}