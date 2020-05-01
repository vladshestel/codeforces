using System;
using System.Linq;

namespace Issue_160A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var coinsCount = int.Parse(input);

            input = Console.ReadLine();

            var coins = input.Split()
                .Select(int.Parse)
                .ToArray();

            Array.Sort(coins);

            var amount = coins.Sum();
            var stealed = 0;
            var stealedCount = 0;

            for (int i = coinsCount - 1; i >= 0; --i) {
                stealed += coins[i];
                stealedCount++;

                if (stealed > (amount - stealed)) {
                    break;
                }
            }

            Console.WriteLine(stealedCount);
        }
    }
}