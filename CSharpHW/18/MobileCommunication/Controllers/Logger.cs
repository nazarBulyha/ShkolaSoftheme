namespace MobileCommunication.Controllers
{
	using System;
	using System.IO;

	using MobileCommunication.Interfaces;
	using MobileCommunication.Models;

	internal class Logger : ILog
    {
        private readonly string standartLogName = $"CallLogFor {DateTime.Now:dd_MM_yyyy}.json";
        private readonly string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\CallLogs\";

        public LogMessage LoggerMessage { get; set; }

        public void Log(string message, bool isError = false)
        {
			LoggerMessage = new LogMessage
			{
                Message = message,
                IsError = isError,
                DateTime = DateTime.Now
            };

            try
            {
				CheckExcisting(path, isError);

                //using (var file = File.AppendText(path + standartLogName))
                {
	                //var jSerializer = new JsonSerializer { Formatting = Formatting.Indented };

	                //jSerializer.Serialize(file, LoggerMessage);
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
                //using (var streamReader = File.OpenText(path + standartLogName))
                {
                    //var jSerializer = new JsonSerializer();
                    //var derializeFromJson = (Logger)jSerializer.Deserialize(streamReader, typeof(Logger));

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

        public void CheckExcisting(string filePath, bool isError = false)
        {
	        if (Directory.Exists( filePath )) return;
	        if (File.Exists(filePath + standartLogName)) return;

			Directory.CreateDirectory(filePath);
	        File.Create(filePath + standartLogName);
        }
    }
}