using System;
using System.IO;

using MobileCommunication.Interfaces;

namespace MobileCommunication
{
    public class CallLog : ILog
    {
        private readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)}\\" + "CallLogs\\";
        private StreamWriter StreamWriter { get; set; }


        public void WriteToFile(string message, int sender, int receiver, bool isError = false)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                var fileName = isError == true ? $"ErrorCallLogFor {DateTime.Now:dd_MM_yyyy}.txt" : $"CallLogFor {DateTime.Now:dd_MM_yyyy}.txt";

                using (StreamWriter = File.AppendText(path + fileName))
                {
                    var myMessage = $@"Message: {message}{Environment.NewLine} Sender {sender}{Environment.NewLine} Receiver {receiver}{Environment.NewLine} DateTime {DateTime.Now} 
                                        {Environment.NewLine}";
                    StreamWriter.WriteLine(myMessage);
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

        public void ReadFromFile()
        {

        }
    }
}