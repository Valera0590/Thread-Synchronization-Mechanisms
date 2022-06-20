using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_3._3_потоки_
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m, k;
            do
            {
                Console.Write("Введите кол-во потоков, строк хеш-таблицы и основание хеш-функции через пробел: ");
                string[] str = Console.ReadLine().Split(' ');
                n = Convert.ToInt32(str[0]);
                m = Convert.ToInt32(str[1]);
                k = Convert.ToInt32(str[2]);
                if (m < k) Console.WriteLine("Проверьте, чтобы кол-во строк хеш-таблицы было не меньше основания хеш-функции!\n");
            } while (m<k);
            Monitor HTab = new Monitor(n, k, m);
            HTab.Create_hash_table();
            Console.WriteLine("Пожалуйста подождите, происходит распределение чисел по хеш-таблице");
            while(HTab.IsRun())
                Thread.Sleep(20);
            HTab.Show();
            Console.ReadKey();
        }
    }

    class Monitor
    {
        int val;
        int n;
        int k;
        int m;
        List<int>[] table;
        List<Mutex> mutixes = new List<Mutex>();
        List<Thread> threads = new List<Thread>();


        public Monitor(int n, int k, int m)
        {
            this.n = n;
            this.k = k;
            this.m = m;
            table = new List<int>[m];
            for (int i = 0; i < m; i++)
            {
                table[i] = new List<int>();
            }
        }
        public void Create_hash_table()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            val = rnd.Next(5,100);
            Console.WriteLine("Кол-во распределяемых значений: {0}",val);
            for (int i = 0; i < m; i++)
                mutixes.Add(new Mutex());

            for (int j = 0; j < n ;j++)
            {
                threads.Add(new Thread(new ThreadStart(Fill_data)));
                threads[j].Name = Convert.ToString(j);
                threads[j].Start();
            }
        }
        void Fill_data()
        {
            //int m = (int)m1;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int hash, temp;
            for (int i = 0; i < val/n; i++)
            {
                temp = rnd.Next(0, 30);
                hash = Hash(temp);
                mutixes[hash].WaitOne();
                
                if (table[hash] == null || !table[hash].Contains(temp))
                {
                    table[hash].Add(temp);
                }
                Console.WriteLine("({0}) {1}",Thread.CurrentThread.Name,temp);
                mutixes[hash].ReleaseMutex();
                
            }

            if(val/n*n + Convert.ToInt32(Thread.CurrentThread.Name) < val)
            {
                temp = rnd.Next(0, 10);
                hash = Hash(temp);
                mutixes[hash].WaitOne();
                
                if (table[hash] == null || !table[hash].Contains(temp))
                {
                    table[hash].Add(temp);
                }
                Console.WriteLine("({0}) {1}", Thread.CurrentThread.Name, temp);
                mutixes[hash].ReleaseMutex();
                
            }
        }
        int Hash(int x)
        {
            return x % k;
        }
        public void Show()
        {
            for (int i = 0; i < m; i++)
            {
                foreach(int a in table[i])
                    Console.Write(a + "\t");

                    //for (int j = 0; j < table[i].Count; j++)
                    //{
                    //    Console.Write(table[i][j] + " ");
                    //}
                    Console.WriteLine();
            }
        }
        public bool IsRun()
        {
            for (int i = 0; i < n; i++)
            {
                if (threads[i].IsAlive) return true;
            }
            return false;
        }
    }
}
