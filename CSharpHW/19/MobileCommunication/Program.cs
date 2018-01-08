
namespace MobileCommunication
{
	using System;

	using MobileCommunication.Controllers;
	using MobileCommunication.Extensions;
	using MobileCommunication.Models;

	public class Program
	{
		private static void Main()
		{
			var myOperator = (MobileOperator)Logger.Deserialize<MobileOperator>();

			// initialize empty Operator
			if (myOperator.MobileAccounts.Count == 0)
			{
				#region Initializing mobile accounts

				var vasyl = myOperator.CreateMobileAccount();
				vasyl = myOperator.SetAccountParametres(vasyl,
														"Vasyl",
														"Vasylovych",
														"vasyl.vasylovych@gmail.com",
														new DateTime(1987, 10, 24));

				var petro = myOperator.CreateMobileAccount();
				petro = myOperator.SetAccountParametres(petro,
														"Petro",
														"Petrovych",
														"petro.petrovych@gmail.com",
														new DateTime(1988, 01, 15));

				var taras = myOperator.CreateMobileAccount();
				taras = myOperator.SetAccountParametres(taras,
														"Taras",
														"Tarasovych",
														"taras.tarasovych@gmail.com",
														new DateTime(1991, 01, 28));

				var nazar = myOperator.CreateMobileAccount();
				nazar = myOperator.SetAccountParametres(nazar,
														"Nazar",
														"Nazarovych",
														"nazar.nazarovych@gmail.com",
														new DateTime(1997, 06, 10));

				var igor = myOperator.CreateMobileAccount();
				igor = myOperator.SetAccountParametres(igor,
													   "Igor",
													   "Igorovych",
													   "igor.igorovych@gmail.com",
													   new DateTime(1997, 01, 28));

				var andriy = myOperator.CreateMobileAccount();
				andriy = myOperator.SetAccountParametres(andriy,
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
			}

			#region Account actions via Operator

			#region Get/initialize accounts from file

			var vasyl1 = myOperator.FindMobileAccountByName("Vasyl");
			var petro1 = myOperator.FindMobileAccountByName("Petro");
			var taras1 = myOperator.FindMobileAccountByName("Taras");
			var nazar1 = myOperator.FindMobileAccountByName("Nazar");
			var igor1 = myOperator.FindMobileAccountByName("Igor");
			var andriy1 = myOperator.FindMobileAccountByName("Andriy");

			#endregion

			#region Call
			vasyl1.MakeCall(petro1.Account.Number);
			vasyl1.MakeCall(andriy1.Account.Number);
			vasyl1.MakeCall(nazar1.Account.Number);
			vasyl1.MakeCall(petro1.Account.Number);
			vasyl1.MakeCall(petro1.Account.Number);

			petro1.MakeCall(vasyl1.Account.Number);
			petro1.MakeCall(vasyl1.Account.Number);
			petro1.MakeCall(andriy1.Account.Number);
			petro1.MakeCall(andriy1.Account.Number);

			taras1.MakeCall(nazar1.Account.Number);
			taras1.MakeCall(nazar1.Account.Number);
			taras1.MakeCall(nazar1.Account.Number);
			taras1.MakeCall(andriy1.Account.Number);
			taras1.MakeCall(andriy1.Account.Number);
			taras1.MakeCall(vasyl1.Account.Number);
			taras1.MakeCall(igor1.Account.Number);
			taras1.MakeCall(igor1.Account.Number);
			taras1.MakeCall(nazar1.Account.Number);

			nazar1.MakeCall(taras1.Account.Number);
			nazar1.MakeCall(taras1.Account.Number);
			nazar1.MakeCall(andriy1.Account.Number);
			nazar1.MakeCall(andriy1.Account.Number);
			nazar1.MakeCall(igor1.Account.Number);
			nazar1.MakeCall(igor1.Account.Number);
			nazar1.MakeCall(petro1.Account.Number);
			nazar1.MakeCall(vasyl1.Account.Number);

			igor1.MakeCall(andriy1.Account.Number);
			igor1.MakeCall(nazar1.Account.Number);
			igor1.MakeCall(taras1.Account.Number);
			igor1.MakeCall(petro1.Account.Number);

			andriy1.MakeCall(igor1.Account.Number);
			andriy1.MakeCall(nazar1.Account.Number);
			andriy1.MakeCall(taras1.Account.Number);
			andriy1.MakeCall(petro1.Account.Number);

			#endregion Call

			#region SMS
			vasyl1.SendSms(petro1.Account.Number);
			vasyl1.SendSms(andriy1.Account.Number);
			vasyl1.SendSms(nazar1.Account.Number);
			vasyl1.SendSms(petro1.Account.Number);
			vasyl1.SendSms(petro1.Account.Number);

			petro1.SendSms(vasyl1.Account.Number);
			petro1.SendSms(vasyl1.Account.Number);
			petro1.SendSms(andriy1.Account.Number);
			petro1.SendSms(andriy1.Account.Number);

			taras1.SendSms(nazar1.Account.Number);
			taras1.SendSms(nazar1.Account.Number);
			taras1.SendSms(nazar1.Account.Number);
			taras1.SendSms(andriy1.Account.Number);
			taras1.SendSms(andriy1.Account.Number);
			taras1.SendSms(vasyl1.Account.Number);
			taras1.SendSms(igor1.Account.Number);
			taras1.SendSms(igor1.Account.Number);
			taras1.SendSms(nazar1.Account.Number);

			nazar1.SendSms(taras1.Account.Number);
			nazar1.SendSms(taras1.Account.Number);
			nazar1.SendSms(andriy1.Account.Number);
			nazar1.SendSms(andriy1.Account.Number);
			nazar1.SendSms(igor1.Account.Number);
			nazar1.SendSms(igor1.Account.Number);
			nazar1.SendSms(petro1.Account.Number);
			nazar1.SendSms(vasyl1.Account.Number);

			igor1.SendSms(andriy1.Account.Number);
			igor1.SendSms(nazar1.Account.Number);
			igor1.SendSms(taras1.Account.Number);
			igor1.SendSms(petro1.Account.Number);

			andriy1.SendSms(igor1.Account.Number);
			andriy1.SendSms(nazar1.Account.Number);
			andriy1.SendSms(taras1.Account.Number);
			andriy1.SendSms(petro1.Account.Number);

			#endregion SMS 

			#endregion Account actions via Operator

			Logger.ShowAllLog();

			//TODO: most active users
			myOperator.GetMostActiveUser(Logger.FolderPath + Logger.CallLoggerName, myOperator.MobileAccounts);

			//Console.ReadKey();

			Logger.Serialize(myOperator);
		}
	}
}