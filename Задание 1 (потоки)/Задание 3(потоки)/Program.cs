using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_3_потоки_
{
    class Program
    {
        static int m, n;
        static int[,] mtx;
        static int mnozh;

        static void Main(string[] args)
        {
            Console.Write("Введите кол-во строк и столбцов через пробел  ");
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

            m = Convert.ToInt32(m_n1[0]);
            n = Convert.ToInt32(m_n1[1]);
            mtx = new int[m, n];
            Console.Write("Введите множитель для умножения всей матрицы на это число:  ");
            mnozh = Convert.ToInt32(Console.ReadLine());

            ParameterizedThreadStart create = new ParameterizedThreadStart(Create_matrix);
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < m; i++)
            {
                threads.Add(new Thread(create));
                threads[i].Start(i);
            }
            for (int i = 0; i < m; i++)
                threads[i].Join();

            threads.Clear();
            Console.WriteLine("Матрица до умножения");
            Output();

            ParameterizedThreadStart mull = new ParameterizedThreadStart(Mull_matrix);
            for (int i = 0; i < m; i++)
            {
                threads.Add(new Thread(mull));
                threads[i].Start(i);
            }
            for (int i = 0; i < m; i++)
                threads[i].Join();
            threads.Clear();


            Console.WriteLine();
            Console.WriteLine("Матрица после умножения на " + mnozh);
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

        static void Mull_matrix(object i)
        {
            for (int j = 0; j < n; j++)
                mtx[Convert.ToInt32(i), j] *= mnozh;
        }

       

    }
}
