using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {

        bool finish = false;

        while (!finish)
        {
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.WriteLine("Выберите пункт 1-4 для продолжения:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Введите корректное значение (1-4)!");

                Console.WriteLine("1. Отгадай ответ");
                Console.WriteLine("2. Об авторе");
                Console.WriteLine("3. Сортировка массива");
                Console.WriteLine("4. Выход");
                Console.WriteLine("Выберите пункт 1-4");
            }

            switch (choice)
            {
                case 1:
                    PlayGame();
                    break;
                case 2:
                    ShowAuthor();
                    break;
                case 3:
                    SortArrays();
                    break;
                case 4:
                    finish = ConfirmExit();
                    break;
                default:
                    Console.WriteLine("Введите число от 1 до 4:");
                    break;
            }
        }

        Console.WriteLine("Вы вышли из программы. До свидания!");
    }

    static double InputDouble(string answer)
    {
        while (true)
        {
            Console.Write(answer);
            double value;
            if (double.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Некорректно введено значение!");
        }
    }

    static double ComputeFunction(double a, double b)
    {
        const double PI = Math.PI;
        double degA = a * PI / 180.0;
        double degB = b * PI / 180.0;
        return Math.Pow(Math.Sin((3 * PI) / 4 + (degA / 3)), 2) + Math.Sqrt(Math.Cos(degB));
    }

    static void PlayGame()
    {
        Console.Write("\nФормула для решения: \nf = sin^2(3*Pi/4 + a/3) + sqrt(cos(b))\n");

        double a = InputDouble("Введите значение угла альфа в градусах: ");

        bool check = false;
        double b = 0;
        while (!check)
        {
            b = InputDouble("Введите значение угла бета в градусах: ");
            if (Math.Cos(b * Math.PI / 180.0) < 0)
                Console.WriteLine("cos(b) не может быть отрицательным, так как находится под корнем.");
            else
                check = true;
        }

        double correct = Math.Round(ComputeFunction(a, b), 2);
        PlayGuessingGame(correct);
    }

    static void PlayGuessingGame(double correct)
    {
        int attempts = 3;

        while (attempts > 0)
        {
            double userAnswer = Math.Round(InputDouble("Введите ваш ответ (значение f): "), 2);

            if (userAnswer == correct)
            {
                Console.WriteLine("Поздравляем! Вы выиграли!\n");
                return;
            }

            attempts--;
            if (attempts > 0)
                Console.WriteLine($"Неверный ответ! Осталось попыток: {attempts}");
        }

        Console.WriteLine($"Вы проиграли! Правильный ответ: {correct}\n");
    }

    static void ShowAuthor()
    {
        Console.WriteLine("\nФИО: Яшина Ксения Александровна");
        Console.WriteLine("Группа: 6104-09.03.01D\n");
    }

    static bool ConfirmExit()
    {
        while (true)
        {
            Console.WriteLine("Вы точно хотите выйти?");
            Console.WriteLine("\"д\" - да, выйти");
            Console.WriteLine("\"н\" - нет, вернуться");
            string answer = Console.ReadLine();

            if (answer == "д" || answer == "Д")
                return true;

            if (answer == "н" || answer == "Н")
            {
                Console.WriteLine("Вы вернулись в меню!\n");
                return false;
            }

            Console.WriteLine("Введено некорректное значение! Попробуйте ещё раз.\n");
        }
    }

    static int InputArrayLength()
{
    uint n = 0;
    bool valid = false;

    while (!valid)
    {
        Console.Write("Введите размер массива n (размер более 10 - значения не выводятся): ");

        if (!uint.TryParse(Console.ReadLine(), out n) || n == 0)
        {
            Console.WriteLine("Некорректно введено значение! Введите целое число больше 0.");
        }
        else if (n > int.MaxValue)
        { 
            Console.WriteLine("Слишком большое значение для массива.");
        }
        else
        {
            valid = true;
        }
    }

    return (int)n;
}

    static int[] CreateAndFillArray(int n)
    {
        int[] arr = new int[n];
        Random rnd = new Random();
        for (int i = 0; i < n; i++)
            arr[i] = rnd.Next(-100, 101);
        return arr;
    }

    static int[] CloneArray(int[] source)
    {
        int[] copy = new int[source.Length];
        for (int i = 0; i < source.Length; i++)
            copy[i] = source[i];
        return copy;
    }

    static void PrintArray(int[] arr)
    {
        if (arr.Length > 10)
        {
            Console.WriteLine("Массивы не могут быть выведены на экран, так как длина массива больше 10");
            return;
        }

        Console.WriteLine(string.Join(" ", arr));
    }

    static int[] GnomeSort(int[] arr)
    {
        int i = 0;
        while (i < arr.Length)
        {
            if (i == 0 || arr[i - 1] <= arr[i])
            {
                i++;
            }
            else
            {
                int temp = arr[i];
                arr[i] = arr[i - 1];
                arr[i - 1] = temp;
                i--;
            }
        }
        return arr;
    }

    static int[] CocktailSort(int[] arr)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    int temp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = temp;
                }
            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (arr[i - 1] > arr[i])
                {
                    int temp = arr[i];
                    arr[i] = arr[i - 1];
                    arr[i - 1] = temp;
                }
            }
            left++;
        }

        return arr;
    }

    static void SortArrays()
    {
        int n = InputArrayLength();
        int[] original = CreateAndFillArray(n);

        Console.WriteLine("\nИсходный массив:");
        PrintArray(original);

        int[] forGnome = CloneArray(original);
        int[] forCocktail = CloneArray(original);

        Stopwatch sw1 = Stopwatch.StartNew();
        GnomeSort(forGnome);
        sw1.Stop();

        Stopwatch sw2 = Stopwatch.StartNew();
        CocktailSort(forCocktail);
        sw2.Stop();

        Console.WriteLine("\nМассив после гномьей сортировки:");
        PrintArray(forGnome);
        Console.WriteLine($"Время гномьей сортировки: {sw1.Elapsed.TotalMilliseconds} мс");

        Console.WriteLine("\nМассив после сортировки перемешиванием:");
        PrintArray(forCocktail);
        Console.WriteLine($"Время сортировки перемешиванием: {sw2.Elapsed.TotalMilliseconds} мс");
    }
}
