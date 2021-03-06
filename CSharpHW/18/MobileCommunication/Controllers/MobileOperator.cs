﻿namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Serialization;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class MobileOperator : IMobileOperator
	{
		public List<MobileAccount> MobileAccounts { get; set; }

		[XmlIgnore]
		public List<Account> StandardMobileAccounts { get; set; }

		[XmlIgnore]
		public Logger Logger { get; set; }

		private MobileAccount mobileAccountSender;

		private MobileAccount mobileAccountReceiver;

		private int number = 2219320;

		public MobileOperator()
		{
			StandardMobileAccounts = CreateStandardMobileAccounts();
			MobileAccounts = new List<MobileAccount>();
			Logger = new Logger();
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

			mobileAccount.OnCallHandler += EndCall;
			mobileAccount.OnCallHandler += TryMakeCall;
			mobileAccount.OnSmsHandler += ReceiveSms;
			mobileAccount.OnSmsHandler += TrySendSms;

			return mobileAccount;
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
					mobileAccountSender.OnCallHandler -= EndCall;
				}

				LogCallEvent("Call crashed.", true);

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Call crashed");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}
			catch (ArgumentException)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnCallHandler -= EndCall;
				}

				LogCallEvent("Call crashed.", true);

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Call crashed");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}
			catch (Exception exceptionStandart)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnCallHandler -= EndCall;
				}

				LogCallEvent("Call crashed.", true);

				Console.WriteLine(exceptionStandart.Message);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Call crashed");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}

			LogCallEvent("Try to call");

			mobileAccountReceiver.ReceiveCall(mobileAccountSender.Account.Number);
		}

		private void TrySendSms(object sender, AccountEventArgs e)
		{
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

				LogSmsEvent("Sms wasn't send.", true);

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sms wasn't send.");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}
			catch (ArgumentException)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", true);

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sms wasn't send.");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}
			catch (Exception standartException)
			{
				if (mobileAccountSender != null)
				{
					mobileAccountSender.OnSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", true);

				Console.WriteLine(standartException.Message + Environment.NewLine);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sms wasn't send.");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.White;

				return;
			}

			LogSmsEvent("Try to send sms");

			mobileAccountReceiver.ReceiveSms(mobileAccountSender.Account.Number);
		}

		// TODO: Implement logic after receiving Call
		private void EndCall(object sender, AccountEventArgs e)
		{
			// end call for both users
			// if number doesn't exists, end call for one user
			LogSmsEvent("Call ended.");
		}

		private void ReceiveSms(object sender, AccountEventArgs e)
		{
			// if sender number isn't in blocked numbers than receive sms
			LogSmsEvent("Sms received.");
		}

		private void LogCallEvent(string message, bool isError = false)
		{
			Logger.Log(message, isError);
		}

		private void LogSmsEvent(string message, bool isError = false)
		{
			Logger.Log(message, isError);
		}
	}
}