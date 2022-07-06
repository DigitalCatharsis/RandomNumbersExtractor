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
        event Action INumberWriter.OnComplete
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateStatus()
        {
            //var sw = new Stopwatch();
            //sw.Start();

            //Thread th = (Thread)ThreadToFinish;
            //do
            //{
            //    Console.Title = ("Printing duration is " + (sw.ElapsedMilliseconds).ToString() + " Seconds");
            //} while (th.IsAlive == true);
            //sw.Stop();
            //Console.WriteLine("Finished saving to file! Saving duration: " + sw.ElapsedMilliseconds + " milliseconds.");
        }

        public void Write(IEnumerable<int> numbers)
        {
        //    Console.WriteLine(digitInput + " random integers between " + _randMin + " and " + _randMax);
        //    foreach (var elem in _DigitsCollection)
        //    {
        //        Console.Write("{0,8:N0}", elem);
        //    }
        }
    }
}
