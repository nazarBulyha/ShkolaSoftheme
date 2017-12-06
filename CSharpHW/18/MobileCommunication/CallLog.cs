using System;
using MobileCommunication.Interfaces;

namespace MobileCommunication
{
    class CallLog : ILog
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public void WriteToFileSuccess()
        {
            throw new NotImplementedException();
        }

        public void WriteToFileWithException()
        {
            throw new NotImplementedException();
        }
    }
}