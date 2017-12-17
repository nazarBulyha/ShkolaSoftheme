using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
	using System.Xml.Serialization;

	using MobileCommunication.Models;

	public interface IMobileOperator
    {
        List<IMobileAccount> MobileAccounts { get; set; }

		List<Account> StandardMobileAccounts { get; set; }

		[XmlIgnore]
		ILog Logger { get; set; }

        IMobileAccount CreateMobileAccount();

        IMobileAccount SetAccountParametres(IMobileAccount account, string name, string surname, string email, DateTime dateTime);

        int CreateNumber();
    }
}