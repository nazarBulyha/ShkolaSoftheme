using MobileCommunication.Models;
using System;

namespace MobileCommunication.Interfaces
{
    internal interface ILog
    {
        LogMessage LoggerMessage { get; set; }

        void Log(string message, bool isError);

        void ShowAllLog();

        void ShowLog(DateTime dateTime, string message, int sender, int receiver, bool isError = false);

        void CheckExcisting(string path, bool isError);
    }
}