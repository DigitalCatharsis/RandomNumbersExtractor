using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal class Dispatcher 
    {

        #region Initialization
        private readonly Random _rand = new Random();
        private readonly int _randMin = 0;
        private readonly int _randMax = 100;

        private readonly int _digits;
        public string Method { get; private set; }
        private readonly string _path;       

        private INumberWriter _writer; //выбор Writera

        public Dispatcher(int digits, string method, string path)
        {
            _digits = digits;

            Method = method;

            _path = path;
        }
        #endregion



        public void StartWriteThread()
        {
            

            if (Method == "y")
            {
                _writer = new FileWriter(_path);
            }
            else
            {
                _writer = new ConsoleWriter();
            }

            //_writer.OnComplete += () => WriteThreadFinishingHandler();

            Thread threadSaveToFile = new Thread(() => _writer.Write(GetItem()));  //Сделано специально через лямбду, не помню почему...чертов рефакторинг
            threadSaveToFile.Start();


            Thread updateStatus = new Thread(() => _writer.UpdateStatus());
            updateStatus.Start();
        }


        private IEnumerable<int> GetItem() // Перешел от генератора списка к Инумераторам, чтобы не засрать память при обращении ко всем эллементам списка
        {           
            for (int ctr = 0; ctr < _digits; ctr++)
            {
                var randomNumber = _rand.Next(_randMin, _randMax); 
                yield return randomNumber;
            }

        }

        //private List<int> DigitsCollectionGenerator()
        //{
        //    List<int> ListCollection = new List<int>();

        //    for (int ctr = 0; ctr < _digits; ctr++)
        //    {
        //        ListCollection.Add(_rand.Next(_randMin, _randMax));
        //    }
        //    return ListCollection;
        //}
    }
}
