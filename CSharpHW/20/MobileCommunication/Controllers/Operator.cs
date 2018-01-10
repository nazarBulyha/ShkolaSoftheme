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
			ListStandardAccounts = CreateStandardAccounts();
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

			return Validate(mobileAccount.User) ? mobileAccount : null;
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

		private List<Account> CreateStandardAccounts()
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

			Console.WriteLine($"User '{user.Name}' is Valid");

			return true;
		}

		private static bool Validate(IEnumerable<User> user)
		{
			foreach (var userValidate in user)
			{

				var results = new List<ValidationResult>();
				var context = new ValidationContext(userValidate);

				if (!Validator.TryValidateObject(userValidate, context, results, true))
				{
					foreach (var error in results)
					{
						Console.WriteLine($"User: {userValidate}, Error: {error.ErrorMessage}");
					}

					return false;
				}

				Console.WriteLine($"User '{userValidate.Name}' is Valid");
			}

			return true;
		}
	}
}