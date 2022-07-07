using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal class ConsoleWriter : INumberWriter
    {

        private volatile bool _shouldStop;

        public event Action OnComplete;

        public void UpdateStatus()
        {
            var sw = new Stopwatch();
            sw.Start();
            do
            {
                Console.Title = ("Printing duration is " + (sw.ElapsedMilliseconds).ToString() + " Seconds");
            } while (_shouldStop == false);
            sw.Stop();
            Console.WriteLine("Finished saving to file! Saving duration: " + sw.ElapsedMilliseconds + " milliseconds.");//////////////////////////////////
        }

        public void Write(IEnumerable<int> numbers)
        {
            foreach (var elem in numbers)
            {
                Console.Write("{0,8:N0}", elem);
            }

            _shouldStop = true;

        }
    }
}
