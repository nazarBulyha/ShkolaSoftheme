using System;
using System.IO;

namespace _05_FS
{
    class Program
    {
        static void Main()
        {
            var filePath = "doc.bin";

            Write(filePath);

            Read(filePath);
        }

        private static void Write(string filePath)
        {
            BinaryWriter writer = null;

            int intValue = 10;
            double doubleValue = 1023.56;
            bool boolValue = true;
            string stringValue = "Some text";

            try
            {
                writer = new BinaryWriter(new FileStream(filePath, FileMode.OpenOrCreate));
                writer.Write(intValue);
                writer.Write(doubleValue);
                writer.Write(boolValue);
                writer.Write(stringValue);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private static void Read(string filePath)
        {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    int intValue = reader.ReadInt32();
                    double doubleValue = reader.ReadDouble();
                    bool boolValue = reader.ReadBoolean();
                    string stringValue = reader.ReadString();

                    Console.WriteLine("intValue: {0}  doubleValue: {1}  boolValue: {2} stringValue: {3}",
                        intValue,
                        doubleValue,
                        boolValue,
                        stringValue);
                }
            }
        }
    }
}
