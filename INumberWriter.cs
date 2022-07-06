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
        void Write(IEnumerable<int> numbers);
        void UpdateStatus();
    }
}
