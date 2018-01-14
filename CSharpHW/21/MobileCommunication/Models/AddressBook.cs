namespace MobileCommunication.Models
{
	using System.Collections.Generic;
	using System.Linq;

	using MobileCommunication.Controllers;

	using ProtoBuf;

	[ProtoContract]
	public class AddressBook
	{
		[ProtoMember(1)]
		public List<User> NumberList { get; set; }

		[ProtoMember(2)]
		public List<User> StandardNumberList { get; set; }

		public string GetAccountNameByNumber(int number)
		{
			var accountName =
				NumberList.SingleOrDefault(user => user.Number == number) != null
				|| NumberList.SingleOrDefault(user => user.Number == number) != null
					? NumberList.Where(user => user.Number == number)
								.Select(account => account.Name)
								.SingleOrDefault()
					: number.ToString();


			return accountName;
		}

		public void SetAccounts(params Account[] accounts)
		{
			NumberList = new List<User>();
			NumberList.AddRange(accounts.Select(a => a.User));
		}

		public void SetAccounts(List<Account> accounts)
		{
			NumberList = new List<User>();
			NumberList.AddRange(accounts.Select(a => a.User));
		}

		public void SetStandardAccounts(List<Account> standardAccounts)
		{
			StandardNumberList = new List<User>();
			StandardNumberList.AddRange(standardAccounts.Select(a => a.User));
		}
	}
}