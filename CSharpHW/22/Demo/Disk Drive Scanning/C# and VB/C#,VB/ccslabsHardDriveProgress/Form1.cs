using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace ccslabsHardDriveProgress
{
    public partial class Form1 : Form
    {
        #region "API Stuff"
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindFirstFileW(string lpFileName, out WIN32_FIND_DATAW lpFindFileData);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATAW lpFindFileData);

        [DllImport("kernel32.dll")]
        public static extern bool FindClose(IntPtr hFindFile);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAW
        {
            public FileAttributes dwFileAttributes;
            internal System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
        #endregion

        bool KeepRunning = true;
        double HDSpaceUsed = 0;
        double FileSizeTotal = 0;

        public Form1()
        {
            InitializeComponent();
            string[] txt = { "Remaining", "Done" };
            pccDiskDriveControl.Texts = txt;
            pccDiskDriveControl.ShadowStyle = System.Drawing.PieChart.ShadowStyle.GradualShadow;
            Color[] cols = { Color.Red, Color.Green };
            pccDiskDriveControl.Colors = cols;
            pccDiskDriveControl.InitialAngle = 45;
            pccDiskDriveControl.SliceRelativeHeight = 0.2f;
            
        }



        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "Start")
            {
                KeepRunning = true;
                FileSizeTotal = 0;
                btnStartStop.Text = "Stop";
                Start();
            }
            else // must be stop then :P
            {
                btnStartStop.Text = "Start";
                KeepRunning = false;
            }
        }

        // Run the scan and update the progress
        private void Start()
        {
            // We can either collect all the files and then process which would be easier as we would know in advance how many files we had to process.
            // However, for this we will show the progress before knowing how many files we are going to process.
            // We will of course still need to know the total amount to do so we can calculate 100%. The easiest and quickest total we can get is the amount of
            // Hard drive space that has been used by the files. This is our 100%, and as each file is discovered we will get its size and that will
            // be the percentage done. Which will be shown in the graphical progress.

            string[] allDrives = Environment.GetLogicalDrives();
            DriveInfo dinfo = new DriveInfo(allDrives[1]); // 0 = A, 1 = C
            HDSpaceUsed = (dinfo.TotalSize - dinfo.TotalFreeSpace);
            decimal[] values = { (decimal)HDSpaceUsed, 0 };
            // HDSpaceUSed = 100%

            // Setup the Graphics
            DoChart(values);

            // Run the Scan
            Scan(allDrives[1].ToString());


        }

        private void Scan(string directory)
        {
            if (KeepRunning)
            {
                IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
                WIN32_FIND_DATAW findData;
                IntPtr findHandle = INVALID_HANDLE_VALUE;


                try
                {
                    findHandle = FindFirstFileW(directory + @"\*", out findData);
                    if (findHandle != INVALID_HANDLE_VALUE)
                    {

                        do
                        {
                            Application.DoEvents();
                            try
                            {
 if (findData.cFileName == "." || findData.cFileName == "..") continue;

                            string fullpath = directory + (directory.EndsWith("\\") ? "" : "\\") + findData.cFileName;
                            if ((findData.dwFileAttributes & FileAttributes.Directory) != 0)
                            {
                                Scan(fullpath);
                            }

                            FileInfo finfo = new FileInfo(fullpath);
                            FileSizeTotal += finfo.Length;

                            decimal[] values = { (decimal)HDSpaceUsed, (decimal)FileSizeTotal };
                            DoChart(values);
                            lblFilename.Text = finfo.Name;
                            }
                            catch (Exception)
                            {
                               // ignore errors for this demo
                            }
                           



                        }
                        while (FindNextFile(findHandle, out findData));

                    }
                }
                finally
                {
                    if (findHandle != INVALID_HANDLE_VALUE) FindClose(findHandle);
                }

            }
            else
            {
                // Do not do anything
            }
        }

        // Show and update the pie chart
        private void DoChart(decimal[] values)
        {
            pccDiskDriveControl.Values = values;
        }



    }
}
