using System;
using System.IO;

using MobileCommunication.Interfaces;
using MobileCommunication.Models;
using Newtonsoft.Json;

namespace MobileCommunication
{
    internal class CallLog : ILog
    {
        private string currentLog;
        private string standartLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.json";
        private string standartLogErrorName = $"ErrorCallLogFor {DateTime.Now:dd_MM_yyyy}.json";
        private readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\\CallLogs\\";

        public LogMessage LoggerMessage { get; set; }

        public void Log(int sender, int receiver, string message, bool isError = false)
        {
            LoggerMessage = new LogMessage()
            {
                Sender = sender,
                Receiver = receiver,
                Message = message,
                IsError = isError,
                DateTime = DateTime.Now
            };

            try
            {
                CheckExcisting(path, isError);

                using (StreamWriter file = File.AppendText(path + currentLog))
                {
                    JsonSerializer jSerializer = new JsonSerializer();
                    jSerializer.Formatting = Formatting.Indented;

                    jSerializer.Serialize(file, LoggerMessage);
                }
            }
            catch (NullReferenceException nullException)
            {
                Console.WriteLine(nullException.Message);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void ShowAllLog()
        {
            try
            {
                CheckExcisting(path);

                // TODO: Fix LoggerMessage = null for deserialization
                using (var StreamReader = File.OpenText(path + currentLog))
                {
                    JsonSerializer jSerializer = new JsonSerializer();
                    var derializeFromJson = (CallLog)jSerializer.Deserialize(StreamReader, typeof(CallLog));

                    //Console.WriteLine(derializeFromJson.ToString());
                }
            }
            catch (NullReferenceException nullException)
            {
                Console.WriteLine(nullException.Message);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        public void ShowLog(DateTime dateTime, string message = null, int sender = 0, int receiver = 0, bool isError = false)
        {
            CheckExcisting(path);

            using (StreamReader reader = new StreamReader(path + standartLogName))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("DateTime"))
                    {

                    }
                }
            }
        }

        public void CheckExcisting(string path, bool isError = false)
        {
            currentLog = isError == true ? standartLogErrorName : standartLogName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                if (!File.Exists(path + currentLog))
                {
                    File.Create(path + currentLog);
                }
            }
        }
    }
}