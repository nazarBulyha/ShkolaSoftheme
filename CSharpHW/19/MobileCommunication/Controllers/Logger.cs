namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;
	using System.Xml.Serialization;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Models;

	public static class Logger
	{
		public static readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		public static readonly string CallLoggerName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";
		public static readonly string SerializedItemName = @"Operator.txt";

		public static LogMessage LoggerMessage { get; set; }

		public static void Log(string message, AccountEventArgs numbersArgs, MessageType messageType)
		{
			LoggerMessage = new LogMessage
			{
				MessageType = messageType,
				Message = message,
				Sender = numbersArgs.SenderNumber,
				Receiver = numbersArgs.ReceiverNumber,
				DateTime = DateTime.Now
			};

			if (!Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}

			using (var fileStream = new FileStream(FolderPath + CallLoggerName, FileMode.Append))
			using (var writter = new StreamWriter(fileStream))
			{
				writter.WriteLine($"MessageType: {LoggerMessage.MessageType}");
				writter.WriteLine($"Message: {LoggerMessage.Message}");
				writter.WriteLine($"Sender: {LoggerMessage.Sender}");
				writter.WriteLine($"Receiver: {LoggerMessage.Receiver}");
				writter.WriteLine($"Date and time: {LoggerMessage.DateTime}");
				writter.WriteLine();
			}
		}

		public static void ShowAllLog()
		{
			using (var fileStream = new FileStream(FolderPath + CallLoggerName, FileMode.OpenOrCreate))
			using (var reader = new StreamReader(fileStream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					// TODO: make readable sort
					Console.WriteLine(line);
				}
			}
		}

		public static void ShowLog(DateTime dateTime, string message, MessageType messageType = MessageType.Error)
		{
			// TODO: Read and sort data from file
			using (var reader = new StreamReader(FolderPath + CallLoggerName))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (line.Contains("DateTime"))
					{

					}
				}
			}
		}

		public static void Serialize<TItem>(TItem myItem)
		{
			var serializer = new XmlSerializer(typeof(TItem));

			using (var fileStream = new FileStream(FolderPath + SerializedItemName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				serializer.Serialize(fileStream, myItem);
			}
		}

		public static object Deserialize<TItem>()
		{
			if (!File.Exists(FolderPath + SerializedItemName))
			{
				return Activator.CreateInstance(typeof(TItem));
			}

			using (var fileStream = new FileStream(FolderPath + SerializedItemName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				return (TItem)serializer.Deserialize(fileStream);
			}
		}
	}
}