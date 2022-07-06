using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal class FileWriter : INumberWriter
    {
        public event Action OnComplete;

        public void UpdateStatus()
        {
            //var sw = new Stopwatch();
            //sw.Start();

            //Thread th = (Thread)ThreadToFinish;
            //do
            //{
            //    Console.Write(". ");
            //    Thread.Sleep(1000);
            //} while (th.IsAlive == true);
            //sw.Stop();
            //Console.WriteLine("Finished saving to file! Saving duration: " + sw.ElapsedMilliseconds + " milliseconds.");
        }

        public void Write(IEnumerable<int> numbers)
        {
            //    Console.WriteLine(digitInput + " random integers between " + _randMin + " and " + _randMax);
            //    foreach (var elem in _DigitsCollection)
            //    {
            //         sw.Write("{0,8:N0}", elem);
            //    }
        }
    }
}
