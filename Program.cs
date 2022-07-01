using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

#region Таск
//Написать программу, которая выведет указанное число рандомных значений в файл или консоль.
//Требования:
//1.Число рандомных чисел, которое надо вывести, считывается из консоли +
//2. Куда выводим, тоже указывается в консоли, если это файл, то выводим в out.txt +
//3. Проверка на валидность данных. Если данные невалидны, запрашиваем повторно  +
//4. Программа должна уметь выводь вплоть до 1000000000 значений +
//5. Вывод должен происходить в отдельном потоке, чтобы не блокировать основной поток. Т.е. Write должен вернуть управление сразу же +
//6. Пока идет запись, программа должна раз в секунду вызывать UpdateStatus, чтобы пользователь видел, что процесс идет +
//7. UpdateStatus должен:
//7.1.Для консоли - обновлять заголовок консоли, выводя туда время прошедшее с начала записи +
//7.2. Для файла - выводить точку в консоль +
//8. Классы для вывода должны наследоваться от интерфейса:
//public interface INumberWriter
//{
//    event Action OnComplete;

//    void Write(IEnumerable<int> numbers);
//    void UpdateStatus();
//}
//9. После того, как запись будет завершена, вывести в консоль "Done" и время, которое запись заняла

//Tips:
//1.Для запуска функции в отдельном потоке используется класс Thread. Пример использования, в твоем случее:
//void ThreadFunc()
//{

//}
//new Thread(ThreadFunc).Start();

//2.Чтобы корректно остановить программу, в классе Program объяви переменную:
//   private static volatile bool s_shouldStop;
//Проверяй ее в цикле который вызывает UpdateStatus
//   Выстави ее в обработчике завершения процесса записи

//3. Заголовок консоли меняется через Console.Title

#endregion region

namespace RandomNumbersExtractor
{
    internal class Program
    {
        static void Main()
        {
            var _reader = new DivineObject();
            _reader.StartDerive();

            Console.ReadLine();
        }
    }
}

public interface INumberWriter
{
    event Action OnComplete;

    void Write(IEnumerable<int> numbers);
    void UpdateStatus();
}


public class DivineObject
{
    public delegate string Validator();

    private Random _rand = new Random();
    private int _randMin = 0;
    private int _randMax = 100;

    public void StartDerive()
    {
        int digits = int.Parse(InputDigitValidator());

        string method = InputSaveMethodValidator();

        if (method == "y")
        {
            string path = InputSavePathValidator();

            Thread threadSaveToFile = new Thread(() => SaveToFile(digits, path));
            threadSaveToFile.Start();

            UpdateMethod FileMethod = new UpdateMethod(PointUpdater);
            UpdateStatus(PointUpdater, threadSaveToFile);
        }
        else
        {
            Thread threadPrintDigits = new Thread(() => PrintDigits(digits));
            threadPrintDigits.Start();
            UpdateStatus(TitleUpdater, threadPrintDigits);
        }

    }

    private string InputDigitValidator() //Проверка, что вбиваешь именно цифры
    {

        string Input = "";
        Console.Write("\n\t\t\t Please enter your first number: ");
        var validInput = false;
        int result = 0;
        while (!validInput)
        {
            Input = Console.ReadLine();

            validInput = int.TryParse(Input, out result);

            if (!validInput)
            {
                Console.WriteLine("\n\t\t\t Try again, dummy! Use digits");
            }
            else if (result <= 0)
            {
                validInput = false;
                Console.WriteLine("\n\t\t\t AHAHAHAHHAHA! Nice try, dummy. Did you try more than 2 147 483 647 or less than 1? >:3");
            }
            if (result > 1000000000)
            {
                validInput = false;
                Console.WriteLine("\n\t\t\t Wow-wow-wow! Not so fast, sweetheart! You can only do 1 000 000 000");
            }
        }
        return Input;
    }
    private string InputSaveMethodValidator() //на y или n (y сохранить в файл, n вывести на консоль)
    {
        Console.Write("\n\t\t\t Save file (y) or Write it to console (n)?: ");
        string Input = "";
        var validInput = false;
        while (!validInput)
        {
            Input = Console.ReadLine();
            //validate it
            if (Input.Count() == 1)
            {
                if ((Input[0] == 'y') || (Input[0] == 'n'))
                {
                    validInput = true;

                }
                else
                {
                    Console.WriteLine("\n\t\t\tTry again, dummy! Use y or n");
                }
            }
            else
            {
                Console.WriteLine("\n\t\t\tTry again, dummy! Use y or n");
            }
        }

        return Input;
    }
    private string InputSavePathValidator()
    {
        Console.WriteLine("\n\t\t\t Type the file location. Example: C:\\temp  ");
        string Input = "";
        var validInput = false;
        while (!validInput)
        {
            Input = Console.ReadLine();
            if (!Directory.Exists(Input))
            {
                Console.WriteLine("\n\t\t\t Try again, dummy! Example: C:\\temp  ");
            }
            else
            {
                validInput = true;
            }
        }
        return Input;
    }

    private void SaveToFile(int digitInput, string path)
    {
        using (var sw = new StreamWriter(path + @"\out.txt", false))
        {

            Console.WriteLine(digitInput + " random integers between " + _randMin + " and " + _randMax);

            for (int ctr = 0; ctr < digitInput; ctr++)
            {
                sw.Write("{0,8:N0}", _rand.Next(_randMin, _randMax));
            }
        }
    }
    private void PrintDigits(int digitInput)
    {
        Console.WriteLine(digitInput + " random integers between " + _randMin + " and " + _randMax);

        for (int ctr = 0; ctr < digitInput; ctr++)
        {
            Console.Write("{0,8:N0}", _rand.Next(_randMin, _randMax));
        }
    }


    delegate void UpdateMethod(Object ThreadToFinish);

    private void UpdateStatus(UpdateMethod Method, Thread ThreadToFinish)
    {
        Thread UpdStThread = new Thread(new ParameterizedThreadStart(Method));
        UpdStThread.Start(ThreadToFinish);
    }

    public void PointUpdater(Object ThreadToFinish)
    {
        var sw = new Stopwatch();
        sw.Start();

        Thread th = (Thread)ThreadToFinish;
        do
        {
            Console.Write(". ");
            Thread.Sleep(1000);
        } while (th.IsAlive == true);
        sw.Stop();
        Console.WriteLine("Finished saving to file! Saving duration: " + sw.ElapsedMilliseconds + " milliseconds.");
    }

    public void TitleUpdater(Object ThreadToFinish)
    {
        var sw = new Stopwatch();
        sw.Start();

        Thread th = (Thread)ThreadToFinish;
        do
        {
            Console.Title = ("Printing duration is " + (sw.ElapsedMilliseconds).ToString() + " Seconds");
        } while (th.IsAlive == true);
        sw.Stop();
        Console.WriteLine("Finished saving to file! Saving duration: " + sw.ElapsedMilliseconds + " milliseconds.");
    }


}







//static void CheckAndCreateDiR(string destination)  //not sure i have to implement dis
//{
//    if (!Directory.Exists(destination))
//    {
//        Directory.CreateDirectory(destination);
//    }

//}



