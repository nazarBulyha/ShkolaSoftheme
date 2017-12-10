using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace VisualCSharpZipper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();

            this.textBox1.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();

            this.textBox2.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( Directory.Exists(textBox1.Text)&& Directory.Exists(textBox2.Text))
            {
                ZipFile.CreateFromDirectory(textBox1.Text, textBox2.Text +".zip");
            }
            else
            {
                if (Directory.Exists(textBox1.Text))
                    MessageBox.Show("Zip Path is invalid");
                else
                    MessageBox.Show("Open folder path is invalid");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox3.Text) && Directory.Exists(textBox4.Text))
            {
                ZipFile.CreateFromDirectory(textBox3.Text +".zip", textBox4.Text);
            }
            else
            {
                if (Directory.Exists(textBox3.Text))
                    MessageBox.Show("Extract Path is invalid");
                else
                    MessageBox.Show("Open Zip path is invalid");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();

            this.textBox3.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();

            this.textBox4.Text = this.folderBrowserDialog1.SelectedPath;
        }
    }
}
