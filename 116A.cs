using System;
using System.Linq;

namespace Issue_116A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var stopsCount = int.Parse(input);
            var maxVolume = 0;
            var currentVolume = 0;

            for (int i = 0; i < stopsCount; ++i) {
                input = Console.ReadLine();

                var traffic = input.Split()
                    .Select(int.Parse)
                    .ToArray();

                currentVolume = currentVolume - traffic[0] + traffic[1];

                if (currentVolume > maxVolume)
                    maxVolume = currentVolume;
            }

            Console.WriteLine(maxVolume);
        }
    }
}