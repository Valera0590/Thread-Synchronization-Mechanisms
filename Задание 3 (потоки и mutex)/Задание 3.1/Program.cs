using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_3._1
{
    class Program
    {
        static string[] str1;
        static string[] str2;
        //static double[] result;
        static double rslt = 0;
        static int n;
        static List<Thread> th;
        static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            Console.Write("Введите размер векторов: ");
            n = Convert.ToInt32(Console.ReadLine());
            //result = new double[n];
            th = new List<Thread>(n);
            Console.Write("\nВведите элементы первого вектора через пробел: ");
            str1 = Console.ReadLine().Split(' ');
            Console.Write("\nВведите элементы второго вектора через пробел: ");
            str2 = Console.ReadLine().Split(' ');
            if ((str1.Length == str2.Length) && (str1.Length == n))
            {
                for (int i = 0; i < n; i++)
                {
                    th.Add(new Thread(new ParameterizedThreadStart(ScalarMull)));
                    th[i].Name = Convert.ToString(i + 1);
                    th[i].Start(i);
                }
                while (IsRun())
                    Thread.Sleep(50);
                Console.WriteLine("Скалярное произведение введённых векторов равно: {0}", rslt);
            }
            else Console.WriteLine("Некорректно введены векторы!");
            Console.ReadKey();
        }
        static void ScalarMull(object i)
        {
            int j = (int)i;
            //result[j] = Convert.ToDouble(str1[j]) * Convert.ToDouble(str2[j]);
            Console.WriteLine(Thread.CurrentThread.Name + " ожидает мьютекс");
            mutex.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " получает мьютекс");
            //Thread.Sleep(100);
            rslt += Convert.ToDouble(str1[j]) * Convert.ToDouble(str2[j])/*result[j]*/;
            Console.WriteLine(Thread.CurrentThread.Name + " закончил работу");
            mutex.ReleaseMutex();
        }
        static bool IsRun()
        {
            for (int i = 0; i < n; i++)
            {
                if (th[i].IsAlive) return true;
            }
            return false;
        }
    }
}
