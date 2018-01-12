namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml.Serialization;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class Logger : ILog
	{
		public readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		public readonly string CallLoggerFileName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";
		public readonly string SerializedFileName = @"Operator.txt";

		public List<LoggerMessage> ListMessages { get; set; } = new List<LoggerMessage>();

		public LoggerMessage Message { get; set; }

		public void AddLogMessage(string message, AccountEventArgs numbersArgs, MessageType messageType)
		{
			Message = new LoggerMessage
			{
				MessageType = messageType,
				Message = message,
				Sender = numbersArgs.SenderNumber,
				Receiver = numbersArgs.ReceiverNumber,
				DateTime = DateTime.Now
			};

			ListMessages.Add(Message);
		}

		public void WriteLogMessages()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + SerializedFileName) ?? throw new InvalidOperationException());

			using (var fileStream = new FileStream(FolderPath + CallLoggerFileName, FileMode.Append))
			using (var writer = new StreamWriter(fileStream))
			{
				foreach (var message in ListMessages)
				{
					writer.WriteLine($"MessageType: {message.MessageType}");
					writer.WriteLine($"Message: {message.Message}");
					writer.WriteLine($"Sender: {message.Sender}");
					writer.WriteLine($"Receiver: {message.Receiver}");
					writer.WriteLine($"Date and time: {message.DateTime}");
					writer.WriteLine();
				}
			}

			ListMessages.Clear();
		}

		public void ShowAllLog()
		{
			using (var fileStream = new FileStream(FolderPath + CallLoggerFileName, FileMode.OpenOrCreate))
			using (var reader = new StreamReader(fileStream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					Console.WriteLine(line);
				}
			}
		}

		public void ShowLog(DateTime dateTime, string message, MessageType messageType = MessageType.Error)
		{
			// TODO: Read and sort data from file
			using (var reader = new StreamReader(FolderPath + CallLoggerFileName))
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

		public void ShowLog(int messageCount, string message, MessageType messageType = MessageType.Error)
		{
			// TODO: Read and sort data from file
		}

		public void Serialize<TItem>(TItem myItem)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + SerializedFileName) ?? throw new InvalidOperationException());

			var serializer = new XmlSerializer(typeof(TItem));

			using (var fileStream = new FileStream(FolderPath + SerializedFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				serializer.Serialize(fileStream, myItem);
			}
		}

		public TItem Deserialize<TItem>()
		{
			if (!File.Exists(FolderPath + SerializedFileName))
			{
				return (TItem)Activator.CreateInstance(typeof(TItem));
			}

			using (var fileStream = new FileStream(FolderPath + SerializedFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(TItem));

				return (TItem)serializer.Deserialize(fileStream);
			}
		}
	}
}