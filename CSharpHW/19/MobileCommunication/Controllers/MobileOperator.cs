namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Xml.Serialization;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	[Serializable]
	public class MobileOperator : IMobileOperator
	{
		public List<MobileAccount> MobileAccounts { get; set; }

		[XmlIgnore]
		public List<Account> StandardMobileAccounts { get; set; }

		private MobileAccount mobileAccountSender;

		private MobileAccount mobileAccountReceiver;

		private int number = 2219320;

		public MobileOperator()
		{
			StandardMobileAccounts = CreateStandardMobileAccounts();
			MobileAccounts = new List<MobileAccount>();
		}

		private List<Account> CreateStandardMobileAccounts()
		{
			#region Define standard accounts for address book

			var standartAccount1 = new Account(CreateNumber())
			{
				Name = "standartName1",
				Surname = "standartSurname1",
				DateBirth = DateTime.Now
			};
			var standartAccount2 = new Account(CreateNumber())
			{
				Name = "standartName2",
				Surname = "standartSurname2",
				DateBirth = DateTime.Now
			};
			var standartAccount3 = new Account(CreateNumber())
			{
				Name = "standartName3",
				Surname = "standartSurname3",
				DateBirth = DateTime.Now
			};
			var standartAccount4 = new Account(CreateNumber())
			{
				Name = "standartName4",
				Surname = "standartSurname4s",
				DateBirth = DateTime.Now
			};

			StandardMobileAccounts = new List<Account>
			{
				standartAccount1, standartAccount2, standartAccount3, standartAccount4
			};
			#endregion

			return StandardMobileAccounts;
		}

		public MobileAccount CreateMobileAccount()
		{
			var mobileAccount = new MobileAccount();

			MobileAccounts.Add(mobileAccount);

			mobileAccount.OnCallHandler += TryMakeCall;
			mobileAccount.OnCallHandler += EndCall;
			mobileAccount.OnSmsHandler += TrySendSms;
			mobileAccount.OnSmsHandler += ReceiveSms;

			return mobileAccount;
		}

		public MobileAccount FindMobileAccountByName(string name)
		{
			var mobileAccount = MobileAccounts.SingleOrDefault(mobileAcc => mobileAcc.Account.Name == name);

			if (mobileAccount == null)
			{
				return null;
			}

			mobileAccount.OnCallHandler += TryMakeCall;
			mobileAccount.OnCallHandler += EndCall;
			mobileAccount.OnSmsHandler += TrySendSms;
			mobileAccount.OnSmsHandler += ReceiveSms;

			return mobileAccount;
		}

		public void GetMostActiveUser(string filePath, List<MobileAccount> accountList)
		{
			var messageBlocksList = new List<List<string>>();
			var fileContent = new List<string>();

			double senderMaxCount = 0, receiverMaxCount = 0;
			string maxSenderAccountName = "Nobody", maxReceiverAccountName = "Nobody";

			using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = new StreamReader(fileStream, Encoding.UTF8))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();

					if (!string.IsNullOrEmpty(line))
					{
						fileContent.Add(line);
					}
					else
					{
						messageBlocksList.Add(fileContent);
						fileContent = new List<string>();
					}
				}
			}

			foreach (var user in accountList)
			{
				double senderCount = 0, receiverCount = 0;

				messageBlocksList.ForEach(messageBlock =>
				messageBlock.ForEach(message =>
				{
					if (!message.Contains("Try to call") && !message.Contains("Try to send sms") && !message.Contains("Call ended")
					    && !message.Contains("Sms received"))
					{
						return;
					}

					if (message.Contains("Try to call") &&
					    Regex.Match(messageBlock.Find(s => s.Contains("Sender")), @"\d+")
					         .Value == user.Account.Number.ToString())
					{
						senderCount++;
					}

					if (message.Contains("Call ended") &&
					    Regex.Match(messageBlock.Find(s => s.Contains("Receiver")), @"\d+")
					         .Value == user.Account.Number.ToString())
					{
						receiverCount++;
					}

					if (message.Contains("Try to send sms") &&
					    Regex.Match(messageBlock.Find(s => s.Contains("Sender")), @"\d+")
					         .Value == user.Account.Number.ToString())
					{
						senderCount += 0.5;
					}

					if (message.Contains("Sms received") &&
					    Regex.Match(messageBlock.Find(s => s.Contains("Receiver")), @"\d+")
					         .Value == user.Account.Number.ToString())
					{
						receiverCount += 0.5;
					}
				}));

				if (senderMaxCount < senderCount)
				{
					senderMaxCount = senderCount;
					maxSenderAccountName = user.Account.Name;
				}

				// ReSharper disable once InvertIf
				if (receiverMaxCount < receiverCount)
				{
					receiverMaxCount = receiverCount;
					maxReceiverAccountName = user.Account.Name;
				}
			}

			Console.WriteLine($"Most SENDER POINTS has User: {maxSenderAccountName}, points: {senderMaxCount}"
			 + $"\n Most RECEIVER POINTS has User: {maxReceiverAccountName}, points: {receiverMaxCount}");

			Console.ReadKey();
		}

		public List<MobileAccount> GetMostActiveUsers()
		{

			return null;
		}

		public MobileAccount SetAccountParametres(MobileAccount mobileAccount, string name, string surname, string email, DateTime dateTime)
		{
			var account = new Account(CreateNumber())
			{
				Name = name,
				Surname = surname,
				Email = email,
				DateBirth = dateTime
			};

			mobileAccount.AddressBook.SetAccounts(StandardMobileAccounts);
			mobileAccount.Account = account;

			return mobileAccount;
		}

		public int CreateNumber()
		{
			return number++;
		}

		private void TryMakeCall(object sender, AccountEventArgs e)
		{
			LogCallEvent("Try to call.", e, MessageType.Call);

			try
			{
				mobileAccountSender = sender as MobileAccount;
				mobileAccountReceiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

				if (string.IsNullOrEmpty(mobileAccountSender?.ToString()) || string.IsNullOrEmpty(mobileAccountReceiver?.ToString()))
				{
					throw new NullReferenceException();
				}

				if (mobileAccountSender.Account.Number == 2219324 || mobileAccountSender.Account.Number == 2219325 || mobileAccountReceiver.Account.Number == 2219324 || mobileAccountReceiver.Account.Number == 2219325)
				{
					throw new ArgumentException();
				}
			}
			catch (NullReferenceException)
			{
				e.IsHandled = true;
				LogCallEvent("Call crashed.", e);

				return;
			}
			catch (ArgumentException)
			{
				e.IsHandled = true;
				LogCallEvent("Call crashed.", e);

				return;
			}
			catch (Exception)
			{
				e.IsHandled = true;
				LogCallEvent("Call crashed.", e);

				return;
			}

			LogCallEvent("Speaking.", e, MessageType.Call);

			mobileAccountReceiver.ReceiveCall(mobileAccountSender.Account.Number);
		}

		private void TrySendSms(object sender, AccountEventArgs e)
		{
			LogSmsEvent("Try to send sms.", e, MessageType.Message);

			try
			{
				mobileAccountSender = sender as MobileAccount;
				mobileAccountReceiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

				if (string.IsNullOrEmpty(mobileAccountSender?.ToString()) || string.IsNullOrEmpty(mobileAccountReceiver?.ToString()))
				{
					throw new NullReferenceException();
				}

				if (mobileAccountSender.Account.Number == 2219324 || mobileAccountSender.Account.Number == 2219325 || mobileAccountReceiver.Account.Number == 2219324 || mobileAccountReceiver.Account.Number == 2219325)
				{
					throw new ArgumentException();
				}
			}
			catch (NullReferenceException)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}
			catch (ArgumentException)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}
			catch (Exception)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}

			LogSmsEvent("Sending.", e, MessageType.Message);

			mobileAccountReceiver.ReceiveSms(mobileAccountSender.Account.Number);
		}

		// TODO: Implement logic after receiving Call
		private void EndCall(object sender, AccountEventArgs e)
		{
			if (e.IsHandled)
			{
				return;
			}

			// end call for both users
			// if number doesn't exists, end call for one user
			LogSmsEvent("Call ended.", e, MessageType.Call);
		}

		private void ReceiveSms(object sender, AccountEventArgs e)
		{
			// if sender number isn't in blocked numbers than receive sms
			LogSmsEvent("Sms received.", e, MessageType.Message);
		}

		private static void LogCallEvent(string message, AccountEventArgs accountArgs, MessageType messageType = MessageType.Error)
		{
			Logger.Log(message, accountArgs, messageType);
		}

		private static void LogSmsEvent(string message, AccountEventArgs accountArgs, MessageType messageType = MessageType.Error)
		{
			Logger.Log(message, accountArgs, messageType);
		}
	}
}