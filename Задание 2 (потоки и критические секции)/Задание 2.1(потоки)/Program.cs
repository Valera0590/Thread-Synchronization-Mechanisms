using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_2._1_потоки_
{
    class Program
    {
        static int n, m;
        static double[,] mtx;
        static double sum=0;
        static double[] s;
        static object[] blocking;
        static object locker = new object();
        static Random rnd = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args)
        {
            Console.Write("Введите кол-во строк и столбцов через пробел:  ");
            string m_n = Console.ReadLine();
            while (m_n.First() == ' ')
                m_n = m_n.Remove(0, 1);

            string[] m_n1 = new string[2];
            while (m_n.First() != ' ')
            {
                m_n1[0] += m_n.First();
                m_n = m_n.Remove(0, 1);
            }

            while (m_n.First() == ' ')
                m_n = m_n.Remove(0, 1);

            while (m_n.First() != ' ')
            {
                m_n1[1] += m_n.First();
                m_n = m_n.Remove(0, 1);
                if (m_n == "") m_n += ' ';
            }

            n = Convert.ToInt32(m_n1[0]);
            m = Convert.ToInt32(m_n1[1]);
            mtx = new double[n, m];
            s = new double[n];
            for (int i = 0; i < n; i++)
                s[i] = 0;
            blocking = new object[n];
            for (int i = 0; i < n; i++)
                blocking[i] = new object();
            Console.Write("   Сумма на каждой итерации:");
            for (int i = 0; i < n; i++)
            {
                new Thread(new ParameterizedThreadStart(Create_matrix)).Start(i);
                new Thread(new ParameterizedThreadStart(Str_summa)).Start(i);
            }
            //List<Thread> threads = new List<Thread>();
            //ParameterizedThreadStart create_m = new ParameterizedThreadStart(Create_matrix);
            //ParameterizedThreadStart str_s = new ParameterizedThreadStart(Str_summa);
            //for (int i = 0, j=0; i < 2*n; i+=2, j++)
            //{
            //    threads.Add(new Thread(create_m));
            //    threads.Add(new Thread(str_s));
            //    threads[i].Start(j);
            //    threads[i + 1].Start(j);
            //    //Thread.Sleep(5);
            //}
            //for (int i = 0; i < 2*n; i++)
            //{
            //    threads[i].Join();
            //}
            Thread.Sleep(1000);     //Приостанавливаем основной поток для повышения надежности синхронизации
            Console.WriteLine();
            Output();

            Console.Write("   Итоговая сумма: " + Math.Round(sum, 2));
            Console.ReadKey();
        }

        static void Output()
        {
            double itog = 0;
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < m; j++)
                    Console.Write(" " + mtx[i, j]);

                itog += s[i];
                Console.WriteLine("\t  " + s[i] + "    " + itog);
            }
            Console.WriteLine();
        }

        static void Create_matrix(object i)
        {
            //Thread.Sleep(20);
            lock (blocking[(int)i])
            {
                for (int j = 0; j < m; j++)
                {
                    mtx[Convert.ToInt32(i), j] = Math.Round(rnd.NextDouble()*100 , 2);
                    s[Convert.ToInt32(i)] += mtx[Convert.ToInt32(i), j];
                }
            }
        }

        static void Str_summa(object str_num)
        {
            Thread.Sleep(50);
            double str_s = 0;
            // Находим искомую сумму в строке
            lock (blocking[(int)str_num])
            {
                for (int i = 0; i < m; i++)
                    str_s += mtx[Convert.ToInt32(str_num), i];
                lock(locker)
                {
                    sum += str_s;
                    Console.Write(" " + Math.Round(sum, 2) );
                }
                //Thread.Sleep(500);
            }
        }

    }
}
