namespace MobileCommunication.Interfaces
{
	using System;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Models;

	public interface ILog
	{
		LogMessage LoggerMessage { get; set; }

		void Log(string message, AccountEventArgs numbersArgs, MessageType messageType);
		
		void ShowLog(DateTime dateTime, string message, MessageType messageType);

		void ShowAllLog();
	}
}