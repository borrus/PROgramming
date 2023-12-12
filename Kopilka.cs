using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tasks;

namespace LastTasks
{
    class Moneta
    {
        public int price = 0;
        public int weight = 0;

        public Moneta(int price, int weight)
        {
            this.price = price;
            this.weight = weight;
        }   
    }

    class UserConversation
    {
        public static int GetStartWeight()
        {
            int result = 0;
            Console.WriteLine("Введите вес пустой копилки ");
            result = int.Parse(Console.ReadLine());

            return result;
        }

        public static int GetFinishWeight()
        {
            int result = 0;
            Console.WriteLine("Введите вес копилки с монетами ");
            result = int.Parse(Console.ReadLine());

            return result;
        }

        public static List<Moneta> CreateListOFMonets()
        {         
            List<Moneta> list = new List<Moneta>();

            Console.WriteLine("Введите количество монет ");
            int amount = int.Parse(Console.ReadLine());

            
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("Введите вес монеты ");
                int temp1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите ценность монеты ");
                int temp2 = int.Parse(Console.ReadLine());

                Moneta tempMoneta = new Moneta(temp2, temp1);

                list.Add(tempMoneta);
            }

            return list;
        }
    }

    class Kopilka
    {
        public static bool CanPutMonet(int position, List<Moneta> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (position >= list[i].weight)
                {
                    return true;
                }
            }

            return false;
        }

        public static int ZapolnenieTable(List<int> table, int position, List<Moneta> list)
        {
            if (!CanPutMonet(position, list))
            {
                return 0;
            }

            int Max = -2;

            for (int i = 0; i < list.Count; i++)
            {
                if (position >= list[i].weight)
                {
                    int ostatokWeight = position - list[i].weight;
                    int temp = table[ostatokWeight] + list[i].price;

                    if (temp > Max)
                    {
                        Max = temp;
                    }

                }
                else
                {
                    table[position] = 0;
                }
            }


            return Max;
        }

        public static void Task()
        {
            int startWeight = UserConversation.GetStartWeight();
            int endWeight = UserConversation.GetFinishWeight();
            List<Moneta> list = UserConversation.CreateListOFMonets();


            if (startWeight >= endWeight)
            {
                Console.WriteLine("Incorrect data");
            }

            if (list.Count == 0)
            {
                Console.WriteLine("Incorrect data");
            }

            int tableLength = endWeight - startWeight;

            List<int> table = new List<int>();

            for (int i = 0; i < tableLength; i++)
            {
                table.Add(-1);
            }

            table[0] = 0;

            for (int i = 1; i < tableLength; i++)
            {
                table[i] = ZapolnenieTable(table, i, list);
            }

            Console.WriteLine(table[tableLength - 1]);
        }

        public static void Main()
        {
            //Kopilka.Task();
            //Wires672.Wires();
            LastTasks.Diploms.Diploms1923();
        }
    }
}
