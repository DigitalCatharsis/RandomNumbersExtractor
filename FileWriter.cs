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
        private readonly string _path;
        public FileWriter(string path)
        {
            _path = path;
        }
        public event Action OnComplete;

        private volatile bool _shouldStop;

        public void UpdateStatus()
        {
            var sw = new Stopwatch();
            sw.Start();

            do
            {
                Console.Write(". ");
                Thread.Sleep(1000);
            } while (!_shouldStop);
            sw.Stop();
        }

        public void Write(IEnumerable<int> numbers)
        {
            using (var sw = new StreamWriter(_path + @"\out.txt", false))
            {
                foreach (var elem in numbers)
                {
                    sw.Write("{0,8:N0}", elem);
                }
                _shouldStop = true;
            }
        }
    }
    }
