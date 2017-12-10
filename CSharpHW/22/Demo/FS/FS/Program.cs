using System;
using System.IO;

namespace FS
{
    class Program
    {
        static void Main()
        {
            CreateNewFile("file.txt");

            PrintDirectoryInfo();

            EnumerateFiles();
        }

        static void CreateNewFile(string fileName)
        {
            FileInfo f = new FileInfo(fileName);
            FileStream fs = f.Create();
            fs.Close();
        }

        static void PrintDirectoryInfo()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--== Directory info ==---");
            Console.ResetColor();

            Console.WriteLine("Path: {0}\nFolder name: {1}\nParent folder: {2}\nCreated : {3}\nAttributes: {4}\nRoot folder: {5}\n", 
                              dir.FullName,
                              dir.Name,
                              dir.Parent,
                              dir.CreationTime,
                              dir.Attributes,
                              dir.Root);
        }

        static void EnumerateFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Nick\Desktop\demo\other");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--== Files info ==---");
            Console.ResetColor();

            FileInfo[] imageFiles = dir.GetFiles();
            foreach (FileInfo f in imageFiles)
            {
                Console.WriteLine("File name: " + f.Name);
                Console.WriteLine("File size: " + f.Length);
                Console.WriteLine("Created: " + f.CreationTime);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
