using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Network_Diff
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Configuration Sort Tool";
            textBox1.AllowDrop = true;
            textBox2.AllowDrop = true;
            textBox1.DragEnter += new DragEventHandler(textBox1_DragEnter);
            textBox1.DragDrop += new DragEventHandler(textBox1_DragDrop);
            textBox2.DragEnter += new DragEventHandler(textBox2_DragEnter);
            textBox2.DragDrop += new DragEventHandler(textBox2_DragDrop);

            checkBox1.Checked = true;
            checkBox2.Enabled = false;
          

        }

    #region DragEnterCode
        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);


            string s = "";

            foreach (string File in FileList)
                s = s + " " + File;
            textBox1.Text = s;
        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);


            string s = "";

            foreach (string File in FileList)
                s = s + " " + File;
            textBox2.Text = s;
        }
    #endregion

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

    #region ButtonAction
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog2.SelectedPath;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            char log;
            if (checkBox3.Checked == true)
                log = 'K';
            else
                log = 'C';

            string date = DateTime.Now.ToString("MM.dd.yyyy_HH-mm-ss");

            string pathToSortedTNCT = "C:\\NetworkSortTool\\"+date+"\\TNCTSorted";
            DirectoryInfo directory1 = Directory.CreateDirectory(pathToSortedTNCT);
            string pathToSortedLive = "C:\\NetworkSortTool\\" + date + "\\LiveSorted";
            DirectoryInfo directory2 = Directory.CreateDirectory(pathToSortedLive);

            string strCmdText1 = "/" + log + " java -jar rSorting.jar " + textBox1.Text + " " + pathToSortedTNCT;
            var sortProcess1 = System.Diagnostics.Process.Start("CMD.exe", strCmdText1);
            string strCmdText2 = "/" + log + " java -jar rSorting.jar " + textBox2.Text + " " + pathToSortedLive;
            var sortProcess2 = System.Diagnostics.Process.Start("CMD.exe", strCmdText2);
            sortProcess1.WaitForExit();
            sortProcess2.WaitForExit();

            System.Windows.Forms.MessageBox.Show("Finished:For Sorted Configs go to C:\\NetworkSortTool");
        }

    #endregion

    }
}
