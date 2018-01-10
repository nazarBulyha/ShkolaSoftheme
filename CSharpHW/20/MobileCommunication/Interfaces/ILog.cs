namespace MobileCommunication.Interfaces
{
	using System;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;
	using MobileCommunication.Models;

	public interface ILog
	{
		LoggerMessage Message { get; set; }

		void AddLogMessage(string message, AccountEventArgs numbersArgs, MessageType messageType);

		void ShowLog(DateTime dateTime, string message, MessageType messageType);

		void ShowLog(int messageCount, string message, MessageType messageType);

		void WriteLogMessages();

		void ShowAllLog();

		void Serialize<TItem>(TItem myItem);

		TItem Deserialize<TItem>();
	}
}