namespace MobileCommunication.Interfaces
{
    internal interface ILog
    {
        void WriteToFile(string message, int sender, int receiver, bool isError);

        void ReadFromFile(/*parameters*/);
    }
}