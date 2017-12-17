namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;

	using MobileCommunication.Additional;
	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class Logger : ILog
	{
		private readonly string standardLogName = Global.StandardLogName;
		private readonly string path = Global.Path;

		public LogMessage LoggerMessage { get; set; }

		public void Log(string message, bool isError = false)
		{
			LoggerMessage = new LogMessage
			{
				Message = message,
				IsError = isError,
				DateTime = DateTime.Now
			};

			// TODO: write to file .txt
			using (var writter = new StreamWriter(path + standardLogName))
			{
				writter.WriteLine(isError ? $"Error: {message}" : message);
			}
		}

		public void ShowAllLog()
		{
			CreateDirectoryAndPathExcisting(path);

			using (var reader = new StreamReader(path + standardLogName))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					Console.WriteLine(line);
				}
			}
		}

		public void ShowLog(DateTime dateTime, string message = null, bool isError = false)
		{
			CreateDirectoryAndPathExcisting(path);

			// TODO: Read and sort data from file
			using (var reader = new StreamReader(path + standardLogName))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					// if ( sort statement )
					Console.WriteLine(line);
				}
			}
		}

		public void CreateDirectoryAndPathExcisting(string filePath, bool isError = false)
		{
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}

			if (!File.Exists(filePath + standardLogName))
			{
				File.Create(filePath + standardLogName);
			}
		}
	}
}