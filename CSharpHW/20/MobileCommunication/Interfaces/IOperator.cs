using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
	using MobileCommunication.Controllers;

	public interface IOperator
	{
		List<Account> ListAccounts { get; set; }

		List<Account> ListStandardAccounts { get; set; }

		Account CreateMobileAccount();

		Account SetAccountParameters(Account account, string name, string surname, string email, DateTime dateTime);

		Account FindMobileAccountByName(string name);

		List<Account> GetMostActiveUsers();

		void GetMostActiveUser(string filePath, List<Account> accountList);

		int CreateNumber();
	}
}