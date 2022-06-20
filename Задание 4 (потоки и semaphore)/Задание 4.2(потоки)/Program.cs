using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_4._2_потоки_
{
    class Program
    {
        static Semaphore semOne = new Semaphore(1, 1);
        //static Mutex mutex = new Mutex();
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static int countSymbols;
        static int buferSize;
        static int n;
        static readonly Random rndGen = new Random();
        static List<Thread> threads;
        static List<char> bufer;
        static Thread thReader;
        static Thread thReaderUnc;



        static void Main(string[] args)
        {
            CreateFile();
            Console.Write("Введите размер буфера: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество потоков для шифрования: ");
            n = Convert.ToInt32(Console.ReadLine());
            bufer = new List<char>(m);
            buferSize = m;
            
            threads = new List<Thread>(n);
            
            thReader = new Thread(new ThreadStart(Reader));
            thReader.Start();
            
            Thread thWriter = new Thread(new ThreadStart(Writer));
            thWriter.Start();

            while (thWriter.IsAlive) Thread.Sleep(50);
            Thread.Sleep(100);
            Process.Start(desktop + "\\Source.txt");
            Process.Start(desktop + "\\Encrypted.txt");

            Console.WriteLine("\nПрограмма закончила шифрование.\nДля того, чтобы начать расшифровку нужно нажать любую клавишу на клавиатуре.");
            Console.ReadKey();

            bufer.Clear();
            thReaderUnc = new Thread(new ThreadStart(ReaderUnc));
            thReaderUnc.Start();

            Thread thUncoder = new Thread(new ThreadStart(Uncoder));
            thUncoder.Start();

            while (thUncoder.IsAlive) Thread.Sleep(50);
            Thread.Sleep(100);
            Process.Start(desktop + "\\Uncrypted.txt");

            Console.WriteLine("\nПрограмма закончила расшифровку.\nДля закрытия программы необходимо нажать любую клавишу на клавиатуре\nВнимание! После закрытия все файлы данной программы будут удалены.");
            Console.ReadKey();
            DeletingFiles();
        }

        static void Reader()
        {
            try
            {
                semOne.WaitOne();
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source.txt", FileMode.Open, FileAccess.Read));
                string str = SW.ReadToEnd();
                SW.Close();
                bool flag = false, frst = true;
                //Thread.Sleep(2);        //Синхронизация потоков заполнения пула и потока записи в файл из пула
                //Console.WriteLine("\nНачато считывание файла №{0} : {1} символов", Thread.CurrentThread.Name, str.Length);
                while (str.Length != 0)
                {
                    if ((str.Length != 0) && (bufer.Count < buferSize) && (!frst)) semOne.WaitOne(); else frst = false;
                    while ((str.Length != 0) && (bufer.Count < buferSize))
                    {
                        //mutex.WaitOne();
                        bufer.Add(str[0]);
                        //mutex.ReleaseMutex();
                        str = str.Remove(0, 1);
                        flag = true;
                        Thread.Sleep(5);
                    }
                    if (str.Length == 0)
                    {
                        Console.WriteLine("Файл считан полностью");
                        threads.Clear();
                        for (int i = 0; i < n; i++)
                        {
                            threads.Add(new Thread(new ParameterizedThreadStart(Crypt)));
                            threads[i].Name = Convert.ToString(i + 1);
                            threads[i].Start(i);
                        }
                        while (IsRun()) Thread.Sleep(10);
                        semOne.Release();
                    }
                    else
                    {
                        Console.WriteLine("Файл полностью не влез в буфер - {0}", bufer.Count);
                        if(flag)
                        {
                            flag = false;
                            threads.Clear();
                            for (int i = 0; i < n; i++)
                            {
                                threads.Add(new Thread(new ParameterizedThreadStart(Crypt)));
                                threads[i].Name = Convert.ToString(i + 1);
                                threads[i].Start(i);
                            }
                            while (IsRun()) Thread.Sleep(10);
                            semOne.Release();
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void Crypt(object num)
        {
            int numThread = (int)num;
            if (Thread.CurrentThread.Name == "1")
            {
                for (int i = 0; i < bufer.Count; i++)
                {
                    Console.Write(bufer[i] + " ");
                }
            Console.WriteLine();
            }
            Thread.Sleep(500);
            for (int i = Convert.ToInt32(Thread.CurrentThread.Name) - 1; i < bufer.Count; i+=n)
            {
                if (bufer[i] > 64 && bufer[i] < 90) { bufer[i]++; bufer[i] = char.ToLower(bufer[i]); }
                else if (bufer[i] > 96 && bufer[i] < 122) { bufer[i]++; bufer[i] = char.ToUpper(bufer[i]); }
                else if (bufer[i] == 90) bufer[i] = (char)97;
                else if (bufer[i] == 122) bufer[i] = (char)65;
            }
            if (Thread.CurrentThread.Name == "1")
            {
                for (int i = 0; i < bufer.Count; i++)
                {
                    Console.Write(bufer[i] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Writer()
        {
            StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Encrypted.txt", FileMode.Create, FileAccess.Write));
            while ((thReader.IsAlive) || (bufer.Count != 0))
            {
                //Console.WriteLine("Символов в пуле - {0}", bufer.Count);
                //Thread.Sleep(10);
                //while (!IsRun())
                //{
                    semOne.WaitOne();
                    int k = buferSize;
                    semOne.Release();
                    while((bufer.Count != 0) && (k > 0))
                    {
                        SW.Write(bufer[0]);
                        bufer.RemoveAt(0);
                        k--;
                    }
                    //mutex.ReleaseMutex();
                //}
                Console.WriteLine("Буфер пуст - Writer");
                Thread.Sleep(2);
            }
            SW.Close();
        }

        /*-------------------Расшифровка---------------------------------------------*/

        static void ReaderUnc()
        {
            try
            {
                semOne.WaitOne();
                StreamReader SW = new StreamReader(new FileStream(desktop + "\\Encrypted.txt", FileMode.Open, FileAccess.Read));
                string str = SW.ReadToEnd();
                SW.Close();
                bool flag = false, frst = true;
                //Thread.Sleep(2);        //Синхронизация потоков заполнения пула и потока записи в файл из пула
                //Console.WriteLine("\nНачато считывание файла №{0} : {1} символов", Thread.CurrentThread.Name, str.Length);
                while (str.Length != 0)
                {
                    if ((str.Length != 0) && (bufer.Count < buferSize) && (!frst)) semOne.WaitOne(); else frst = false;
                    while ((str.Length != 0) && (bufer.Count < buferSize))
                    {
                        //mutex.WaitOne();
                        bufer.Add(str[0]);
                        //mutex.ReleaseMutex();
                        str = str.Remove(0, 1);
                        flag = true;
                        Thread.Sleep(5);
                    }
                    if (str.Length == 0)
                    {
                        Console.WriteLine("Файл считан полностью");
                        threads.Clear();
                        for (int i = 0; i < n; i++)
                        {
                            threads.Add(new Thread(new ParameterizedThreadStart(UnCrypt)));
                            threads[i].Name = Convert.ToString(i + 1);
                            threads[i].Start(i);
                        }
                        while (IsRun()) Thread.Sleep(10);
                        semOne.Release();
                    }
                    else
                    {
                        Console.WriteLine("Файл полностью не влез в буфер - {0}", bufer.Count);
                        if (flag)
                        {
                            flag = false;
                            threads.Clear();
                            for (int i = 0; i < n; i++)
                            {
                                threads.Add(new Thread(new ParameterizedThreadStart(UnCrypt)));
                                threads[i].Name = Convert.ToString(i + 1);
                                threads[i].Start(i);
                            }
                            while (IsRun()) Thread.Sleep(10);
                            semOne.Release();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void UnCrypt(object num)
        {
            int numThread = (int)num;
            for (int i = Convert.ToInt32(Thread.CurrentThread.Name) - 1; i < bufer.Count; i += n)
            {
                if (bufer[i] > 65 && bufer[i] < 91) { bufer[i]--; bufer[i] = char.ToLower(bufer[i]); }
                else if (bufer[i] > 97 && bufer[i] < 123) { bufer[i]--; bufer[i] = char.ToUpper(bufer[i]); }
                else if (bufer[i] == 65) bufer[i] = (char)122;
                else if (bufer[i] == 97) bufer[i] = (char)90;
            }
        }
        static void Uncoder()
        {
            StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Uncrypted.txt", FileMode.Create, FileAccess.Write));
            while ((thReaderUnc.IsAlive) || (bufer.Count != 0))
            {
                //Console.WriteLine("Символов в пуле - {0}", bufer.Count);
                //Thread.Sleep(10);
                //while (!IsRun())
                //{
                semOne.WaitOne();
                int k = buferSize;
                semOne.Release();
                while ((bufer.Count != 0) && (k > 0))
                {
                    SW.Write(bufer[0]);
                    bufer.RemoveAt(0);
                    k--;
                }
                //mutex.ReleaseMutex();
                //}
                Console.WriteLine("Буфер пуст - Writer");
                Thread.Sleep(2);
            }
            SW.Close();
        }

        static bool IsRun()
        {
            for (int i = 0; i < n; i++)
            {
                if (threads[i].IsAlive) return true;
            }
            return false;
        }



        /*------------------ Создание и удаление файлов ----------------------------------------------*/
        static void CreateFile()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            const string dictionary = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            countSymbols = rnd.Next(15, 30);
   
            StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Source.txt", FileMode.Create, FileAccess.Write));
            SW.Write(GetRandomPassword(dictionary, countSymbols));
            SW.Close();
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
            if (File.Exists(desktop + "\\Source.txt"))
                File.Delete(desktop + "\\Source.txt");
            
            if (File.Exists(desktop + "\\Encrypted.txt"))
                File.Delete(desktop + "\\Encrypted.txt");

            if (File.Exists(desktop + "\\Uncrypted.txt"))
                File.Delete(desktop + "\\Uncrypted.txt");
        }
    }
}
