using MobileCommunication.Interfaces;
using System;

namespace MobileCommunication
{
	using System.IO;
	using System.Runtime.Serialization;
	using System.Xml;

	using MobileCommunication.Controllers;

	public class Program
	{
		public static IMobileOperator MyOperator = new MobileOperator();

		public static readonly string Path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\XmlSerialization.xml";

		private static void Main()
		{
			CurrentDomainProcessStart();

			AppDomain.CurrentDomain.ProcessExit += CurrentDomainProcessExit;

			#region Initializing mobile accounts
			var tempAccount = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(tempAccount, "tempAccount", "tempAccount", "", new DateTime(1988, 01, 15));

			var adminAccount = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(adminAccount, "adminAccount", "adminAccount", "", new DateTime(1977, 01, 15));

			var vasyl = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(vasyl,
											 "Vasyl",
											 "Vasylovych",
											 "vasyl.vasylovych@gmail.com",
											 new DateTime(1987, 10, 24));

			var petro = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(petro,
											 "Petro",
											 "Petrovych",
											 "petro.petrovych@gmail.com",
											 new DateTime(1988, 01, 15));

			var taras = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(taras,
											 "Taras",
											 "Tarasovych",
											 "taras.tarasovych@gmail.com",
											 new DateTime(1991, 01, 28));

			var nazar = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(nazar,
											 "Nazar",
											 "Nazarovych",
											 "nazar.nazarovych@gmail.com",
											 new DateTime(1997, 06, 10));

			var igor = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(igor,
											 "Igor",
											 "Igorovych",
											 "igor.igorovych@gmail.com",
											 new DateTime(1997, 01, 28));

			var andriy = MyOperator.CreateMobileAccount();
			MyOperator.SetAccountParametres(andriy,
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

			vasyl.MakeCall(adminAccount.Account.Number);
			vasyl.MakeCall(tempAccount.Account.Number);
			vasyl.MakeCall(petro.Account.Number);
			taras.MakeCall(nazar.Account.Number);
			igor.MakeCall(andriy.Account.Number);

			vasyl.SendSms(tempAccount.Account.Number);
			tempAccount.SendSms(vasyl.Account.Number);

			vasyl.SendSms(petro.Account.Number);
			taras.SendSms(nazar.Account.Number);
			igor.SendSms(andriy.Account.Number);

			#endregion


			var callLog = new Logger();
			callLog.ShowAllLog();

			//Console.ReadKey();
		}

		private static void CurrentDomainProcessExit(object sender, EventArgs args)
		{
			// TODO: do deserialization
			using (var fileStream = new FileStream(Path, FileMode.Create))
			{
				var serializer = new DataContractSerializer(typeof(IMobileOperator));

				serializer.WriteObject(fileStream, MyOperator);
			}
		}

		private static void CurrentDomainProcessStart()
		{
			if (!File.Exists(Path)) return;
			// TODO: do serialization
			using (var fileStream = new FileStream(Path, FileMode.Open))
			{
				var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());

				var serializer = new DataContractSerializer(typeof(IMobileOperator));

				// Exception
				MyOperator = (IMobileOperator)serializer.ReadObject(reader, true);

			}
		}
	}
}