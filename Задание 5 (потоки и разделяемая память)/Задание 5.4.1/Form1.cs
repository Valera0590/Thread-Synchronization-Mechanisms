using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание_5._4._1
{
    public partial class Form1 : Form
    {
        MemoryMappedFile sharedMemory;
        MemoryMappedFile sharedMemory1;
        MemoryMappedFile sharedMemory2;
        bool[] actions = {false, false, false, false, false, false };
        bool close = false;
        public Form1()
        {
            Thread.Sleep(200);
            InitializeComponent();
            sharedMemory1 = MemoryMappedFile.CreateOrOpen("MemoryClosing1", 2);
            Thread th = new Thread(() => Listener());
            th.Start();
            
            
        }

        void Listener()
        {
            Thread.Sleep(100);
            sharedMemory2 = MemoryMappedFile.OpenExisting("MemoryClosing2");
            bool closing = false;
            while (closing == false)
            {
                using (MemoryMappedViewAccessor reader = sharedMemory2.CreateViewAccessor(0, 2, MemoryMappedFileAccess.Read))
                {
                    //Массив символов сообщения
                    closing = reader.ReadBoolean(0);
                }
                Thread.Sleep(50);
            }
            //this.Close();
            //this.Dispose();
            Application.Exit();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            actions[0] = true;
            //Создание участка разделяемой памяти
            //Первый параметр - название участка, 
            //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
            //плюс четыре байта для одного объекта типа Integer
            sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", 2 * actions.Length);
            //Создаем объект для записи в разделяемый участок памяти
            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, 2 * actions.Length))
            {
                //запись в разделяемую память
                //запись сообщения с первого байта в разделяемой памяти
                writer.WriteArray<bool>(0, actions, 0, actions.Length);
            }
            actions[0] = false;
            checkBox1.Checked = false;
            actions[1] = false;
            checkBox2.Checked = false;
            actions[2] = false;
            checkBox3.Checked = false;
            actions[3] = false;
            checkBox4.Checked = false;
            actions[4] = false;
            checkBox5.Checked = false;
            actions[5] = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) actions[1] = true;
            else actions[1] = false;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) actions[2] = true;
            else actions[2] = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true) actions[3] = true;
            else actions[3] = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true) actions[4] = true;
            else actions[4] = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true) actions[5] = true;
            else actions[5] = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            close = true;
            //Создание участка разделяемой памяти
            //Первый параметр - название участка, 
            //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
            //плюс четыре байта для одного объекта типа Integer
            sharedMemory1 = MemoryMappedFile.CreateOrOpen("MemoryClosing1", 2);
            //Создаем объект для записи в разделяемый участок памяти
            using (MemoryMappedViewAccessor writer = sharedMemory1.CreateViewAccessor(0, 2))
            {
                //запись в разделяемую память
                //запись сообщения с первого байта в разделяемой памяти
                writer.Write(0, close);
            }
        }
    }
}
