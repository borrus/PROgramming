using System;
using System.IO;
using ProcessingDataForWEB_APP;

namespace PROgramming
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 7)
            {
                Console.Error.WriteLine(
                    "Ошибка: передайте 7 путей в порядке: " +
                    "names, codes, titles, ratings, links, tagWeights, tags.");
                return;
            }

            var paths = new ProcessingPaths(
                Names: args[0],
                Codes: args[1],
                Titles: args[2],
                Ratings: args[3],
                Links: args[4],
                TagWeights: args[5],
                Tags: args[6]);

            foreach (var path in paths.All)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"Файл не найден: {path}");
                    return;
                }
            }

            var result = F.SetF(paths);

            Console.WriteLine("Обработка завершена!");
            Console.WriteLine($"Количество записей: {result.Count}");
        }
    }
}
