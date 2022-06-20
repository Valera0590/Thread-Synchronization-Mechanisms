using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_4._1_потоки_
{
    class Program
    {
        static Semaphore semOne = new Semaphore(1, 1);
        static Mutex mutex = new Mutex();
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static int countFiles;
        static int poolSize;
        static readonly Random rndGen = new Random();
        static List<Thread> threads;
        static List<char> printPool;

        static void Main(string[] args)
        {
            CreateFiles();
            Console.Write("Введите размер пула n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            printPool = new List<char>(n);
            poolSize = n;
            int m = countFiles;
            threads = new List<Thread>(m);
            for (int i = 0; i < m; i++)
            {
                threads.Add(new Thread(new ThreadStart(Reader)));
                threads[i].Name = Convert.ToString(i + 1);
                threads[i].Start();
            }
            Thread wrote = new Thread(new ThreadStart(Writer));
            wrote.Start();

            while (wrote.IsAlive) Thread.Sleep(50);
            Thread.Sleep(100);
            for (int i = 0; i < countFiles; i++)
            {
                Process.Start(desktop + "\\Source" + Convert.ToString(i + 1) + ".txt");
            }
            Process.Start(desktop + "\\Mergin.txt");

            Console.WriteLine("\nПрограмма закончила обработку пула печати.\nДля закрытия окна програмы необходимо нажать любую клавишу на клавиатуре\nВнимание! После закрытия все файлы данной программы будут удалены.");
            Console.ReadKey();
            DeletingFiles();
        }

        static void Reader()
        {
            try
            {
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source" + Thread.CurrentThread.Name + ".txt", FileMode.Open, FileAccess.Read));
                string str = SW.ReadToEnd();
                SW.Close();

                semOne.WaitOne();
                Thread.Sleep(2);        //Синхронизация потоков заполнения пула и потока записи в файл из пула
                Console.WriteLine("\nНачато считывание файла №{0} : {1} символов", Thread.CurrentThread.Name, str.Length);
                while (str.Length != 0)
                {
                    while((str.Length != 0) && (printPool.Count < poolSize))
                    {
                        mutex.WaitOne();
                        printPool.Add(str[0]);
                        mutex.ReleaseMutex();
                        str = str.Remove(0,1);
                        //Thread.Sleep(1);
                    }
                    if (str.Length == 0) { Console.WriteLine("Файл №{0} считан полностью. Оставшихся символов в пуле - {1}", Thread.CurrentThread.Name, printPool.Count); semOne.Release(); }
                    else { Console.WriteLine("Файл полностью не влез в пул - {0}", printPool.Count); Thread.Sleep(10); }
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void Writer()
        {
            StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Mergin.txt", FileMode.Create, FileAccess.Write));
            while ((printPool.Count != 0) || IsRun())
            {
                //Console.WriteLine("Символов в пуле - {0}", printPool.Count);
                while (printPool.Count != 0)
                {
                    mutex.WaitOne();
                    SW.Write(printPool[0]);
                    printPool.RemoveAt(0);
                    mutex.ReleaseMutex();
                }
                Console.WriteLine("Пул пуст - Writer");
                Thread.Sleep(2);
            }
            SW.Close();
        }

        static bool IsRun()
        {
            for (int i = 0; i < countFiles; i++)
            {
                if (threads[i].IsAlive) return true;
            }
            return false;
        }



        /*------------------ Создание и удаление файлов ----------------------------------------------*/
        static void CreateFiles()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            const string dictionary = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789-_+=*&~^%$@(){}[]";
            countFiles = rnd.Next(3, 8);
            for (int i = 0; i < countFiles; i++)
            {
                StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Source"+Convert.ToString(i+1)+".txt", FileMode.Create, FileAccess.Write));
                SW.Write(GetRandomPassword(dictionary, (2 * i) % 3 + 2 * (i % 2 + 1)));
                SW.Close();
            }
        }
        static string GetRandomPassword(string ch, int pwdLength)
        {
            char[] pwd = new char[pwdLength];
            for (int i = 0; i < pwd.Length; i++)
                pwd[i] = ch[rndGen.Next(ch.Length)];
            return new string(pwd);
        }
        static void DeletingFiles()
        {
            for (int i = 0; i < countFiles; i++)
            {
                if (File.Exists(desktop + "\\Source" + Convert.ToString(i + 1) + ".txt"))
                    File.Delete(desktop + "\\Source" + Convert.ToString(i + 1) + ".txt");
            }
            if (File.Exists(desktop + "\\Mergin.txt"))
                File.Delete(desktop + "\\Mergin.txt");
        }
    }
}
