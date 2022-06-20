
double Factorial(int n)
{
    double res = 1;
    for (int i = 2; i <= n; i++)
    {
        res *= i;
    }
    return res;
}

double Degree(double x, int q)
{
    double result = 1;
    while (q > 0)
    {
        result *= x;
        q--;
    }
    return result;
}

double SumForN(double x, int n)
{
    double sum = 0, deg = 1, fact = 1;
    for (int i = 0; i <= n; i++)
    {
        Thread th1 = new Thread(() => deg = Degree(x, i));
        Thread th2 = new Thread(() => fact = Factorial(i));
        th1.Start();
        th2.Start();
        th1.Join();
        th2.Join();
        sum += deg / fact;
    }

    return sum;
}



double summa_while(double x, double epsilon)
{
    double sum = 0.0;
    double sum_old = 0.0;
    int i = 0;

    sum = x * 1.0 / i;

    sum = (Math.Pow(-1, i) / (2 * i + 1)) * Math.Pow(x, 2 * i + 1);
    do
    {
        sum_old = sum;
        i += 1;
        sum += (Math.Pow(-1, i) / (2 * i + 1)) * Math.Pow(x, 2 * i + 1);
    } while (Math.Abs(sum - sum_old) > epsilon);
    return sum;
}


Console.Write("Для вычисления функции e^x введите x: ");
double x = Convert.ToDouble(Console.ReadLine());
//Console.Write("Введите число epsilon: ");
//double epsilon = Convert.ToDouble(Console.ReadLine());
//Console.WriteLine("Total summ_while {0}", summa_while(x, epsilon));
Console.Write("Введите число n: ");
int n = Convert.ToInt32(Console.ReadLine());
double sum = 0;
Thread th = new Thread(() => sum = SumForN(x, n));
th.Start();
th.Join();
Console.WriteLine("\nЗначение функции e^x по ее разложению в ряд Маклорена: {0} при n = {1}", sum, n);


