namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	using ProtoBuf;

	[ProtoContract]
	public class Operator : IOperator
	{
		[ProtoMember(1)]
		public List<Account> ListAccounts { get; set; }

		[ProtoMember(2)]
		public List<Account> ListStandardAccounts { get; set; }

		[ProtoMember(3)]
		public Logger Logger { get; set; }

		public Operator()
		{
			ListAccounts = new List<Account>();
			ListStandardAccounts = new List<Account>();
			Logger = new Logger();
		}

		[ProtoMember(4)]
		public const int OperatorNumber = 2219320;

		private Account accountSender;

		private Account accountReceiver;

		private int number = 2219320;

		public Account CreateAccount()
		{
			var account = new Account();

			ListAccounts.Add(account);

			account.OnCallHandler += CallConnection;
			account.OnCallHandler += EndCall;
			account.OnSmsHandler += SmsConnection;
			account.OnSmsHandler += ReceiveSms;

			return account;
		}

		public Account SetAccountParameters(Account account, string name, string surname, string email, DateTime dateTime)
		{
			var newAccount = new User(CreateNumber())
			{
				Name = name,
				Surname = surname,
				Email = email,
				DateBirth = dateTime
			};

			account.AddressBook.SetStandardAccounts(ListStandardAccounts);

			account.User = newAccount;

			return Validate(account.User) ? account : null;
		}

		public List<Account> CreateStandardAccounts()
		{
			var standardAccounts = new List<Account>
									   {
										   new Account
											   {
												   User = new User(CreateNumber())
															  {
																  Name = "standardName1",
																  Surname = "standardSurname1",
																  DateBirth = DateTime.Now,
																  Email = "standard1@gmail.com"
															  }
											   },
										   new Account
											   {
												   User = new User(CreateNumber())
															  {
																  Name = "standardName2",
																  Surname = "standardSurname2",
																  DateBirth = DateTime.Now,
																  Email = "standard2@gmail.com"
															  }
											   },
										   new Account
											   {
												   User = new User(CreateNumber())
															  {
																  Name = "standardName3",
																  Surname = "standardSurname3",
																  DateBirth = DateTime.Now,
																  Email = "standard3@gmail.com"
															  }
											   },
										   new Account
											   {
												   User = new User(CreateNumber())
															  {
																  Name = "standardName4",
																  Surname = "standardSurname4",
																  DateBirth = DateTime.Now,
																  Email = "standard4@gmail.com"
															  }
											   }
									   };

			return Validate(standardAccounts.Select(u => u.User)) ? standardAccounts : null;
		}

		public int CreateNumber()
		{
			// TODO maybe add some logic
			return number++;
		}

		public Account FindAccountByName(string name)
		{
			// more than one user
			var account = ListAccounts.FirstOrDefault(mobileAcc => mobileAcc.User.Name == name);

			if (account == null)
			{
				return null;
			}

			account.OnCallHandler += CallConnection;
			account.OnCallHandler += EndCall;
			account.OnSmsHandler += SmsConnection;
			account.OnSmsHandler += ReceiveSms;

			return account;
		}

		private void CallConnection(object sender, AccountEventArgs e)
		{
			LogCallEvent("Try to call.", e, MessageType.Call);

			try
			{
				accountSender = sender as Account;
				accountReceiver = ListAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.User.Number));

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
				accountReceiver = ListAccounts.FirstOrDefault(account => e.ReceiverNumber.Equals(account.User.Number));

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

		private static bool Validate(User user)
		{
			var results = new List<ValidationResult>();
			var context = new ValidationContext(user);

			if (!Validator.TryValidateObject(user, context, results, true))
			{
				foreach (var error in results)
				{
					Console.WriteLine($"User: {user}, Error: {error.ErrorMessage}");
				}

				return false;
			}

			// Console.WriteLine($"User '{user.Name}' is Valid");

			return true;
		}

		private static bool Validate(IEnumerable<User> user)
		{
			foreach (var userValidate in user)
			{

				var results = new List<ValidationResult>();
				var context = new ValidationContext(userValidate);

				if (Validator.TryValidateObject(userValidate, context, results, true))
					continue;

				foreach (var error in results)
				{
					Console.WriteLine($"User: {userValidate}, Error: {error.ErrorMessage}");
				}

				return false;

				// Console.WriteLine($"User '{userValidate.Name}' is Valid");
			}

			return true;
		}
	}
}