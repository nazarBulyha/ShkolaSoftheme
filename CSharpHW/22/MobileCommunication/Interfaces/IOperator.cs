using System;
using System.Collections.Generic;

namespace MobileCommunication.Interfaces
{
	using MobileCommunication.Controllers;

	public interface IOperator
	{
		List<Account> ListAccounts { get; set; }

		List<Account> ListStandardAccounts { get; set; }

		Account CreateAccount();

		Account SetAccountParameters(Account account, string name, string surname, string email, DateTime dateTime);

		Account FindAccountByName(string name);

		int CreateNumber();
	}
}