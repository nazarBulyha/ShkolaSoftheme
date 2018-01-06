using MobileCommunication.Models;

namespace MobileCommunication.Interfaces
{
	using System;

	using MobileCommunication.Enums;

	public interface ILog
	{
		LogMessage LoggerMessage { get; set; }

		void Log(string message, MessageType messageType);
		
		void ShowLog(DateTime dateTime, string message, MessageType messageType);

		void ShowAllLog();
	}
}