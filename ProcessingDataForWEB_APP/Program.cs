using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROgramming
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Ошибка: передайте пути к файлам (names, codes, movies, ratings).");
                return;
            }

            var processor = new DataProcessor();
            // Передаем пути из консоли в твой метод
            var result = processor.SetF(args[0], args[1], args[2], args[3]);
            
            Console.WriteLine("Обработка завершена!");
            Console.WriteLine($"Количество фильмов: {result.Count}");
        }
    }
}
