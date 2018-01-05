namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;

	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	public class Logger : ILog
	{
		private readonly string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";
		private readonly string standartLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";

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

			//if (CreateNotExcistingDirectoryAndPath())
			//	return;

			// TODO: show all log
			Console.WriteLine("All log");
		}

		public void ShowLog(DateTime dateTime, string message = null, bool isError = false)
		{
			//CreateNotExcistingDirectoryAndPath();

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