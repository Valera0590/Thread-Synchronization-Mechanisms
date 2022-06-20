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

namespace Задание_3._2._1_потоки_
{
    public partial class Form1 : Form
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //bool flag;
        //StreamWriter sw = new StreamWriter(desktop + "\\Test.txt");
        static Mutex mutex = new Mutex(false, "mut");
        //static Mutex mutex = new Mutex(true, "mut");
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            //mutex.WaitOne();
            //flag = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (mutex.WaitOne(0)) 
            {
            //if (flag) mutex.WaitOne();
            //else flag = true;
                StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Source.txt", FileMode.Create, FileAccess.Write));
                SW.Write(richTextBox1.Text);
                SW.Close();
                MessageBox.Show("Текст успешно записан в файл Source.txt\nна вашем рабочем столе!", "Приложение 1",MessageBoxButtons.OK,MessageBoxIcon.Information);
                mutex.ReleaseMutex();
            }
            else MessageBox.Show("В данный момент файл занят другой программой (ожидание освобождения мьютекса)! Попробуйте позже.", "Приложение 1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //if (!flag) { mutex.ReleaseMutex(); flag = true; }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(File.Exists(desktop + "\\Source.txt"))
                File.Delete(desktop + "\\Source.txt");
        }
    }
}
