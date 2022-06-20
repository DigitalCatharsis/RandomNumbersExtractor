using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal class Program
    {



        static void Main()
        {
            var _reader = new Generator();
            _reader.ReadInput();
            _reader.GenerateDigits();

            Console.ReadLine();
        }
    }
}

public class Generator
{
    private string DigitInput { get; set; }     //а зачем я сделал свойство, а не Private переменную? Мозги кипят...
    private string SaveMethInput { get; set; }     //а зачем я сделал свойство, а не Private переменную? Мозги кипят...
    private int  DigitResult { get; set; }
    private int  SaveMethResult { get; set; }
    public void ReadInput()
    {
        CheckDigitValidation();
        SaveMethodValidation();
    }

    private void CheckDigitValidation()
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
    
    private void SaveMethodValidation()
    {
        var result = 0;
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
        SaveMethResult = result;
    }
        

 public void GenerateDigits()
    {
        int _randMin = 0;
        int _randMax = 100;

        // Instantiate random number generator using system-supplied value as seed.
        Random rand = new Random();


        if (SaveMethResult == 'n')
        {
            // Generate and display N random integers from _randMin to _randMax.
            Console.WriteLine(DigitInput + " random integers between " + _randMin + " and " + _randMax);
            for (int ctr = 0; ctr < DigitResult; ctr++)
            {
                Console.Write("{0,8:N0}", rand.Next(_randMin, _randMax));
            }
        }
        //else
        //{

        //}

    }

 //public void TypeToFile()
 //   {
 //       public static async Task ExampleAsync()
 //   {
 //       string[] lines =
 //       {
 //           "First line", "Second line", "Third line"
 //       };

 //       await File.WriteAllLinesAsync("WriteLines.txt", lines);
 //   }
//}



//    public void ExtractResult()
//    {



//    }


}


