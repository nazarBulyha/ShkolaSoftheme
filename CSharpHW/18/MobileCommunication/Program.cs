using MobileCommunication.Interfaces;
using System;

namespace MobileCommunication
{
	using System.Configuration;
	using System.IO;
	using System.Xml.Serialization;

	using MobileCommunication.Controllers;

	public class Program
	{
		public static IMobileOperator MyOperator = new MobileOperator();

		public static readonly string FileName = "XmlSerialization.xml";

		public static readonly string Path = ConfigurationManager.AppSettings["LocalStoragePath"];

		private static void Main()
		{
			Deserialization();

			AppDomain.CurrentDomain.ProcessExit += Serialization;

			//#region Initializing mobile accounts
			//var tempAccount = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(tempAccount, "tempAccount", "tempAccount", "", new DateTime(1988, 01, 15));

			//var adminAccount = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(adminAccount, "adminAccount", "adminAccount", "", new DateTime(1977, 01, 15));

			//var vasyl = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(vasyl,
			//								 "Vasyl",
			//								 "Vasylovych",
			//								 "vasyl.vasylovych@gmail.com",
			//								 new DateTime(1987, 10, 24));

			//var petro = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(petro,
			//								 "Petro",
			//								 "Petrovych",
			//								 "petro.petrovych@gmail.com",
			//								 new DateTime(1988, 01, 15));

			//var taras = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(taras,
			//								 "Taras",
			//								 "Tarasovych",
			//								 "taras.tarasovych@gmail.com",
			//								 new DateTime(1991, 01, 28));

			//var nazar = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(nazar,
			//								 "Nazar",
			//								 "Nazarovych",
			//								 "nazar.nazarovych@gmail.com",
			//								 new DateTime(1997, 06, 10));

			//var igor = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(igor,
			//								 "Igor",
			//								 "Igorovych",
			//								 "igor.igorovych@gmail.com",
			//								 new DateTime(1997, 01, 28));

			//var andriy = MyOperator.CreateMobileAccount();
			//MyOperator.SetAccountParametres(andriy,
			//								 "Andriy",
			//								 "Andriyovych",
			//								 "andriy.andriyovych@gmail.com",
			//								 new DateTime(1997, 10, 16));

			//#endregion

			//#region Set AddressBook contacts

			//vasyl.AddressBook.SetAccounts(petro.Account, taras.Account, nazar.Account, igor.Account, andriy.Account);
			//petro.AddressBook.SetAccounts(vasyl.Account, taras.Account, nazar.Account, igor.Account, andriy.Account);
			//taras.AddressBook.SetAccounts(vasyl.Account, petro.Account, nazar.Account, igor.Account, andriy.Account);
			//nazar.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, igor.Account, andriy.Account);
			//andriy.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, nazar.Account, igor.Account);
			//igor.AddressBook.SetAccounts(vasyl.Account, petro.Account, taras.Account, nazar.Account);

			//#endregion

			//#region Account actions via Operator

			//vasyl.MakeCall(adminAccount.Account.Number);
			//vasyl.MakeCall(tempAccount.Account.Number);
			//vasyl.MakeCall(petro.Account.Number);
			//taras.MakeCall(nazar.Account.Number);
			//igor.MakeCall(andriy.Account.Number);

			//vasyl.SendSms(tempAccount.Account.Number);
			//tempAccount.SendSms(vasyl.Account.Number);

			//vasyl.SendSms(petro.Account.Number);
			//taras.SendSms(nazar.Account.Number);
			//igor.SendSms(andriy.Account.Number);

			//#endregion

			var vasyl1 = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(vasyl1,
			                                "Vasyl",
			                                "Vasylovych",
			                                "vasyl.vasylovych@gmail.com",
			                                new DateTime(1987, 10, 24));

			var petro1 = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(petro1,
											 "Petro",
											 "Petrovych",
											 "petro.petrovych@gmail.com",
											 new DateTime(1988, 01, 15));
			vasyl1.AddressBook.SetAccounts(petro1.Account);


			//var callLog = new Logger();
			//callLog.ShowAllLog();

			//Console.ReadKey();
		}

		private static void Serialization(object sender, EventArgs args)
		{
			using (var fileStream = new FileStream(Path + FileName, FileMode.OpenOrCreate))
			{
				var serializer = new XmlSerializer(typeof(MobileOperator));

				serializer.Serialize(fileStream, MyOperator);
			}
		}

		private static void Deserialization()
		{
			if (!File.Exists(Path + FileName)) return;

			using (var fileStream = new FileStream(Path + FileName, FileMode.Open))
			{
				var serializer = new XmlSerializer(typeof(MobileOperator));

				MyOperator = (IMobileOperator)serializer.Deserialize(fileStream);
			}
		}
	}
}