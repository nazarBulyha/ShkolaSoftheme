using System;
using System.IO;
using System.Text;

namespace _04_FS
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = "doc.txt";

            ReadAllFile(filePath);

            ReadFileLineByLine(filePath);

            ReadBlockFromFile(filePath, 4);
        }

        private static void ReadBlockFromFile(string filePath, int blockSize)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Read file by blocks");
            Console.ResetColor();

            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                var array = new char[blockSize];
                reader.Read(array, 0, blockSize);

                Console.WriteLine(array);
            }
        }

        private static void ReadFileLineByLine(string filePath)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Read file line by line");
            Console.ResetColor();

            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine();
        }

        private static void ReadAllFile(string filePath)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Read all file");
            Console.ResetColor();
            
            using (var reader = new StreamReader(filePath))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            
            Console.WriteLine();
        }
    }
}
