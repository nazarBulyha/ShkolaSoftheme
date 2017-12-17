namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;

	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	internal class Logger : ILog
	{
		private readonly string standartLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";
		private readonly string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";

		public LogMessage LoggerMessage { get; set; }

		public void Log(string message, bool isError = false)
		{
			LoggerMessage = new LogMessage
			{
				Message = message,
				IsError = isError,
				DateTime = DateTime.Now
			};
		}

		public void ShowAllLog()
		{

			CreateDirectoryAndPathIfNotExcist(path);

			// TODO: write to file .txt
		}

		public void ShowLog(DateTime dateTime, string message = null, bool isError = false)
		{
			CreateDirectoryAndPathIfNotExcist(path);

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

		public void CreateDirectoryAndPathIfNotExcist(string filePath, bool isError = false)
		{
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}

			if (!File.Exists(filePath + standartLogName))
			{
				File.Create(filePath + standartLogName);
			}
		}
	}
}