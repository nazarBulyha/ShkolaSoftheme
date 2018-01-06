using System;

namespace MobileCommunication
{
	using MobileCommunication.Controllers;
	using MobileCommunication.Extensions;

	public class Program
	{
		private static void Main()
		{
			var myOperator = (MobileOperator)SerializerDesserializer.Deserialize<MobileOperator>();

			// initialize empty Operator
			if (myOperator.MobileAccounts.Count == 0)
			{
				#region Initializing mobile accounts

				var vasyl = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(vasyl,
				                                "Vasyl",
				                                "Vasylovych",
				                                "vasyl.vasylovych@gmail.com",
				                                new DateTime(1987, 10, 24));

				var petro = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(petro,
				                                "Petro",
				                                "Petrovych",
				                                "petro.petrovych@gmail.com",
				                                new DateTime(1988, 01, 15));

				var taras = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(taras,
				                                "Taras",
				                                "Tarasovych",
				                                "taras.tarasovych@gmail.com",
				                                new DateTime(1991, 01, 28));

				var nazar = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(nazar,
				                                "Nazar",
				                                "Nazarovych",
				                                "nazar.nazarovych@gmail.com",
				                                new DateTime(1997, 06, 10));

				var igor = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(igor, "Igor", "Igorovych", "igor.igorovych@gmail.com", new DateTime(1997, 01, 28));

				var andriy = myOperator.CreateMobileAccount();
				myOperator.SetAccountParametres(andriy,
				                                "Andriy",
				                                "Andriyovych",
				                                "andriy.andriyovych@gmail.com",
				                                new DateTime(1997, 10, 16));

				#endregion

				#region Set AddressBook contacts

				vasyl.AddressBook.SetAccounts(petro.Account, taras.Account, nazar.Account, igor.Account, andriy.Account);
				petro.AddressBook.SetAccounts(vasyl.Account, taras.Account, nazar.Account, igor.Account, andriy.Account);
				taras.AddressBook.SetAccounts(vasyl.Account, petro.Account, nazar.Account, igor.Account, andriy.Account);
				nazar.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, igor.Account, andriy.Account);
				andriy.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, nazar.Account, igor.Account);
				igor.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, nazar.Account);

				#endregion

				#region Account actions via Operator

				vasyl.MakeCall(petro.Account.Number);
				taras.MakeCall(nazar.Account.Number);
				igor.MakeCall(andriy.Account.Number);

				vasyl.SendSms(petro.Account.Number);
				taras.SendSms(nazar.Account.Number);
				igor.SendSms(andriy.Account.Number);

				#endregion
			}

			myOperator.Logger.ShowAllLog();
			Console.ReadKey();

			SerializerDesserializer.Serialize(myOperator);
		}
	}
}