using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RandomNumbersExtractor
{
    internal class FileWriter : INumberWriter
    {
        private Stopwatch sW = new Stopwatch();

        private readonly string _path;
        public FileWriter(string path)
        {
            _path = path;
        }
        public event Action OnComplete;


        public void UpdateStatus()
        {            
            sW.Start();
            Console.Write(". ");
        }

        public void  Write(IEnumerable<int> numbers)
        {
            using (var sw = new StreamWriter(_path + @"\out.txt", false))
            {
                foreach (var elem in numbers)
                {
                    sw.Write("{0,8:N0}", elem);
                }
                OnComplete.Invoke();
                Console.WriteLine("Finished wrighting to file! Saving duration: " + sW.ElapsedMilliseconds + " milliseconds.");//////////////////////////////////
            }
        }

        public void StartWrite(IEnumerable<int> numbers)
        {
            Thread threadSaveToFile = new Thread(() => Write(numbers));  //Сделано специально через лямбду, не помню почему...чертов рефакторинг
            threadSaveToFile.Start();
        }
    }
}
