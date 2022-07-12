using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace RandomNumbersExtractor
{
    internal class ConsoleWriter : INumberWriter
    {
        private Stopwatch sW = new Stopwatch();

        public event Action OnComplete;



        public void UpdateStatus()
        {
            sW.Start();
            Console.Title = ("Printing duration is " + (sW.ElapsedMilliseconds).ToString() + " Seconds");
        }

        public void Write(IEnumerable<int> numbers)
        {
            foreach (var elem in numbers)
            {
                Console.Write("{0,8:N0}", elem);
            }

            OnComplete.Invoke();

            Console.WriteLine("Finished printing! Saving duration: " + sW.ElapsedMilliseconds + " milliseconds.");//////////////////////////////////
        }

        public void StartWrite(IEnumerable<int> numbers)
        {

            Thread threadSaveToFile = new Thread(() => Write(numbers));  //Сделано специально через лямбду, не помню почему...чертов рефакторинг
            threadSaveToFile.Start();
        }

    }
}
