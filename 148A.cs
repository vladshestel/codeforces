using System;

namespace Issue_148A
{
    class Program
    {
        public static void Main(string[] args) {
            const int dividersCount = 4;

            string input;
            var dividers = new int[dividersCount];

            for (int i = 0; i < dividersCount; ++i) {
            	input = Console.ReadLine();
            	dividers[i] = int.Parse(input);
            }

            input = Console.ReadLine();
            var dragonsCount = int.Parse(input);
            var fightedDragons = 0;

            for (int i = 1; i <= dragonsCount; ++i) {
                for (int j = 0; j < dividersCount; ++j) {
                    if ((i % dividers[j]) == 0) {
                        fightedDragons++;
                        break;
                    }
                }
            }

            Console.WriteLine(fightedDragons);
        }
    }
}