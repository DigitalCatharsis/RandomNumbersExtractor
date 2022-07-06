using System;
using System.IO;
using System.Linq;

namespace RandomNumbersExtractor
{
    internal class ReaderAndValidator
    {
        public delegate string Validator();



        public string InputDigitValidator() //Проверка, что вбиваешь именно цифры
        {

            string Input = "";
            Console.Write("\n\t\t\t Please enter your first number: ");
            var validInput = false;
            while (!validInput)
            {
                Input = Console.ReadLine();

                validInput = int.TryParse(Input, out int result);

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
        public string InputSaveMethodValidator() //на y или n (y сохранить в файл, n вывести на консоль)
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
        public string InputSavePathValidator()
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

    }
}
