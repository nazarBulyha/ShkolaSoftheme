using System;
using System.IO;

namespace _02_FS
{
    class Program
    {
        static void Main()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Drive Name: {0}", drive.Name);
                Console.WriteLine("DriveType: {0}", drive.DriveType);
                
                if (drive.IsReady)
                {
                    Console.WriteLine("TotalSize: {0}", drive.TotalSize);
                    Console.WriteLine("TotalFreeSpace: {0}", drive.TotalFreeSpace);
                    Console.WriteLine("VolumeLabel: {0}", drive.VolumeLabel);
                }
                
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
