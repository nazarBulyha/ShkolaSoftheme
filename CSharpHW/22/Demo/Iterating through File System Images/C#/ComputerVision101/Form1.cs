using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerVision101
{
    public partial class Form1 : Form
    {
        public readonly List<string> ImageExtensions =
            new List<string> { ".jpg", ".jpe", "jpeg", ".bmp", ".gif", ".png" }; // Will do for now
        private bool STOP = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            pbImages.LoadCompleted += pbImages_LoadCompleted;
            if (btnStartStop.Text != "Stop")
            {
                DialogResult dr = FBD.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    btnStartStop.Text = "Stop";
                    var files = Directory.GetFiles(FBD.SelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (STOP) break; // Exit the loop if the StartStop button is pressed

                        FileInfo finfo = new FileInfo(file);
                        if (ImageExtensions.Contains(finfo.Extension.ToLowerInvariant()))
                        {
                            try
                            {
                                pbImages.Image = null;
                                pbImages.Load(file);
                                Thread.Sleep(1000);
                                pbImages.Refresh();
                                Application.DoEvents();
                            }
                            catch (Exception)
                            {
                                
                            }
                            
                        }
                        Application.DoEvents();
                    }
                    STOP = false;
                }
            }
            else
            {
                STOP = true;
                btnStartStop.Text = "Start";
            }
        }

        void pbImages_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Thread.Sleep(100);
            pbImages.Refresh();
            Application.DoEvents();
        }
    }
}
