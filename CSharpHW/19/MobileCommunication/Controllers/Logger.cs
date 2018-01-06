namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;

	using MobileCommunication.Enums;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class Logger : ILog
	{
		private readonly string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		private readonly string standartLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";

		public LogMessage LoggerMessage { get; set; }

		public void Log(string message, MessageType messageType)
		{
			LoggerMessage = new LogMessage
			{
				Message = message,
				MessageType = messageType,
				DateTime = DateTime.Now
			};

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			using (var fileStream = new FileStream(path + standartLogName, FileMode.Append))
			using (var writter = new StreamWriter(fileStream))
			{
				writter.WriteLine($"MessageType: {LoggerMessage.MessageType}");
				writter.WriteLine($"Message: {LoggerMessage.Message}");
				writter.WriteLine($"Date and time: {LoggerMessage.DateTime}");
				writter.WriteLine();
			}
		}

		public void ShowAllLog()
		{
			using (var fileStream = new FileStream(path + standartLogName, FileMode.OpenOrCreate))
			{
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
		}

		public void ShowLog(DateTime dateTime, string message, MessageType messageType = MessageType.Error)
		{
			// TODO: Read and sort data from file
			using (var reader = new StreamReader(path + standartLogName))
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
	}
}