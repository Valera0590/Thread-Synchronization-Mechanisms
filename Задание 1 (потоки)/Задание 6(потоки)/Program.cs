using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_6_потоки_
{
    class Program
    {
        static string[] blocks;
        static string text;
        static string key;
        static void Main(string[] args)
        {
            Console.Write("Введите текст для шифрования:  ");
            text = Console.ReadLine();
            Console.Write("Введите ключ для шифрования:  ");
            key = Console.ReadLine();
            //text = text.ToLower();
            //key = key.ToLower();
            blocks = new string[(int) Math.Ceiling((decimal) text.Length/key.Length)];

            Crypted();
            Console.Write("\nХотите ли вы расшифровать полученный текст? \nВведите да(yes): ");
            string answer = Console.ReadLine();
            answer.ToLower();
            if (answer == "да" || answer == "yes")
            {
                Uncrypted();
            }
            Console.ReadKey();
        }

        static void Crypted()
        {
            Console.WriteLine("Кол-во блоков: " + blocks.Length);
            int i = 0;
            List<Thread> threads = new List<Thread>();
            ParameterizedThreadStart shifr = new ParameterizedThreadStart(Crypt);
            while (text != "")
            {
                if (text.Length < key.Length)
                {
                    int temp = key.Length;
                    while (text.Length != temp)
                        temp--;
                    blocks[i] = text.Substring(0, temp);
                    text = "";
                    threads.Add(new Thread(shifr));
                    threads[i].Start(i);
                    //threads[i].Join();
                    //Thread.Sleep(1000);
                }
                else
                {
                    blocks[i] = text.Substring(0, key.Length);
                    text = text.Remove(0, key.Length);
                    threads.Add(new Thread(shifr));
                    threads[i].Start(i);
                    //threads[i].Join();
                    //Thread.Sleep(1000);
                }

                i++;
            }
            for (int l = 0; l < blocks.Length; l++)
                threads[l].Join();

            for (int j = 0; j < blocks.Length; j++)
            {
                text += blocks[j];
            }
            Console.Write("Текст после шифрования: ");
            Console.WriteLine(text);
        }

        static void Uncrypted()
        {
            int i = 0;
            List<Thread> threads = new List<Thread>();
            ParameterizedThreadStart unshifr = new ParameterizedThreadStart(Uncrypt);
            while (text != "")
            {
                if (text.Length < key.Length)
                {
                    int temp = key.Length;
                    while (text.Length != temp)
                        temp--;
                    blocks[i] = text.Substring(0, temp);
                    text = "";
                    threads.Add(new Thread(unshifr));
                    threads[i].Start(i);
                    threads[i].Join();
                    //Thread.Sleep(1000);
                }
                else
                {
                    blocks[i] = text.Substring(0, key.Length);
                    text = text.Remove(0, key.Length);
                    threads.Add(new Thread(unshifr));
                    threads[i].Start(i);
                    threads[i].Join();
                    //Thread.Sleep(1000);
                }

                i++;
            }
            for (int j = 0; j < blocks.Length; j++)
            {
                text += blocks[j];
            }
            Console.Write("Текст после дешифрования: ");
            Console.WriteLine(text);
        }

        static void Crypt(object bl)
        {
            int bl2 = (int) bl;
            char[] block = blocks[bl2].ToCharArray();
            char[] block2 = new char[block.Length];
            for (int i = 0; i < block.Length; i++)      //реверс
            {
                block2[i] = block[block.Length - i - 1];
            }
            //Console.WriteLine(block2);
            char[] k2 = key.ToCharArray();
            //char b, k;
            for (int i = 0; i < block2.Length; i++)     //шифрование
            {
                //b = block2[i]; k = k2[i];
                block[i] = (char) (block2[i] ^ k2[i]);
            }
            blocks[bl2] = new string(block);
            
            //Console.WriteLine(block);
        }

        static void Uncrypt(object bl)
        {
            int bl2 = (int)bl;
            char[] block = blocks[bl2].ToCharArray();
            char[] block2 = new char[block.Length];
            char[] k2 = key.ToCharArray();
            //char b, k;

            for (int i = 0; i < block.Length; i++)      //расшифровывание
            {
                //b = block[i]; k = k2[i];
                block2[i] = (char)(block[i] ^ k2[i]);
            }
            for (int i = 0; i < block2.Length; i++)     //реверс
            {
                block[i] = block2[block2.Length - i - 1];
            }
            //Console.WriteLine(block2);
            blocks[bl2] = new string(block);

            //Console.WriteLine(block);
        }
    }
}
