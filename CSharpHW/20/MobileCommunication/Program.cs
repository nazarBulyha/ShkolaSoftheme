namespace MobileCommunication
{
	using System;
	using System.Linq;

	using MobileCommunication.Controllers;
	using MobileCommunication.Models;

	public class Program
	{
		private static void Main()
		{
			var myOperator = new Logger().Deserialize<Operator>();

			var createOperator = false;
			var fillOperator = false;
			var addNewUser = false;

			// Get user answer what to do
			int activity;

			do
			{
				Console.WriteLine("Choose what do you want to execute:");
				Console.WriteLine("1 - Create Operator.");
				Console.WriteLine("2 - Make actions(call/sms).");
				Console.WriteLine("3 - Add new user.");
				Console.WriteLine("4 - Create Operator + Make actions + Add new user.");

				int.TryParse(Console.ReadLine(), out activity);
			}
			while (activity != 1 && activity != 2 && activity != 3 && activity != 4);

			// ReSharper disable once SwitchStatementMissingSomeCases
			switch (activity)
			{
				case 1:
					{
						createOperator = myOperator.ListAccounts.Count == 0;

						break;
					}
				case 2:
					{
						createOperator = myOperator.ListAccounts.Count == 0;
						fillOperator = true;

						break;
					}
				case 3:
					{
						createOperator = myOperator.ListAccounts.Count == 0;
						addNewUser = true;

						break;
					}
				case 4:
					{
						createOperator = myOperator.ListAccounts.Count == 0;
						fillOperator = true;
						addNewUser = true;

						break;
					}
			}

			// initialize empty Operator
			if (createOperator)
			{
				myOperator = new Logger().Deserialize<Operator>();

				#region Initializing mobile accounts

				var vasyl = myOperator.CreateMobileAccount();
				vasyl = myOperator.SetAccountParameters(vasyl,
														"Vasyl",
														"Vasylovych",
														"vasyl.vasylovych@gmail.com",
														new DateTime(1987, 10, 24));

				var petro = myOperator.CreateMobileAccount();
				petro = myOperator.SetAccountParameters(petro,
														"Petro",
														"Petrovych",
														"petro.petrovych@gmail.com",
														new DateTime(1988, 01, 15, 10, 0, 0));

				var taras = myOperator.CreateMobileAccount();
				taras = myOperator.SetAccountParameters(taras,
														"Taras",
														"Tarasovych",
														"taras.tarasovych@gmail.com",
														new DateTime(1991, 01, 28, 10, 0, 0));

				var nazar = myOperator.CreateMobileAccount();
				nazar = myOperator.SetAccountParameters(nazar,
														"Nazar",
														"Nazarovych",
														"nazar.nazarovych@gmail.com",
														new DateTime(1997, 06, 10, 10, 0, 0));

				var igor = myOperator.CreateMobileAccount();
				igor = myOperator.SetAccountParameters(igor,
													   "Igor",
													   "Igorovych",
													   "igor.igorovych@gmail.com",
													   new DateTime(1997, 01, 28, 10, 0, 0));

				var andriy = myOperator.CreateMobileAccount();
				andriy = myOperator.SetAccountParameters(andriy,
														 "Andriy",
														 "Andriyovych",
														 "andriy.andriyovych@gmail.com",
														 new DateTime(1997, 10, 16, 10, 0, 0));

				#endregion

				#region Set AddressBook contacts

				vasyl.AddressBook.SetAccounts(petro, taras, nazar, igor, andriy);
				petro.AddressBook.SetAccounts(vasyl, taras, nazar, igor, andriy);
				taras.AddressBook.SetAccounts(vasyl, petro, nazar, igor, andriy);
				nazar.AddressBook.SetAccounts(vasyl, petro, taras, igor, andriy);
				andriy.AddressBook.SetAccounts(vasyl, petro, taras, nazar, igor);
				igor.AddressBook.SetAccounts(vasyl, petro, taras, nazar);

				#endregion

				myOperator.Logger.Serialize(myOperator);
			}

			if (fillOperator)
			{
				myOperator = new Logger().Deserialize<Operator>();

				#region Get/initialize accounts from file

				var vasyl1 = myOperator.FindMobileAccountByName("Vasyl");
				var petro1 = myOperator.FindMobileAccountByName("Petro");
				var taras1 = myOperator.FindMobileAccountByName("Taras");
				var nazar1 = myOperator.FindMobileAccountByName("Nazar");
				var igor1 = myOperator.FindMobileAccountByName("Igor");
				var andriy1 = myOperator.FindMobileAccountByName("Andriy");

				#endregion

				#region Call

				vasyl1.Call(petro1.User.Number);
				vasyl1.Call(andriy1.User.Number);
				vasyl1.Call(nazar1.User.Number);
				vasyl1.Call(petro1.User.Number);
				vasyl1.Call(petro1.User.Number);

				petro1.Call(vasyl1.User.Number);
				petro1.Call(vasyl1.User.Number);
				petro1.Call(andriy1.User.Number);
				petro1.Call(andriy1.User.Number);

				taras1.Call(nazar1.User.Number);
				taras1.Call(nazar1.User.Number);
				taras1.Call(nazar1.User.Number);
				taras1.Call(andriy1.User.Number);
				taras1.Call(andriy1.User.Number);
				taras1.Call(vasyl1.User.Number);
				taras1.Call(igor1.User.Number);
				taras1.Call(igor1.User.Number);
				taras1.Call(nazar1.User.Number);

				nazar1.Call(taras1.User.Number);
				nazar1.Call(taras1.User.Number);
				nazar1.Call(andriy1.User.Number);
				nazar1.Call(andriy1.User.Number);
				nazar1.Call(igor1.User.Number);
				nazar1.Call(igor1.User.Number);
				nazar1.Call(petro1.User.Number);
				nazar1.Call(vasyl1.User.Number);

				igor1.Call(andriy1.User.Number);
				igor1.Call(nazar1.User.Number);
				igor1.Call(taras1.User.Number);
				igor1.Call(petro1.User.Number);

				andriy1.Call(igor1.User.Number);
				andriy1.Call(nazar1.User.Number);
				andriy1.Call(taras1.User.Number);
				andriy1.Call(petro1.User.Number);

				#endregion Call

				#region SMS

				vasyl1.Sms(petro1.User.Number);
				vasyl1.Sms(andriy1.User.Number);
				vasyl1.Sms(nazar1.User.Number);
				vasyl1.Sms(petro1.User.Number);
				vasyl1.Sms(petro1.User.Number);

				petro1.Sms(vasyl1.User.Number);
				petro1.Sms(vasyl1.User.Number);
				petro1.Sms(andriy1.User.Number);
				petro1.Sms(andriy1.User.Number);

				taras1.Sms(nazar1.User.Number);
				taras1.Sms(nazar1.User.Number);
				taras1.Sms(nazar1.User.Number);
				taras1.Sms(andriy1.User.Number);
				taras1.Sms(andriy1.User.Number);
				taras1.Sms(vasyl1.User.Number);
				taras1.Sms(igor1.User.Number);
				taras1.Sms(igor1.User.Number);
				taras1.Sms(nazar1.User.Number);

				nazar1.Sms(taras1.User.Number);
				nazar1.Sms(taras1.User.Number);
				nazar1.Sms(andriy1.User.Number);
				nazar1.Sms(andriy1.User.Number);
				nazar1.Sms(igor1.User.Number);
				nazar1.Sms(igor1.User.Number);
				nazar1.Sms(petro1.User.Number);
				nazar1.Sms(vasyl1.User.Number);

				igor1.Sms(andriy1.User.Number);
				igor1.Sms(nazar1.User.Number);
				igor1.Sms(taras1.User.Number);
				igor1.Sms(petro1.User.Number);

				andriy1.Sms(igor1.User.Number);
				andriy1.Sms(nazar1.User.Number);
				andriy1.Sms(taras1.User.Number);
				andriy1.Sms(petro1.User.Number);

				#endregion SMS 

				myOperator.Logger.WriteLogMessages();

				myOperator.Logger.Serialize(myOperator);
			}

			// ReSharper disable once InvertIf
			if (addNewUser)
			{
				#region Add new user

				// Get answer from user if he want
				// to add a new account until answer yes/no
				string addNewUserAnswer;
				do
				{
					Console.WriteLine("Do you want to add new account, write yes/no");
					addNewUserAnswer = Console.ReadLine();
				}
				while (addNewUserAnswer != "yes" && addNewUserAnswer != "no");

				// check answers and make new account
				if (addNewUserAnswer.ToLower() == "yes")
				{
					var newAccount = new User();

					#region Fill new account properties

					Console.WriteLine();
					Console.WriteLine("Enter user properties:");
					Console.Write("Name: ");
					newAccount.Name = Console.ReadLine();

					Console.Write("Surname: ");
					newAccount.Surname = Console.ReadLine();

					Console.Write("Date of birth: ");
					DateTime.TryParse(Console.ReadLine(), out var birth);
					newAccount.DateBirth = birth;

					Console.Write("E-mail: ");
					newAccount.Email = Console.ReadLine();

					#endregion

					// test new account
					var newMobileAccount = myOperator.CreateMobileAccount();
					newMobileAccount = myOperator.SetAccountParameters(newMobileAccount,
																	   newAccount.Name,
																	   newAccount.Surname,
																	   newAccount.Email,
																	   newAccount.DateBirth);

					#region Make call/sms

					newMobileAccount?.Call(myOperator.ListAccounts.Where(m => m.User.Name == "Nazar")
													 .Select(m => m.User.Number)
													 .FirstOrDefault());

					newMobileAccount?.Sms(myOperator.ListAccounts.Where(m => m.User.Name == "Taras")
													.Select(m => m.User.Number)
													.FirstOrDefault());

					#endregion Make call/sms
				}

				#endregion Add new user

				//Logger.ShowLog(6, "Call ended", MessageType.Call);
				Console.WriteLine();

				myOperator.Logger.WriteLogMessages();

				myOperator.Logger.Serialize(myOperator);
			}

			UserActivity.GetMostActiveUser(myOperator.Logger.FolderPath + myOperator.Logger.CallLoggerFileName, myOperator.ListAccounts);

			Console.ReadLine();
		}
	}
}