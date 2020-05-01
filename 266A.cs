using System;

namespace Issue_266A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var stonesCount = int.Parse(input);
            var lastStoneColor = 0;
            var stonesToRemove = 0;

            for (int i = 0; i < stonesCount; ++i) {
                var stone = Console.Read();

                if (stone == lastStoneColor) {
                    stonesToRemove++;
                } else {
                    lastStoneColor = stone;
                }
            }

            Console.WriteLine(stonesToRemove);
        }
    }
}