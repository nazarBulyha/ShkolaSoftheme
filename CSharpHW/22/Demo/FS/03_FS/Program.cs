using System;
using System.IO;

namespace _03_FS
{
    class Program
    {
        static void Main()
        {
            var fileName = "doc.txt";

            Console.WriteLine("Input text and press Enter:");
            var text = Console.ReadLine();

            WriteToFile(text, fileName);

            ReadFromFile(fileName);

            Console.ReadLine();
        }

        private static void ReadFromFile(string fileName)
        {
            using (var fstream = File.OpenRead(fileName))
            {
                var array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                var textFromFile = System.Text.Encoding.Default.GetString(array);
                
                Console.WriteLine("Text: {0}", textFromFile);
            }
        }

        private static void WriteToFile(string text, string fileName)
        {
            using (var fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);

                Console.WriteLine("File saved");
            }
        }
    }
}
