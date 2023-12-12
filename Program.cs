using System;
using System.Data;

namespace Tasks
{
    public static class Wires672
    {
        public static List<int> GetArrayOfWires()
        {
            Console.WriteLine("введите количество отрезков");
            int length = int.Parse(Console.ReadLine());
            List<int> list = new List<int>(length);
            int counter = 0;

            while (counter != length)
            {
                Console.WriteLine("введите длинну отрезка   " + "осталось " + (length - counter));
                int temp = int.Parse(Console.ReadLine());
                ++counter;
                list.Add(temp);
            }


            return list;
        }

        public static int Razbienie(List<int> list, int middle)
        {
            int counter = 0;

            for (int i = 0; i < list.Count; i++)
            {
                int temp = list[i];

                while (temp >= middle)
                {
                    temp -= middle;
                    ++counter;
                }
            }

            return counter;
        }

        public static int HelpFunction(List<int> list, int neededAmount)
        {
            int middle = list[0] / 2;
            int temp = Razbienie(list, middle);

            if (temp == neededAmount)
            {
                return middle;
            }

            if (temp > neededAmount)
            {
                return HelpFunction(list, ++middle);
            }

            if (temp < neededAmount)
            {
                return HelpFunction(list, --middle);
            }

            return 0;
        }

        public static int FindMiddle(List<int> list, int positionMin, int positionMax, int neededAmount, bool flag)
        {
            int middle = (list[positionMin] + list[positionMax]) / 2 - 1;
            int amount = 0;

            amount = Razbienie(list, middle);

            if (amount > neededAmount && flag)
            {
                return middle;
            }

                if (positionMax == 0)
            {
                return HelpFunction(list, neededAmount);
            }

            if (amount == neededAmount)
            {
                return middle;
            }

            if (amount > neededAmount)
            {
                return FindMiddle(list, (positionMin + positionMax) / 2, list.Count - 1, neededAmount, flag);
            }

            if (amount < neededAmount)
            {
                flag = true;
                return FindMiddle(list, positionMin, (positionMin + list.Count) / 2, neededAmount, flag);
            }

            return -999;
        }

        public static void Wires()
        {
            List<int> list = Wires672.GetArrayOfWires();
            Console.WriteLine("введите koli4estvo константных отрезков");
            int length = int.Parse(Console.ReadLine());

            list.Sort();
            Console.WriteLine(FindMiddle(list, 0, list.Count - 1, length, false));
        }
    }
}
