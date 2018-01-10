namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	[Serializable]
	public class Operator : IOperator
	{
		public List<Account> ListAccounts { get; set; }

		public List<Account> ListStandardAccounts { get; set; }

		[NonSerialized]
		public const int OperatorNumber = 2219320;

		[NonSerialized]
		public readonly Logger Logger;

		private Account accountSender;

		private Account accountReceiver;

		private int number = 2219320;

		public Operator()
		{
			Logger = new Logger();
			ListAccounts = new List<Account>();
			ListStandardAccounts = CreateStandardMobileAccounts();
		}

		public Account CreateMobileAccount()
		{
			var mobileAccount = new Account();

			ListAccounts.Add(mobileAccount);

			mobileAccount.OnCallHandler += CallConnection;
			mobileAccount.OnCallHandler += EndCall;
			mobileAccount.OnSmsHandler += SmsConnection;
			mobileAccount.OnSmsHandler += ReceiveSms;

			return mobileAccount;
		}

		public Account SetAccountParameters(Account mobileAccount, string name, string surname, string email, DateTime dateTime)
		{
			var account = new User(CreateNumber())
			{
				Name = name,
				Surname = surname,
				Email = email,
				DateBirth = dateTime
			};

			mobileAccount.AddressBook.SetStandardAccounts(ListStandardAccounts);

			mobileAccount.User = account;

			return mobileAccount;
		}

		public int CreateNumber()
		{
			// TODO maybe add some logic
			return number++;
		}

		public Account FindMobileAccountByName(string name)
		{
			var mobileAccount = ListAccounts.SingleOrDefault(mobileAcc => mobileAcc.User.Name == name);

			if (mobileAccount == null)
			{
				return null;
			}

			mobileAccount.OnCallHandler += CallConnection;
			mobileAccount.OnCallHandler += EndCall;
			mobileAccount.OnSmsHandler += SmsConnection;
			mobileAccount.OnSmsHandler += ReceiveSms;

			return mobileAccount;
		}

		// TODO: check this shit
		public void GetMostActiveUser(string filePath, List<Account> accountList)
		{
			if (!File.Exists(filePath))
			{
				File.Create(filePath);
			}

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
							 .Value == user.User.Number.ToString())
					{
						senderCount++;
					}
					else if (message.Contains("Call ended") &&
						Regex.Match(messageBlock.Find(s => s.Contains("Receiver")), @"\d+")
							 .Value == user.User.Number.ToString())
					{
						receiverCount++;
					}
					else if (message.Contains("Try to send sms") &&
						Regex.Match(messageBlock.Find(s => s.Contains("Sender")), @"\d+")
							 .Value == user.User.Number.ToString())
					{
						senderCount += 0.5;
					}
					else if (message.Contains("Sms received") &&
						Regex.Match(messageBlock.Find(s => s.Contains("Receiver")), @"\d+")
							 .Value == user.User.Number.ToString())
					{
						receiverCount += 0.5;
					}
				}));

				if (senderMaxCount < senderCount)
				{
					senderMaxCount = senderCount;
					maxSenderAccountName = user.User.Name;
				}

				// ReSharper disable once InvertIf
				if (receiverMaxCount < receiverCount)
				{
					receiverMaxCount = receiverCount;
					maxReceiverAccountName = user.User.Name;
				}
			}

			Console.WriteLine($"Most SENDER POINTS has User: {maxSenderAccountName}, points: {senderMaxCount}"
			 + $"\nMost RECEIVER POINTS has User: {maxReceiverAccountName}, points: {receiverMaxCount}");
		}

		// TODO: realize this shit
		public List<Account> GetMostActiveUsers()
		{

			return null;
		}

		private List<Account> CreateStandardMobileAccounts()
		{
			var standardMobileAccounts = new List<Account>
											 {
												 new Account
													 {
														 User = new User(CreateNumber())
																	{
																		Name = "standardName1",
																		Surname = "standardSurname1",
																		DateBirth = DateTime.Now
																	}
													 },
												 new Account
													 {
														 User = new User(CreateNumber())
																	{
																		Name = "standardName2",
																		Surname = "standardSurname2",
																		DateBirth = DateTime.Now
																	}
													 },
												 new Account
													 {
														 User = new User(CreateNumber())
																	{
																		Name = "standardName3",
																		Surname = "standardSurname3",
																		DateBirth = DateTime.Now
																	}
													 },
												 new Account
													 {
														 User = new User(CreateNumber())
																	{
																		Name = "standardName4",
																		Surname = "standardSurname4",
																		DateBirth = DateTime.Now
																	}
													 }
											 };

			return standardMobileAccounts;
		}

		private void CallConnection(object sender, AccountEventArgs e)
		{
			LogCallEvent("Try to call.", e, MessageType.Call);

			try
			{
				accountSender = sender as Account;
				accountReceiver = ListAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.User.Number));

				if (string.IsNullOrEmpty(accountSender?.ToString()) || string.IsNullOrEmpty(accountReceiver?.ToString()))
				{
					throw new NullReferenceException();
				}

				if (accountSender.User.Number < OperatorNumber || accountReceiver.User.Number < OperatorNumber)
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

			accountReceiver.ReceiveCall(accountSender.User.Number);
		}

		private void SmsConnection(object sender, AccountEventArgs e)
		{
			LogSmsEvent("Try to send sms.", e, MessageType.Message);

			try
			{
				accountSender = sender as Account;
				accountReceiver = ListAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.User.Number));

				if (string.IsNullOrEmpty(accountSender?.ToString()) || string.IsNullOrEmpty(accountReceiver?.ToString()))
				{
					throw new NullReferenceException();
				}

				if (accountSender.User.Number < OperatorNumber || accountReceiver.User.Number < OperatorNumber)
				{
					throw new ArgumentException();
				}
			}
			catch (NullReferenceException)
			{
				e.IsHandled = true;

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}
			catch (ArgumentException)
			{
				e.IsHandled = true;

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}
			catch (Exception)
			{
				e.IsHandled = true;

				LogSmsEvent("Sms wasn't send.", e);

				return;
			}

			LogSmsEvent("Sending.", e, MessageType.Message);

			accountReceiver.ReceiveSms(accountSender.User.Number);
		}

		private void EndCall(object sender, AccountEventArgs e)
		{
			if (e.IsHandled)
			{
				return;
			}

			LogSmsEvent("Call ended.", e, MessageType.Call);
		}

		private void ReceiveSms(object sender, AccountEventArgs e)
		{
			if (e.IsHandled)
			{
				return;
			}

			// if sender number isn't in blocked numbers than receive sms
			LogSmsEvent("Sms received.", e, MessageType.Message);
		}

		private void LogCallEvent(string message, AccountEventArgs accountArgs, MessageType messageType = MessageType.Error)
		{
			Logger.AddLogMessage(message, accountArgs, messageType);
		}

		private void LogSmsEvent(string message, AccountEventArgs accountArgs, MessageType messageType = MessageType.Error)
		{
			Logger.AddLogMessage(message, accountArgs, messageType);
		}

		private static void Validate(User user)
		{
			var results = new List<ValidationResult>();
			var context = new ValidationContext(user);

			if (!Validator.TryValidateObject(user, context, results, true))
			{
				foreach (var error in results)
				{
					Console.WriteLine(error.ErrorMessage);
				}
			}
			else
			{
				Console.WriteLine($"User '{user.Name}' is Valid");
			}
		}
	}
}