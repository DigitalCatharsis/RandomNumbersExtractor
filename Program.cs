using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                SaveToFile(PathInputResult);
            }
            else
            {
                PrintDigits();
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
            Console.Write("\n\t\t\tPlease enter your first number: ");
            DigitInput = Console.ReadLine();

            //validate it
            validInput = int.TryParse(DigitInput, out result);

            //If it was invalid, dominate - humiliate
            if (!validInput)
            {
                Console.WriteLine("\n\t\t\tTry again, dummy! Use digits");
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
            Console.WriteLine(@"\n\t\t\t Type the file location. Example: C:\temp  ");
            PathInputResult = Console.ReadLine();
            //validate it
            if (!Directory.Exists(PathInputResult))
            {
                Console.WriteLine(@"\n\t\t\t Try again, dummy! Example: C:\temp  ");
            }
            else
            {
                validInput = true;
            }
        }
    }

    private void SaveToFile(string path)    //Save to file, obvious
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
                sw.WriteLine("{0,8:N0}", rand.Next(_randMin, _randMax));
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

    static void CheckAndCreateDiR(string destination)  //not sure i have to implement dis
    {
        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

    }
}


