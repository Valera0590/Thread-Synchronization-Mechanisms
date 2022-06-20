

List<int> GeneratingList(int n)
{
    List<int> listRnd = new List<int>(n);
    Random rnd = new(DateTime.Now.Millisecond);
    for (int i = 0; i < n; i++)
    {
        listRnd.Add(rnd.Next(0,200));
    }
    return listRnd;
}

List<int> MergeSort(List<int> spisok)
{
    if (spisok.Count > 2)
    {
        List<int> spL = new List<int>(spisok.Count / 2);
        spL = spisok.GetRange(0,spisok.Count / 2);
        List<int> spR = new List<int>(spisok.Count - spL.Count);
        spR = spisok.GetRange(spisok.Count / 2, spisok.Count - spL.Count);
        //spL = MergeSort(spL);
        //spR = MergeSort(spR);
        Thread th1 = new Thread(() => spL = MergeSort(spL));
        Thread th2 = new Thread(() => spR = MergeSort(spR));
        th1.Start();
        th2.Start();
        th1.Join();
        th2.Join();
        List<int> mergingSp = new List<int>(spL.Count + spR.Count);
        while((spL.Count != 0) && (spR.Count != 0))
        {
            if(spL.Min() >= spR.Min())
            {
                mergingSp.Add(spR.Min());
                spR.Remove(spR.Min());
            }
            else
            {
                mergingSp.Add(spL.Min());
                spL.Remove(spL.Min());
            }
        }
        if (spL.Count != 0) mergingSp.AddRange(spL);
        if (spR.Count != 0) mergingSp.AddRange(spR);
        spL = null;
        spR = null;
        return mergingSp;
    }
    else if (spisok.Count == 2)
    {
        if (spisok[0] >= spisok[1])
        {
            int t = spisok[0];
            spisok[0] = spisok[1];
            spisok[1] = t;
        }
    }
    return spisok;

}

void PrintList(List<int> sp)
{
    foreach (int el in sp)
        Console.Write(el+" ");
    Console.WriteLine();
}

Console.Write("Введите кол-во элементов для случайной генерации списка: ");
List<int> spisok = GeneratingList(Convert.ToInt32(Console.ReadLine()));
Console.Write("Исходный список: ");
PrintList(spisok);
Thread th = new Thread(() => spisok = MergeSort(spisok));
th.Start();
th.Join();
Console.Write("\nСписок после сортировки: ");
PrintList(spisok);


