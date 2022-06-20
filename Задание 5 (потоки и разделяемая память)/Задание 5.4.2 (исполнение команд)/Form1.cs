using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание_5._4._2__исполнение_команд_
{
    delegate void textBoxTextDelegate(string text);
    delegate void arrayDelRepeatDelegate();
    delegate void mostFrequentWordsDelegate();
    //delegate void minAndMaxMatrixDelegate(int n);
    delegate bool threadIsAlive();
    public partial class Form1 : Form
    {
        MemoryMappedFile sharedMemory;
        MemoryMappedFile sharedMemory1;
        MemoryMappedFile sharedMemory2;
        bool[] actions = { false, false, false, false, false, false };
        bool close = false;
        Thread th;
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //string MinMax;
        public Form1()
        {
            InitializeComponent();
            sharedMemory2 = MemoryMappedFile.CreateOrOpen("MemoryClosing2", 2);
            th = new Thread(() => ListenClose());
            th.Start();
            Thread thReader = new Thread(() => ListenRead());
            thReader.Start();
            thReader.IsBackground = true;

        }
        //private void TextBoxText(string text)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new textBoxTextDelegate(TextBoxText), new object[] { text });

        //        tbFirstNum.Enabled = true;
        //        return;
        //    }
        //    else tbFirstNum.Text = text;
        //}
        private bool ThreadIsAlive()
        {
            bool isAlive = th.IsAlive;
            if (InvokeRequired)
            {
                BeginInvoke(new threadIsAlive(ThreadIsAlive));
                return isAlive;
            }
            else return isAlive;
        }
        private void ArrayDelRepeatDelegate()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new arrayDelRepeatDelegate(ArrayDelRepeatDelegate));
                return;
            }
            else
            {
                List<double> list = new List<double>();
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\SourceArrayD.txt", FileMode.Open, FileAccess.Read));
                StreamWriter sw = new StreamWriter(new FileStream(desktop + "\\SourceArray.txt", FileMode.OpenOrCreate, FileAccess.Write));
                list = SW.ReadToEnd().Split(' ').Select(Convert.ToDouble).ToList();
                string strWithoutDupl = string.Join("  ", list.Distinct());
                rtbArray.Text = strWithoutDupl;
                sw.WriteLine(strWithoutDupl);
                SW.Close();
                sw.Close();
            }
        }

        private void MostFrequentWordsDelegate()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new mostFrequentWordsDelegate(MostFrequentWordsDelegate));
                return;
            }
            else
            {
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\SourceWords.txt", FileMode.Open, FileAccess.Read));
                List<string> list = new List<string>();
                list = SW.ReadToEnd().Split(' ').ToList();
                int mostNum = list.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                 .Select(grp => grp.Count()).First();
                IEnumerable<string> mostWords = list.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                    .Where(grp => grp.Count() >= mostNum)
                    .Select(grp => grp.Key);

                rtbWords.Text = string.Join(" ", mostWords);
                tbNumWords.Text = mostNum.ToString();
                SW.Close();
            }
        }
        private void MinAndMaxMatrix(int n)
        {
            int[,] MyMatrix = GenerateMatrix(n);
            tbMaxMatrix.Text = "";
            tbMinMatrix.Text = "";
            if (n == 1)
            {
                tbMinMatrix.Text = "Не возможно вычислить";
                tbMaxMatrix.Text = "Не возможно вычислить";
            }
            else
            {
                tbMinMatrix.Text = MinMyMatrix(MyMatrix).ToString();
                tbMaxMatrix.Text = MaxMyMatrix(MyMatrix).ToString();
            }

        }
        private void PrimeNumbers(int n)
        {
            rtbSieveEratosthenes.Text = "";
            rtbSieveEratosthenes.Text = string.Join(" ", SieveEratosthenes(n));
        }

        private int[,] GenerateMatrix(int n)
        {
            rtbMatrix.Text = "";
            Random rnd = new Random(DateTime.Now.Millisecond);
            int[,] MyMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MyMatrix[i, j] = rnd.Next(-100, 100);
                    rtbMatrix.Text += MyMatrix[i, j] + " ";
                }
                rtbMatrix.Text += "\n";
            }
            return MyMatrix;
        }

        private int MinMyMatrix(int[,] MyMatrix)
        {
            int min = int.MaxValue;
            for (int i = 0; i < MyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (MyMatrix[i, j] < min) min = MyMatrix[i, j];
                }
            }
            return min;
        }
        private int MaxMyMatrix(int[,] MyMatrix)
        {
            int max = int.MinValue;
            for (int i = 0; i < MyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < MyMatrix.GetLength(0) - i - 1; j++)
                {
                    if (MyMatrix[i, j] > max) max = MyMatrix[i, j];
                }
            }
            return max;
        }

        private static List<int> SieveEratosthenes(int n)
        {
            var numbers = new List<int>();
            for (var i = 2; i < n; i++)
            {
                numbers.Add(i);
            }
            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2; j < n; j++)
                {
                    numbers.Remove(numbers[i] * j);
                }
            }
            return numbers;
        }

        void ListenRead()
        {
            MemoryMappedFile sharedMemoryRewrite;
            Thread.Sleep(1000);
            Exception err;
            try
            {
                err = null;
                //Получение существующего участка разделяемой памяти
                //Параметр - название участка
                sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
            }
            catch (Exception e)
            {
                err = e;
            };

            while (err != null)
            {
                try
                {
                    err = null;
                    //Получение существующего участка разделяемой памяти
                    //Параметр - название участка
                    sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
                }
                catch (Exception e)
                {
                    err = e;
                };
                Thread.Sleep(100);      // Снижаем кол-во опросов разделяемой памяти
            }
            sharedMemoryRewrite = MemoryMappedFile.CreateOrOpen("MemoryFile", 2 * actions.Length);
            while (ThreadIsAlive())     // Пока форма не закрыта
            {
                using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 2 * actions.Length, MemoryMappedFileAccess.Read))
                {
                    //Массив символов сообщения
                    reader.ReadArray<bool>(0, actions, 0, actions.Length);
                }
                //Если с клиента поступили команды
                if (actions[0])
                {
                    actions[0] = false;
                    sharedMemoryRewrite = MemoryMappedFile.CreateOrOpen("MemoryFile", 2 * actions.Length);
                    using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, 2 * actions.Length))
                    {
                        writer.WriteArray<bool>(0, actions, 0, actions.Length);
                    }

                    int f;
                    if (actions[1])
                    {
                        tbFirstNum.Invoke(new Action(() =>
                        {
                            tbFirstNum.Enabled = true;
                            tbSecondNum.Enabled = true;
                            tbResultMull.Enabled = true;
                        }));
                    }
                    else
                    {
                        tbFirstNum.Invoke(new Action(() =>
                        {
                            tbFirstNum.Enabled = false;
                            tbFirstNum.Text = "";
                            tbSecondNum.Enabled = false;
                            tbSecondNum.Text = "";
                            tbResultMull.Enabled = false;
                        }));
                    }
                    if (actions[2])
                    {
                        rtbArray.Invoke(new Action(() =>
                        {
                            rtbArray.Enabled = true;
                            rtbArray.Text = "";
                        }));
                        if (File.Exists(desktop + "\\SourceArray.txt"))
                            File.Delete(desktop + "\\SourceArray.txt");
                        Thread th = new Thread(() => ArrayDelRepeatDelegate());
                        th.Start();
                    }
                    else
                    {
                        rtbArray.Invoke(new Action(() =>
                        {
                            rtbArray.Enabled = false;
                            rtbArray.Text = "";
                        }));
                        if (File.Exists(desktop + "\\SourceArray.txt"))
                            File.Delete(desktop + "\\SourceArray.txt");
                    }
                    if (actions[3])
                    {
                        tbNumWords.Invoke(new Action(() =>
                        {
                            rtbWords.Enabled = true;
                            rtbWords.Text = "";
                            tbNumWords.Enabled = true;
                            tbNumWords.Text = "";
                        }));
                        Thread th1 = new Thread(() => MostFrequentWordsDelegate());
                        th1.Start();
                    }
                    else
                    {
                        rtbArray.Invoke(new Action(() =>
                        {
                            rtbWords.Enabled = false;
                            rtbWords.Text = "";
                            tbNumWords.Enabled = false;
                            tbNumWords.Text = "";
                        }));
                    }
                    if (actions[4])
                    {
                        tbNMatrix.Invoke(new Action(() =>
                        {
                            tbNMatrix.Enabled = true;
                            tbNMatrix.Text = "";
                        }));
                    }
                    else
                    {
                        tbNMatrix.Invoke(new Action(() =>
                        {
                            tbNMatrix.Enabled = false;
                            tbNMatrix.Text = "";
                        }));
                    }
                    if (actions[5])
                    {
                        tbNSieveEratosthenes.Invoke(new Action(() =>
                        {
                            tbNSieveEratosthenes.Enabled = true;
                            tbNSieveEratosthenes.Text = "";
                        }));
                    }
                    else
                    {
                        tbNMatrix.Invoke(new Action(() =>
                        {
                            tbNSieveEratosthenes.Enabled = false;
                            tbNSieveEratosthenes.Text = "";
                        }));
                    }
                    //TextBoxText(Convert.ToString(actions[1]) + Convert.ToString(actions[2]) + Convert.ToString(actions[3]) + Convert.ToString(actions[4]) + Convert.ToString(actions[5]));

                }
                Thread.Sleep(300);      // Снижаем кол-во обращений к разделяемой памяти
            }
            if (File.Exists(desktop + "\\SourceArray.txt"))
                File.Delete(desktop + "\\SourceArray.txt");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((tbFirstNum.Text != "") && (tbSecondNum.Text != ""))
            {
                double a = Convert.ToDouble(tbFirstNum.Text);
                double b = Convert.ToDouble(tbSecondNum.Text);
                double rslt = a * b;
                tbResultMull.Text = Convert.ToString(rslt);
            }
            else if (tbFirstNum.Text == "") tbResultMull.Text = "";
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((tbFirstNum.Text != "") && (tbSecondNum.Text != ""))
            {
                double a = Convert.ToDouble(tbFirstNum.Text);
                double b = Convert.ToDouble(tbSecondNum.Text);
                double rslt = a * b;
                tbResultMull.Text = Convert.ToString(rslt);
            }
            else if (tbSecondNum.Text == "") tbResultMull.Text = "";
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
            // ввод в texBox только цифр и кнопки Backspace и запятая
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 44 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void tbNMatrix_TextChanged(object sender, EventArgs e)
        {
            if (tbNMatrix.Text != "")
            {
                rtbMatrix.Enabled = true;
                tbMaxMatrix.Enabled = true;
                tbMinMatrix.Enabled = true;
                tbNMatrix.Enabled = false;
                tbNMatrix.ReadOnly = true;
                MinAndMaxMatrix(Convert.ToInt32(tbNMatrix.Text));
                tbNMatrix.ReadOnly = false;
                tbNMatrix.Enabled = true;
            }
            else
            {
                rtbMatrix.Text = "";
                tbMaxMatrix.Text = "";
                tbMinMatrix.Text = "";
                rtbMatrix.Enabled = false;
                tbMaxMatrix.Enabled = false;
                tbMinMatrix.Enabled = false;
            }
        }
        private void tbNMatrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр, знака минус и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 45)
            {
                e.Handled = true;
            }
        }
        private void tbNSieveEratosthenes_TextChanged(object sender, EventArgs e)
        {
            if (tbNSieveEratosthenes.Text != "")
            {
                rtbSieveEratosthenes.Enabled = true;
                tbNSieveEratosthenes.Enabled = false;
                tbNSieveEratosthenes.ReadOnly = true;
                PrimeNumbers(Convert.ToInt32(tbNSieveEratosthenes.Text));
                tbNSieveEratosthenes.ReadOnly = false;
                tbNSieveEratosthenes.Enabled = true;
            }
            else
            {
                rtbSieveEratosthenes.Text = "";
                rtbSieveEratosthenes.Enabled = false;
            }
        }
        private void tbNSieveEratosthenes_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр, знака минус и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 45)
            {
                e.Handled = true;
            }
        }
        void ListenClose()         // Слушатель события закрытия формы 1
        {
            Thread.Sleep(100);
            Exception err;
            try
            {
                err = null;
                //Получение существующего участка разделяемой памяти
                //Параметр - название участка
                sharedMemory1 = MemoryMappedFile.OpenExisting("MemoryClosing1");
            }
            catch (Exception e)
            {
                err = e;
            };

            while (err != null)
            {
                try
                {
                    err = null;
                    //Получение существующего участка разделяемой памяти
                    //Параметр - название участка
                    sharedMemory1 = MemoryMappedFile.OpenExisting("MemoryClosing1");
                }
                catch (Exception e)
                {
                    err = e;
                };
                Thread.Sleep(100);      // Снижаем кол-во опросов разделяемой памяти
            }
            //sharedMemory1 = MemoryMappedFile.OpenExisting("MemoryClosing1");
            bool closing = false;
            while (closing == false)
            {
                using (MemoryMappedViewAccessor reader = sharedMemory1.CreateViewAccessor(0, 2, MemoryMappedFileAccess.Read))
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
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            close = true;
            //Создание участка разделяемой памяти
            //Первый параметр - название участка, 
            //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
            //плюс четыре байта для одного объекта типа Integer
            sharedMemory2 = MemoryMappedFile.CreateOrOpen("MemoryClosing2", 2);
            //Создаем объект для записи в разделяемый участок памяти
            using (MemoryMappedViewAccessor writer = sharedMemory2.CreateViewAccessor(0, 2))
            {
                //запись в разделяемую память
                //запись сообщения с первого байта в разделяемой памяти
                writer.Write(0, close);
            }
            if (File.Exists(desktop + "\\SourceArray.txt"))
                File.Delete(desktop + "\\SourceArray.txt");
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

    }
}
