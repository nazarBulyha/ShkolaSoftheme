namespace MobileCommunication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using ProtoBuf;

	[ProtoContract]
	public class Logger : ILog
	{
		[ProtoMember(1)]
		public readonly string FolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";

		[ProtoMember(2)]
		public readonly string FileName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";

		[ProtoMember(3)]
		public readonly string JsonFileName = @"Operator.json";

		[ProtoIgnore]
		private List<LoggerMessage> ListMessages { get; } = new List<LoggerMessage>();

		[ProtoIgnore]
		private LoggerMessage Message { get; set; }

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

		public void WriteMessagesToLog()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(FolderPath + FileName) ?? throw new InvalidOperationException());

			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.Append))
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

		public void ShowAllTextLog()
		{
			using (var fileStream = new FileStream(FolderPath + FileName, FileMode.OpenOrCreate))
			using (var reader = new StreamReader(fileStream))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					Console.WriteLine(line);
				}
			}
		}

		public void ShowAllJsonLog()
		{
			using (var file = File.OpenText(FolderPath + JsonFileName))
			using (var reader = new JsonTextReader(file))
			{
				var jobject = (JObject)JToken.ReadFrom(reader);

				Console.WriteLine(jobject);
			}
		}

		// TODO: realize
		public void ShowAllXmlLog()
		{

		}

		// TODO: realize
		public void ShowAllBinaryLog()
		{

		}

		public void ShowLog(DateTime dateTime, string message, MessageType messageType = MessageType.Error)
		{
			// TODO: Read and sort data from file
			using (var reader = new StreamReader(FolderPath + FileName))
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
	}
}