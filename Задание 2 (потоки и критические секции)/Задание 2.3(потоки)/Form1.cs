using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание_2._3_потоки_
{
    
    public partial class Form1 : Form
    {   
        private int m;
        private double result;
        private object locker = new object();
        private object locker2 = new object();
        private object locker3 = new object();
        private List<double> rng = new List<double>();
        private double F(double x) => (-1)*2*x*x+2;//x * (x - 1); 

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox4.Text != ""))
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox4.Text != ""))
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox4.Text != ""))
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace и запятая
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace  и запятая
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch!= 44 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace  и запятая
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            result = 0;
            double a = Convert.ToDouble(textBox1.Text), b = Convert.ToDouble(textBox2.Text);
            int n = Convert.ToInt32(textBox4.Text);
            Random rnd = new Random(DateTime.Now.Millisecond);
            m = rnd.Next(n / 10 + 1 , n * 10);
            textBox3.Text = Convert.ToString(m);
            //List<double> rng = new List<double>();
            //ParameterizedThreadStart Rectangle = new ParameterizedThreadStart(CentralRectangle);
            //List<Thread> threads = new List<Thread>();
            double interval = (b - a) / n;
            double left = a;
            for (int i = 0; i < n; i++, left += interval)
            {
                //threads.Add(new Thread(Rectangle));
                rng.Add(left);
                rng.Add(left+interval);
                Thread.Sleep(20);
                new Thread(new ParameterizedThreadStart(CentralRectangle)).Start(i);
                //threads[i].Start(rng);
                //rng.RemoveAt(0);
                //rng.RemoveAt(0);
            }
            //for (int j = 0; j < n; j++)
            //    threads[j].Join();
            //threads.Clear();
            Thread.Sleep(1000);
            rng.Clear();
            textBox5.Text = Convert.ToString(result);
            button1.Enabled = true;
        }

        private void CentralRectangle(object ind)
        {
            //List<double> range;// = new List<double>();
            //range = (List<double>) list;
            double a = rng[2*(int)ind], b = rng[2 * (int)ind + 1];
            double h = (b - a) / m;
            double x, sum;

            //lock (locker2)
            //{
            sum = (F(a) + F(b)) / 2;//0;
            //for (double i = a; i <= b; i += h)
            //{
            //                            //x = a + h * i;
            //    if (i<b)
            //    {
            //        x = i + h / 2;
            //                            //if (x > b) x = b;
            //        sum += F(x);
            //    }
            //}
            //result += h * sum;
            //}
            for (int i = 1; i < m; i++)
            {
                x = a + h * i;
                //if (x > b) x = b;
                //lock (locker3)
                //{
                    sum += F(x);
                //}
            }
            Thread.Sleep(20);
            lock (locker)
            {
                result += h * sum;
            }
        }

        /*private double Fnc(double x)
        {
            return x/(x-1);
        }*/











        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
