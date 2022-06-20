using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_1_потоки_
{
    class Program
    {
        const int m = 3, n = 3;
        static float[,] mtx = new float[m, n];
        static float [] col_sums = new float[n];     // массив сумм
         
        static void Main(string[] args)
        {
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
            // Заполнение массивов исходными значениями
            for (int i = 0; i < n; i++)
                col_sums[i] = 0;

            ParameterizedThreadStart col_summa = new ParameterizedThreadStart(Col_summa);
            for (int i = 0; i < n; i++)
            {
                threads.Add(new Thread(col_summa));
                threads[i].Start(i);
            }
            for (int i = 0; i < m; i++)
                threads[i].Join();
            //Thread.Sleep(2000);

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
            for (int i = 0; i < n; i++)
                Console.Write(col_sums[i] + " ");

            Console.WriteLine();

            // Находим номер столбца с минимальной суммой
            int num_min = 0;
            float min = col_sums[0];

            for (int i = 1; i < n; i++)
            {
                if (min > col_sums[i])
                {
                    min = col_sums[i];
                    num_min = i;
                }
            }
            num_min++;
            // Вывод результата
            Console.WriteLine("Искомый столбец № " + num_min);
        }

        static void Create_matrix(object i)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int j = 0; j < n; j++)
                mtx[Convert.ToInt32(i), j] = rnd.Next(0, 9);
        }
        
        static void Col_summa(object col_num)
        {
            // Находим искомую сумму
            col_sums[Convert.ToInt32(col_num)] = 0;
            for (int i = 0; i < m; i++)
                col_sums[Convert.ToInt32(col_num)] += mtx[i,Convert.ToInt32(col_num)];

        }

    }
}
