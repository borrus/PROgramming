using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTasks
{
    public class Diploms
    {
        public static int Binary(int S, int side, int element)
        {
            int middle = (side + element) / 2;

            if (middle == side)
            {
                return side + 1;
            }

            if (middle * middle == S)
            {
                return middle;
            }

            if (middle * middle < S)
            {
                return Binary(S, middle, 2 * side);
            }

            if (middle * middle > S)
            {
                return Binary(S, side, middle);
            }

            return 0;
        }

        public static int FindSideBySquare(int S, int side)
        {
            int temp = side * side;

            if (temp == S)
            {
                return side;
            }

            if (temp > S)
            {
                return Binary(S, side / 2, side);
            }

            if (temp < S)
            {
                return FindSideBySquare(S, side * 2);
            }

            return 0;
        }

        public static void Diploms1923()
        {
            Console.WriteLine("введите высоту диплома");
            int height = int.Parse(Console.ReadLine());
            Console.WriteLine("введите длинну диплома");
            int weight = int.Parse(Console.ReadLine());
            Console.WriteLine("введите количество дипломов");
            int amount = int.Parse(Console.ReadLine());

            int first = height;
            int second = weight * amount;
            int square = first * second;

            Console.WriteLine(FindSideBySquare(square, 1));
        }
    }
}
