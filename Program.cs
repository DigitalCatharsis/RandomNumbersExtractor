using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

#region Таск
//Написать программу, которая выведет указанное число рандомных значений в файл или консоль.
//Требования:
//1.Число рандомных чисел, которое надо вывести, считывается из консоли +
//2. Куда выводим, тоже указывается в консоли, если это файл, то выводим в out.txt +
//3. Проверка на валидность данных. Если данные невалидны, запрашиваем повторно  +
//4. Программа должна уметь выводь вплоть до 1000000000 значений +
//5. Вывод должен происходить в отдельном потоке, чтобы не блокировать основной поток. Т.е. Write должен вернуть управление сразу же +
//6. Пока идет запись, программа должна раз в секунду вызывать UpdateStatus, чтобы пользователь видел, что процесс идет +
//7. UpdateStatus должен:
//7.1.Для консоли - обновлять заголовок консоли, выводя туда время прошедшее с начала записи +
//7.2. Для файла - выводить точку в консоль +
//8. Классы для вывода должны наследоваться от интерфейса:
//public interface INumberWriter
//{
//    event Action OnComplete;

//    void Write(IEnumerable<int> numbers);
//    void UpdateStatus();
//}
//9. После того, как запись будет завершена, вывести в консоль "Done" и время, которое запись заняла

//Tips:
//1.Для запуска функции в отдельном потоке используется класс Thread. Пример использования, в твоем случее:
//void ThreadFunc()
//{

//}
//new Thread(ThreadFunc).Start();

//2.Чтобы корректно остановить программу, в классе Program объяви переменную:
//   private static volatile bool s_shouldStop;
//Проверяй ее в цикле который вызывает UpdateStatus
//   Выстави ее в обработчике завершения процесса записи

//3. Заголовок консоли меняется через Console.Title

#endregion region

namespace RandomNumbersExtractor
{
    internal class Program
    {

        private static readonly bool s_shouldStop; //Чтобы корректно остановить программу

        static void Main()
        {
            var temp = new Dispatcher();
            Console.ReadLine();
        }
    }
}






//static void CheckAndCreateDiR(string destination)  //not sure i have to implement dis
//{
//    if (!Directory.Exists(destination))
//    {
//        Directory.CreateDirectory(destination);
//    }

//}



