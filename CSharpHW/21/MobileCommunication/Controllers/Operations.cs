namespace MobileCommunication.Controllers
{
	using System;
	using System.Linq;

	using MobileCommunication.Models;

	public class Operations
	{
		private readonly SerializerDeserializer serializerDeserializer = new SerializerDeserializer();

		public void CreateOperator(bool createOperator)
		{
			if (!createOperator)
			{
				Console.WriteLine("Operator has been already created.");
				Console.WriteLine();

				return;
			}

			var myOperator = serializerDeserializer.DeserializeJsonTime<Operator>();

			#region Initialize operator

			myOperator.ListStandardAccounts = myOperator.CreateStandardAccounts();

			var vasyl = myOperator.CreateAccount();
			vasyl = myOperator.SetAccountParameters(vasyl,
			                                        "Vasyl",
			                                        "Vasylovych",
			                                        "vasyl.vasylovych@gmail.com",
			                                        new DateTime(1987, 10, 24));

			var petro = myOperator.CreateAccount();
			petro = myOperator.SetAccountParameters(petro,
			                                        "Petro",
			                                        "Petrovych",
			                                        "petro.petrovych@gmail.com",
			                                        new DateTime(1988, 01, 15, 10, 0, 0));

			var taras = myOperator.CreateAccount();
			taras = myOperator.SetAccountParameters(taras,
			                                        "Taras",
			                                        "Tarasovych",
			                                        "taras.tarasovych@gmail.com",
			                                        new DateTime(1991, 01, 28, 10, 0, 0));

			var nazar = myOperator.CreateAccount();
			nazar = myOperator.SetAccountParameters(nazar,
			                                        "Nazar",
			                                        "Nazarovych",
			                                        "nazar.nazarovych@gmail.com",
			                                        new DateTime(1997, 06, 10, 10, 0, 0));

			var igor = myOperator.CreateAccount();
			igor = myOperator.SetAccountParameters(igor,
			                                       "Igor",
			                                       "Igorovych",
			                                       "igor.igorovych@gmail.com",
			                                       new DateTime(1997, 01, 28, 10, 0, 0));

			var andriy = myOperator.CreateAccount();
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

			serializerDeserializer.SerializeJsonTime(myOperator);
		}

		public void FillOperator(bool fillOperator)
		{
			if (!fillOperator)
			{
				return;
			}

			var myOperator = serializerDeserializer.DeserializeJsonTime<Operator>();

			#region Initialize accounts

			var vasyl1 = myOperator.FindAccountByName("Vasyl");
			var petro1 = myOperator.FindAccountByName("Petro");
			var taras1 = myOperator.FindAccountByName("Taras");
			var nazar1 = myOperator.FindAccountByName("Nazar");
			var igor1 = myOperator.FindAccountByName("Igor");
			var andriy1 = myOperator.FindAccountByName("Andriy");

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

			myOperator.Logger.WriteMessagesToLog();

			serializerDeserializer.SerializeJsonTime(myOperator);
		}

		public void AddNewUser(bool addNewUser, Operator myOperator)
		{
			if (!addNewUser)
			{
				return;
			}

			// Get answer from user if he want to add a new account until answer yes/no
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
				var user = new User();

				

				Console.WriteLine();
				Console.WriteLine("Enter user properties:");
				Console.Write("Name: ");
				user.Name = Console.ReadLine();

				Console.Write("Surname: ");
				user.Surname = Console.ReadLine();

				Console.Write("Date of birth: ");
				DateTime.TryParse(Console.ReadLine(), out var birth);
				user.DateBirth = birth;

				Console.Write("E-mail: ");
				user.Email = Console.ReadLine();

				

				#region Create new account

				var newAccount = myOperator.CreateAccount();
				newAccount = myOperator.SetAccountParameters(newAccount,
																   user.Name,
																   user.Surname,
																   user.Email,
																   user.DateBirth);

				#endregion

				#region Make call/sms

				newAccount?.Call(myOperator.ListAccounts.Where(m => m.User.Name == "Nazar")
												 .Select(m => m.User.Number)
												 .FirstOrDefault());

				newAccount?.Sms(myOperator.ListAccounts.Where(m => m.User.Name == "Taras")
												.Select(m => m.User.Number)
												.FirstOrDefault());

				#endregion Make call/sms
			}

			Console.WriteLine();

			serializerDeserializer.SerializeJsonTime(myOperator);
		}
	}
}