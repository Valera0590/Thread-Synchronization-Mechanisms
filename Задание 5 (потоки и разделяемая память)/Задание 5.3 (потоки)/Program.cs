


class ReadEventArgs
{
    // Сообщение
    public string Message { get; set; }
    // Сумма, на которую изменился счет
    public int Value { get; set; }

    public ReadEventArgs(string mes, int value)
    {
        Message = mes;
        Value = value;
    }
}
class ReadAndMerge
{
    public delegate void ReadAndMergeHandler(object sender, ReadEventArgs e);
    public event ReadAndMergeHandler Notify;              // Определение события
    static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    public ReadAndMerge(int n)
    {
        N = n;
    }
    public int N { get; private set; }

    public void Read()
    {
        ReadEventArgs eventArgs = new ReadEventArgs("", -1);
        for (int i = Convert.ToInt32(Thread.CurrentThread.Name); i < N; i += 3)
        {
            StreamReader SW = new StreamReader(new FileStream(desktop + "\\Source" + i + ".txt", FileMode.Open, FileAccess.Read));
            while (!SW.EndOfStream)
            {
                if (eventArgs.Value == -1)
                {
                    int value = Convert.ToInt32(SW.ReadLine());
                    eventArgs.Message = $"Считан символ: {value}";
                    eventArgs.Value = value;
                    Notify?.Invoke(this, eventArgs);   // Вызов события 

                }
            }
            SW.Close();
        }
    }
}
class Program
{
    static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    static Semaphore semOne = new Semaphore(1, 1);
    static void Main(string[] args)
    {
        Console.Write("Введите кол-во файлов n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        CreatingFiles(n);

        Thread th1 = new Thread(() => MergingFileTh(n));
        Thread th2 = new Thread(() => MergingFileTh(n));
        Thread th3 = new Thread(() => MergingFileTh(n));
        th1.Name = "0"; th2.Name = "1"; th3.Name = "2";
        th1.Start(); th2.Start(); th3.Start();
        th1.Join(); th2.Join(); th3.Join();

        Console.WriteLine("\nПрограмма закончила слияние.\nДля закрытия программы необходимо нажать любую клавишу на клавиатуре\nВнимание! После закрытия все файлы данной программы будут удалены.");
        Console.ReadKey();
        DeletingFiles(n);
    }

    private static void DisplayMessage(object sender, ReadEventArgs e)
    {
        semOne.WaitOne();
        StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Merged.txt", FileMode.Append, FileAccess.Write));
        SW.Write($"{e.Value} ");
        SW.Close();
        semOne.Release();
        e.Value = -1;
        Console.WriteLine(e.Message);
        e.Message = "";
    }

    static void MergingFileTh(int n)
    {
        ReadAndMerge merging = new ReadAndMerge(n);
        merging.Notify += DisplayMessage;       // добавляем обработчик DisplayMessage

        merging.Read();    // запускаем считывание и запись для каждого потока
    }

    /*------------------ Создание и удаление файлов ----------------------------------------------*/
    static void CreatingFiles(int n)
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < n; i++)
        {
            int count = rnd.Next(4, 10);
            StreamWriter SW = new StreamWriter(new FileStream(desktop + "\\Source" + i + ".txt", FileMode.Create, FileAccess.Write));
            while (count > 0)
            {
                int random = rnd.Next(0, 60);
                SW.WriteLine(random);
                count--;
            }
            SW.Close();
        }
    }

    static void DeletingFiles(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (File.Exists(desktop + "\\Source" + i + ".txt"))
                File.Delete(desktop + "\\Source" + i + ".txt");

        }
        if (File.Exists(desktop + "\\Merged.txt"))
            File.Delete(desktop + "\\Merged.txt");
    }
}