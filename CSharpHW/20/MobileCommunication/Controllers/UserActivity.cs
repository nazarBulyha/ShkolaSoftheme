using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCommunication.Controllers
{
	using System.IO;
	using System.Text.RegularExpressions;

	using MobileCommunication.Interfaces;

	public static class UserActivity
	{
		private static readonly List<List<string>> MessageBlocksList = new List<List<string>>();

		private static List<string> fileContent = new List<string>();

		private static double senderMaxCount, receiverMaxCount;

		private static string maxSenderAccountName, maxReceiverAccountName;

		private static readonly string FinalMessage = $"Most SENDER POINTS has User: {maxSenderAccountName}, points: {senderMaxCount}"
		                                     + $"\nMost RECEIVER POINTS has User: {maxReceiverAccountName}, points: {receiverMaxCount}";

		public static void GetMostActiveUser(string filePath, List<Account> accountList)
		{
			CheckFileExist(filePath);
			ReadAllMessages(filePath);

			foreach (var user in accountList)
			{
				double senderCount = 0, receiverCount = 0;

				foreach (var messageBlock in MessageBlocksList)
				foreach (var message in messageBlock)
					{
						if (!message.Contains("Try to call") && !message.Contains("Try to send sms") && 
							!message.Contains("Call ended") && !message.Contains("Sms received"))
						{
							return;
						}

						if (message.Contains("Try to call") && MatchRegex(user, messageBlock, "Sender"))
						{
							senderCount++;
						}
						else if (message.Contains("Call ended") && MatchRegex(user, messageBlock, "Receiver"))
						{
							receiverCount++;
						}
						else if (message.Contains("Try to send sms") && MatchRegex(user, messageBlock, "Sender"))
						{
							senderCount += 0.5;
						}
						else if (message.Contains("Sms received") && MatchRegex(user, messageBlock, "Receiver"))
						{
							receiverCount += 0.5;
						}
					}

				CompareWithMaxNumbers(user, senderCount, receiverCount);
			}

			Console.WriteLine(FinalMessage);
		}

		// TODO: realize this shit
		public static List<Account> GetMostActiveUsers()
		{

			return null;
		}

		private static void CheckFileExist(string filePath)
		{
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
		}

		private static void ReadAllMessages(string filePath)
		{
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
						MessageBlocksList.Add(fileContent);
						fileContent = new List<string>();
					}
				}
			}
		}

		private static bool MatchRegex(IAccount user, List<string> messageBlock, string text)
		{
			return Regex.Match(messageBlock.Find(s => s.Contains(text)), @"\d+")
			     .Value == user.User.Number.ToString();
		}

		private static void CompareWithMaxNumbers(IAccount user, double senderCount, double receiverCount)
		{
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
	}
}