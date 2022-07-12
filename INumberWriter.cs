using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumbersExtractor
{
    internal interface INumberWriter
    {
        event Action OnComplete;
        void StartWrite(IEnumerable<int> numbers);
        void UpdateStatus();
    }
}
