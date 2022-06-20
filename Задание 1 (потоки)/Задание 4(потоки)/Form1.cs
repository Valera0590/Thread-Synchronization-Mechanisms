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

namespace Задание_4_потоки_
{
    public partial class Form1 : Form
    {
        Graphics graph;
        Random rnd = new Random();
        SolidBrush br = new SolidBrush(Color.FromArgb(0, 0, 0));
        List<Thread> threads = new List<Thread>();
        List<ThreadPriority> prior = new List<ThreadPriority>();
        
        ThreadPriority pr1, pr2, pr3;
        public Form1()
        {
            InitializeComponent();
            prior.Add(ThreadPriority.Lowest);
            prior.Add(ThreadPriority.BelowNormal);
            prior.Add(ThreadPriority.Normal);
            button1.Enabled = false;
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "После нажатия круги будут нарисованы с новыми приоритетами для каждого цвета");
            List<string> Priority = new List<string>();
            Priority.Add("Lowest");
            Priority.Add("BelowNormal");
            Priority.Add("Normal");
            comboBox1.Text = "Выберите приоритет";
            comboBox2.Text = "Выберите приоритет";
            comboBox3.Text = "Выберите приоритет";
            for (int i = 0; i < Priority.Count; i++)
            {
                comboBox1.Items.Add(Priority[i]);
                comboBox2.Items.Add(Priority[i]);
                comboBox3.Items.Add(Priority[i]);
            }

            
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox1.SelectedItem) != "")
            {
                ParameterizedThreadStart paint = new ParameterizedThreadStart(Paint_Circle);
                threads.Add(new Thread(paint));
                threads[0].Priority = pr1;
                threads[0].Start('1');
                //threads[0].IsBackground = true;
                threads.Add(new Thread(paint));
                threads[1].Priority = pr2;
                threads[1].Start('2');
                //threads[1].IsBackground = true;
                threads.Add(new Thread(paint));
                threads[2].Priority = pr3;
                threads[2].Start('3');
                //threads[2].IsBackground = true;
                button1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics graph = this.CreateGraphics();
            if (threads.Count == 0) MessageBox.Show("Рисование кругов не было запущено!");
            else
            {
                for (int i = 0; i < 3; i++)
                    threads[i].Abort();
                MessageBox.Show("Рисование кругов закончено!");
                graph.Clear(Color.White);
                button1.Enabled = true;
                threads.Clear();

            }
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
        }

        private void Paint_Circle(object color)
        {
            Graphics graph;
            Random rnd = new Random();
            SolidBrush br = new SolidBrush(Color.FromArgb(0, 0, 0));

            int x = 450;  //координата Х
            int y = 400; //координата Y
            Pen pen_b = new Pen(Color.Black);
            graph = this.CreateGraphics();
            char c = (char)color;
            switch (c)
            {
                case '1': br.Color = Color.FromArgb(255, 0, 0); break;     //Красный  
                case '2': br.Color = Color.FromArgb(0, 255, 0); break;     //Зелёный
                case '3': br.Color = Color.FromArgb(0, 0, 255); break;     //Синий
                default: break;
            }
            while (true/*button1.Enabled==false*/)
            {
                Point pnt = new Point(rnd.Next(x - 100), rnd.Next(y - 100));     //размещение

                int r = (y - pnt.Y > x - pnt.X) ? rnd.Next(x - pnt.X) : rnd.Next(y - pnt.Y); // понял что тут подбирается размер, принцип не совсем понимаю
                if (r < 70) r += 70;        //если очень маленький радиус круга 

                Size sz = new Size(r, r);
                Rectangle rct = new Rectangle(pnt, sz);
                graph.DrawEllipse(pen_b, rct);
                graph.FillEllipse(br, rct);
            }
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Convert.ToString(comboBox1.SelectedItem) != "") && (Convert.ToString(comboBox2.SelectedItem) != "") && ((Convert.ToString(comboBox3.SelectedItem) != "")) )
                button1.Enabled = true;
            pr1 = prior[comboBox1.SelectedIndex];
            //textBox1.Text = Convert.ToString(pr1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Convert.ToString(comboBox1.SelectedItem) != "") && (Convert.ToString(comboBox2.SelectedItem) != "") && ((Convert.ToString(comboBox3.SelectedItem) != "")) )
                button1.Enabled = true;
            pr2 = prior[comboBox2.SelectedIndex];
            //textBox1.Text = Convert.ToString(pr2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Convert.ToString(comboBox1.SelectedItem) != "") && (Convert.ToString(comboBox2.SelectedItem) != "") && ((Convert.ToString(comboBox3.SelectedItem) != "")) )
                button1.Enabled = true;
            pr3 = prior[comboBox3.SelectedIndex];
            //textBox1.Text = Convert.ToString(pr3);
        }
    }


}
