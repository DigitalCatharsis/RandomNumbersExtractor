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
        private readonly string _method;
        private readonly string _path;

        private readonly List<int> _DigitsCollection;
        

        public Dispatcher()
        {
            var inputClass = new ReaderAndValidator();

            _digits = int.Parse(inputClass.InputDigitValidator());

            _method = inputClass.InputSaveMethodValidator();

            if (_method == "y")
            {
                _path = inputClass.InputSavePathValidator();
            }

            _DigitsCollection = DigitsCollectionGenerator();
        }
        #endregion

        #region write


        public void StartWriteThread()
        {
            INumberWriter writer; // < создать тут, а ниже уже кастануть

            if (_method == "y")
            {
                writer = new FileWriter();
            }
            else
            {
                writer = new ConsoleWriter();
            }

            Thread threadSaveToFile = new Thread(() => writer.Write(DigitsCollectionGenerator()));  //Сделано специально через лямбду, не помню почему...чертов рефакторинг
            threadSaveToFile.Start();

            Thread updateStatus = new Thread(() => writer.UpdateStatus());
            updateStatus.Start();
        }


        #endregion

        #region Update
        delegate void UpdateMethod(Object ThreadToFinish);



        private void UpdateStatus(UpdateMethod Method, Thread ThreadToFinish)
        {
            Thread UpdStThread = new Thread(new ParameterizedThreadStart(Method));
            UpdStThread.Start(ThreadToFinish);
        }

        #endregion

        private List<int> DigitsCollectionGenerator()
        {
            List<int> ListCollection = new List<int>();

            for (int ctr = 0; ctr < _digits; ctr++)
            {
                ListCollection.Add(_rand.Next(_randMin, _randMax));
            }
            return ListCollection;
        }
    }
}
