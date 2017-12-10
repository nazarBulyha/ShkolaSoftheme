using MobileCommunication.Models;
using System;

namespace MobileCommunication.Interfaces
{
    internal interface ILog
    {
        LogMessage LogMessage { get; set; }

        void Log(int sender, int receiver, string message, bool isError);

        void ShowAllLog();

        void ShowLog(DateTime dateTime, string message, int sender, int receiver, bool isError = false);

        void CheckExcisting(string path, bool isError);
    }
}