using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal class FileWriter : INumberWriter
    {
        string _path;
        public FileWriter()
        {
            _path = inputClass.InputSavePathValidator();
        }
        public event Action OnComplete;

        public void UpdateStatus()
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

        public void Write(IEnumerable<int> numbers)
        {
            using (var sw = new StreamWriter(path + @"\out.txt", false))
            {
                foreach (var elem in numbers)
                {_path = inputClass.InputSavePathValidator();
                    sw.Write("{0,8:N0}", elem);
                }
            }
        }
    }
}
