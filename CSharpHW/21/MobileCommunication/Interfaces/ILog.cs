namespace MobileCommunication.Interfaces
{
	using System;

	using MobileCommunication.Enums;
	using MobileCommunication.Extensions;

	public interface ILog
	{
		void AddLogMessage(string message, AccountEventArgs numbersArgs, MessageType messageType);

		void ShowLog(DateTime dateTime, string message, MessageType messageType);

		void ShowLog(int messageCount, string message, MessageType messageType);

		void ShowAllTextLog();

		void ShowAllJsonLog();

		void ShowAllXmlLog();

		void ShowAllBinaryLog();

		void WriteMessagesToLog();
	}
}