using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_2_потоки_
{
    class Program
    {
        static int m, n;
        static int[,] mtx;
        //static float[] col_sums = new float[n];     // массив сумм

        static void Main(string[] args)
        {
            ParameterizedThreadStart create = new ParameterizedThreadStart(Create_matrix);
            Console.Write("Введите кол-во строк и столбцов через пробел  ");
            string m_n=Console.ReadLine();
            while (m_n.First()==' ')
                m_n = m_n.Remove(0, 1);

            string[] m_n1=new string[2];
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

            m = Convert.ToInt32(m_n1[0]);
            n = Convert.ToInt32(m_n1[1]);
            mtx = new int[m, n];
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < m; i++)
            {
                threads.Add(new Thread(create));
                threads[i].Start(i);
            }
            for (int i = 0; i < m; i++)
                threads[i].Join();

            threads.Clear();
            Console.WriteLine("Матрица до сортировки:");
            Output();

            ParameterizedThreadStart sort = new ParameterizedThreadStart(Counting_sort);
            for (int i = 0; i < m; i++)
            {
                threads.Add(new Thread(sort));
                threads[i].Start(i);
            }
            for (int i = 0; i < m; i++)
                threads[i].Join();
            threads.Clear();

            
            Console.WriteLine();
            Console.WriteLine("Матрица после сортировки:");
            Output();
            Console.ReadKey();
        }

        static void Output()
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(mtx[i, j] + " ");

                Console.WriteLine();
            }    
        }

        static void Create_matrix(object i)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int j = 0; j < n; j++)
                mtx[Convert.ToInt32(i), j] = rnd.Next(0, 9);
        }

        private static void Counting_sort(object str)
        {
            int stroka = Convert.ToInt32(str);
            int z = 0, min = Min_str(stroka), max = Max_str(stroka);
            int[] count = new int[max - min + 1];

            for (int i = 0; i < count.Length; i++)
            {
                count[i] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                count[mtx[stroka,i] - min]++;
            }

            for (int i = max; i >= min; i--)
            {
                while (count[i - min]-- > 0)
                {
                    mtx[stroka,z] = i;
                    z++;
                }
            }
        }

        static int Max_str(int stroke)
        {
            int max = mtx[stroke, 0];
            for (int i = 1; i < n; i++)
            {
                if(max < mtx[stroke,i])
                    max = mtx[stroke, i];
            }
            return max;
        }

        static int Min_str(int stroke)
        {
            int min = mtx[stroke, 0];
            for (int i = 1; i < n; i++)
            {
                if (min > mtx[stroke, i])
                    min = mtx[stroke, i];
            }
            return min;
        }

    }
}
