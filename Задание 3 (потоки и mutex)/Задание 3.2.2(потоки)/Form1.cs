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

namespace Задание_3._2._2_потоки_
{
    public partial class Form1 : Form
    {
        //bool mut;
        //bool flag = false;
        static Mutex mutex1;
        //Thread thread1;
        //string text_file;
        //string str;
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public Form1()
        {
            InitializeComponent();

            //mutex1 = new Mutex(true, "mut", out mut);
            //mutex.WaitOne();
            try { mutex1 = Mutex.OpenExisting("mut"); }
            catch (WaitHandleCannotBeOpenedException ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            //thread1 = new Thread(new ThreadStart(ReadingFromFile));
            /*try
            {
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source.txt", FileMode.Open, FileAccess.Read));
                //richTextBox1.Clear();
                //richTextBox1.Text = SW.ReadToEnd();
                text_file = SW.ReadToEnd();
                SW.Close();
                //flag = true;
            }
            catch (FileNotFoundException)
            {

            }*/
        }


        private void Form1_Activated(object sender, EventArgs e)
        {
            
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //mut = mutex1.WaitOne(0);
            //Thread.Sleep(1);
            if (mutex1.WaitOne(0))
            {
                try
                {
                    StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source.txt", FileMode.Open, FileAccess.Read));

                    richTextBox1.Clear();
                    richTextBox1.Text = SW.ReadToEnd();
                    SW.Close();
                    mutex1.ReleaseMutex();
                }
                catch (FileNotFoundException)
                {
                    mutex1.ReleaseMutex();
                    MessageBox.Show("Файл и мьютекс не были созданы!", "Приложение 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //mutex1.ReleaseMutex();
            }
            else /*if (!flag)*/
            {
                //richTextBox1.Clear();
                MessageBox.Show("В данный момент файл занят другой программой (ожидание освобождения мьютекса)!", "Приложение 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }
        /*private void ReadingFromFile()
        {
            mutex1.WaitOne();
            str = "\nМьютекс освобожден другой программой.\n";
            try
            {
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source.txt", FileMode.Open, FileAccess.Read));

                //richTextBox1.Clear();
                str += SW.ReadToEnd();
                SW.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не был создан или неправильно\nуказано его название!", "Приложение 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mutex1.ReleaseMutex();
        }*/

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(desktop + "\\Source.txt"))
                File.Delete(desktop + "\\Source.txt");
        }
    }
}
