namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	internal class MobileOperator : IMobileOperator
    {
        private IMobileAccount mobileAccountSender;
        private IMobileAccount mobileAccountReceiver;
        private int number = 2219320;

        public List<IMobileAccount> MobileAccounts { get; set; } = new List<IMobileAccount>();
        public List<IMobileAccount> StandardMobileAccounts { get; set; }
        public AccountEventArgs NumberEventArgs { get; private set; }
        public ILog CallLogger { get; set; } = new Logger();

        public MobileOperator()
        {
			StandardMobileAccounts = CreateStandardMobileAccounts();
        }

        private List<IMobileAccount> CreateStandardMobileAccounts()
        {
            #region Define standard accounts for address book
            IMobileAccount standartAccount1 = new MobileAccount(this);
            IMobileAccount standartAccount2 = new MobileAccount(this);
            IMobileAccount standartAccount3 = new MobileAccount(this);
            IMobileAccount standartAccount4 = new MobileAccount(this);

            standartAccount1.Account = new Account(number: CreateNumber())
            {
                Name = "standartName1",
                Surname = "standartSurname1",
                DateBirth = DateTime.Now
            };
            standartAccount2.Account = new Account(number: CreateNumber())
            {
                Name = "standartName2",
                Surname = "standartSurname2",
                DateBirth = DateTime.Now
            };
            standartAccount3.Account = new Account(CreateNumber())
            {
                Name = "standartName3",
                Surname = "standartSurname3",
                DateBirth = DateTime.Now
            };
            standartAccount4.Account = new Account(CreateNumber())
            {
                Name = "standartName4",
                Surname = "standartSurname4s",
                DateBirth = DateTime.Now
            };

            StandardMobileAccounts = new List<IMobileAccount>();
			// because AddRange take List of elements but no params type
            StandardMobileAccounts.AddMany(standartAccount1, standartAccount2, standartAccount3, standartAccount4);
            #endregion
			
            return StandardMobileAccounts;
        }

        public IMobileAccount CreateMobileAccount(IMobileOperator mobileOperator)
        {
            var mobileAccount = new MobileAccount(this);

			MobileAccounts.Add(mobileAccount);

            mobileAccount.OnStartCallHandler += TryMakeCall;
            mobileAccount.OnEndCallHandler += EndCall;
            mobileAccount.OnStartSmsHandler += TrySendSms;
            mobileAccount.OnEndSmsHandler += ReceiveSms;

            return mobileAccount;
        }

        public IMobileAccount SetAccountParametres(IMobileAccount mobileAccount, string name, string surname, string email, DateTime dateTime)
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
				mobileAccountSender = sender as IMobileAccount;
				mobileAccountReceiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (mobileAccountSender == null || mobileAccountReceiver == null || mobileAccountReceiver.Account.Number == 0 || mobileAccountSender.Account.Number == 0 || mobileAccountSender.Account == null || mobileAccountReceiver.Account == null)
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
					mobileAccountSender.OnEndCallHandler -= EndCall;
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
					mobileAccountSender.OnEndCallHandler -= EndCall;
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
					mobileAccountSender.OnEndCallHandler -= EndCall;
				}

				LogCallEvent("Call crashed.", true);

	            Console.WriteLine(exceptionStandart.Message);
				Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call crashed");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }

			NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = mobileAccountSender.Account.Number,
                ReceiverNumber = mobileAccountReceiver.Account.Number
            };

			LogCallEvent("Try to call");

			mobileAccountReceiver.ReceiveCall(mobileAccountSender.Account.Number);
        }

        private void TrySendSms(object sender, AccountEventArgs e)
        {
            try
            {
				mobileAccountSender = (IMobileAccount)sender;
				mobileAccountReceiver = MobileAccounts.FirstOrDefault(mobileAccount => e.ReceiverNumber.Equals(mobileAccount.Account.Number));

                if (mobileAccountSender == null || mobileAccountReceiver == null || mobileAccountReceiver.Account.Number == 0 || mobileAccountSender.Account.Number == 0 || mobileAccountSender.Account == null || mobileAccountReceiver.Account == null)
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
					mobileAccountSender.OnEndSmsHandler -= ReceiveSms;
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
					mobileAccountSender.OnEndSmsHandler -= ReceiveSms;
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
					mobileAccountSender.OnEndSmsHandler -= ReceiveSms;
				}

				LogSmsEvent("Sms wasn't send.", true);

	            Console.WriteLine(standartException.Message + Environment.NewLine);
				Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sms wasn't send.");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }

			NumberEventArgs = new AccountEventArgs
            {
                SenderNumber = mobileAccountSender.Account.Number,
                ReceiverNumber = mobileAccountReceiver.Account.Number
            };

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
			CallLogger.Log(message, isError);
        }

        private void LogSmsEvent(string message, bool isError = false)
        {
			CallLogger.Log(message, isError);
        }
    }
}