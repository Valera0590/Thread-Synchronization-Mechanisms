using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_2._2_потоки_
{
    class Program
    {
        static string strok;
        static byte[] data;
        static byte check = 0;
        static int k;
        static List<Thread> threads;
        static object blocking = new object();
        static Random rnd = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку, для которой необоходимо найти контрольную сумму по модулю 256:");
            strok = Console.ReadLine();
            data = Encoding.ASCII.GetBytes(strok);
            k = rnd.Next(2,strok.Length - 1);
            int summa = 0;
            for (int i = 0; i < strok.Length; i++)
            {
                summa += data[i];
            }
            Console.WriteLine("Сумма без многопотока: "+summa+"\t  По модулю 256: "+summa%256+"\n");
            threads = new List<Thread>();
            ParameterizedThreadStart mod = new ParameterizedThreadStart(SumMod256);
            for (int i = 0; i < k; i++)
            {
                threads.Add(new Thread(mod));
                threads[i].Start(i);
            }
            while (IsRun())
                Thread.Sleep(20);
                   
            //Thread.Sleep(1000);
            Console.WriteLine("Контрольная суммма по данному тексту в виде суммы кодов символов по модулю 256 равна: " + Convert.ToString(check));
            Console.ReadKey();
        }

        static void SumMod256(object i)
        {
            int k1 = Convert.ToInt32(i);
            Thread.Sleep(20);
            lock(blocking)
            {
                for (int s = 0; (k1 + k*s) < strok.Length; s++)
                {
                    check += data[k1 + k * s];

                }   
            }
        }

        static bool IsRun()
        {
            for (int i = 0; i < k; i++)
            {
                if (threads[i].IsAlive) return true;
            }
            return false;
        }
    }
}
