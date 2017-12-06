using System;

namespace MobileCommunication.Interfaces
{
    interface ILog
    {
        string ErrorMessage { get; set; }
        string SuccessMessage { get; set; }
        DateTime DateTime { get; set; }
        string Name { get; set; }
        int Number { get; set; }

        void WriteToFileSuccess(/*parameters*/);

        //overload method WriteToFile
        void WriteToFileWithException(/*parameters*/);
    }
}