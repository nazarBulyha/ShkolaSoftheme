using System;
using System.IO;

using MobileCommunication.Interfaces;
using MobileCommunication.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileCommunication
{
    internal class CallLog : ILog
    {
        private string fileName;
        private readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\\CallLogs\\";

        private StreamWriter StreamWriter { get; set; }
        private StreamReader StreamReader { get; set; }

        public LogMessage LogMessage { get; set; }

        public void Log(int sender, int receiver, string message, bool isError = false)
        {
            LogMessage = new LogMessage()
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

                // write to JSon
                using (StreamWriter = File.AppendText(path + fileName))
                {
                    string serializeToJson = JsonConvert.SerializeObject(LogMessage, Formatting.Indented);

                    StreamWriter.WriteLine(serializeToJson);
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
            CheckExcisting(path);

            //var deserializedProduct = JsonConvert.DeserializeObject<CallLog>(path + fileName);

            using (StreamReader reader = File.OpenText(path + fileName))
            {
                JObject jsonObject = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                // do stuff
                var name = jsonObject["Message"].Values();
                string date = (string)jsonObject["DateTime"];

                foreach (var json in jsonObject)
                {
                    Console.WriteLine(json.Key + " " + json.Value);
                }
            }
        }

        public void ShowLog(DateTime dateTime, string message = null, int sender = 0, int receiver = 0, bool isError = false)
        {
            CheckExcisting(path);

            using (StreamReader reader = new StreamReader(path + fileName))
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
            fileName = isError == true ? 
                       $"ErrorCallLogFor {DateTime.Now:dd_MM_yyyy}.txt" : 
                       $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                if (!File.Exists(path + fileName))
                {
                    File.Create(path + fileName);
                }
            }
        }
    }
}