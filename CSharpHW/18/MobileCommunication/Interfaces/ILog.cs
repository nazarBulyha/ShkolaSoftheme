using MobileCommunication.Models;

namespace MobileCommunication.Interfaces
{
	using System;

	public interface ILog
	{
		LogMessage LoggerMessage { get; set; }

		void Log(string message, bool isError);

		void ShowAllLog();

		void ShowLog(DateTime dateTime, string message = null, bool isError = false);

		void CreateDirectoryAndPathExcisting(string path, bool isError);
	}
}