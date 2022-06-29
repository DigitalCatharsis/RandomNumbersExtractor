using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

#region Таск
//Написать программу, которая выведет указанное число рандомных значений в файл или консоль.
//Требования:
//1.Число рандомных чисел, которое надо вывести, считывается из консоли +
//2. Куда выводим, тоже указывается в консоли, если это файл, то выводим в out.txt +
//3. Проверка на валидность данных. Если данные невалидны, запрашиваем повторно +
//4. Программа должна уметь выводь вплоть до 1000000000 значений +
//5. Вывод должен происходить в отдельном потоке, чтобы не блокировать основной поток. Т.е. Write должен вернуть управление сразу же +
//6. Пока идет запись, программа должна раз в секунду вызывать UpdateStatus, чтобы пользователь видел, что процесс идет
//7. UpdateStatus должен:
//7.1.Для консоли - обновлять заголовок консоли, выводя туда время прошедшее с начала записи
//7.2. Для файла - выводить точку в консоль
//7. Классы для вывода должны наследоваться от интерфейса:
//public interface INumberWriter
//{
//    event Action OnComplete;

//    void Write(IEnumerable<int> numbers);
//    void UpdateStatus();
//}
//8.Для старта записи, в классе Program должен быть реализован метод StartWriting, принимающий два параметра: класс, который будет выводить данные и сам набор данных
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

//хотя, забей на 8 пункт, он тут не нужен
#endregion region

namespace RandomNumbersExtractor
{
    internal class Program
    {
        static void Main()
        {
            var _reader = new DivineObject();
            _reader.ReadInput();
            _reader.Derive();



            //using (var sr = new StreamReader(@"C:\TEMP\test.txt"))
            //{

            //    while ((! sr.EndOfStream ))
            //    {
            //        Console.WriteLine(sr.ReadLine());
            //    }
            //}

            Console.Write("\n\t\t\t Done!");
            Console.ReadLine();
        }
    }
}

public class DivineObject
{
    private string DigitInput { get; set; }     //а зачем я сделал свойство, а не Private переменную? Мозги кипят...
    private string SaveMethInput { get; set; }     //а зачем я сделал свойство, а не Private переменную? Мозги кипят...
    private int DigitResult { get; set; }
    private string PathInputResult { get; set; }

    public void ReadInput()   //Метод для получения данных с консоли
    {
        CheckDigitValidation();
        CheckMethodValidation();
        if (SaveMethInput == "y")
        {
            CheckPathValidation();
        }
    }

    public void Derive() //Метод для вывода в файл или на консоль в зависимости от SaveMethInput
    {
        if (DigitInput != null || SaveMethInput != null)
        {
            
            if (SaveMethInput == "y")
            {
                Thread threadSaveToFile = new Thread(new ParameterizedThreadStart(SaveToFile));
                threadSaveToFile.Start(PathInputResult);
            }
            else
            {
                Thread threadPrintDigits = new Thread(new ThreadStart(PrintDigits));
                threadPrintDigits.Start();
            }
        }
        else throw new Exception("Null input");
    }

    
    private void CheckDigitValidation() //Проверка, что вбиваешь именно цифры
    {
        var result = 0;
        var validInput = false;
        while (!validInput)
        {
            //get input 
            Console.Write("\n\t\t\t Please enter your first number: ");
            DigitInput = Console.ReadLine();

            //validate it
            validInput = int.TryParse(DigitInput, out result);

            //If it was invalid, dominate - humiliate
            if (!validInput)
            {
                Console.WriteLine("\n\t\t\t Try again, dummy! Use digits");
            }
            if (result <= 0)
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
        DigitResult = result;
    }

    private void CheckMethodValidation() //Проверка что только Char y или n
    {
        var validInput = false;
        while (!validInput)
        {
            //get input 
            Console.Write("\n\t\t\t Save file (y) or Write it to console (n)?: ");
            SaveMethInput = Console.ReadLine();
            //validate it
            if (SaveMethInput.Count() == 1)
            {
                //If it was invalid, dominate - humiliate
                if (!((char.Parse(SaveMethInput) == 'y') || (char.Parse(SaveMethInput) == 'n')))
                {
                    Console.WriteLine("\n\t\t\tTry again, dummy! Use y or n");
                }
                else
                {
                    validInput = true;
                }
            }
        }
    }
    
    private void CheckPathValidation()   //Можно реализовать попытку создания папки, оставлю метод ниже
    {
        var validInput = false;
        while (!validInput)
        {
            //get input 
            Console.WriteLine("\n\t\t\t Type the file location. Example: C:\\temp  ");
            PathInputResult = Console.ReadLine();
            //validate it
            if (!Directory.Exists(PathInputResult))
            {
                Console.WriteLine("\n\t\t\t Try again, dummy! Example: C:\\temp  ");
            }
            else
            {
                validInput = true;
            }
        }
    }

    private void SaveToFile(object path)    //Save to file, obvious
    {
        using (var sw = new StreamWriter(path + @"\out.txt", false))
        {
            // Instantiate random number generator using system-supplied value as seed.        
            Random rand = new Random();
            int _randMin = 0;
            int _randMax = 100;

            // Generate and display N random integers from _randMin to _randMax.
            Console.WriteLine(DigitInput + " random integers between " + _randMin + " and " + _randMax);

            for (int ctr = 0; ctr < DigitResult; ctr++)
            {
                sw.Write("{0,8:N0}", rand.Next(_randMin, _randMax));
            }
        }
    }

    private void PrintDigits()  //Вывод реализация
    {
        // Instantiate random number generator using system-supplied value as seed.        
        Random rand = new Random();
        int _randMin = 0;
        int _randMax = 100;

        // Generate and display N random integers from _randMin to _randMax.
        Console.WriteLine(DigitInput + " random integers between " + _randMin + " and " + _randMax);

            for (int ctr = 0; ctr < DigitResult; ctr++)
            {
                Console.Write("{0,8:N0}", rand.Next(_randMin, _randMax));
            }
    }

    //static void CheckAndCreateDiR(string destination)  //not sure i have to implement dis
    //{
    //    if (!Directory.Exists(destination))
    //    {
    //        Directory.CreateDirectory(destination);
    //    }

    //}
}


