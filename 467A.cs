using System;
using System.Linq;

namespace Issue_467A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var roomsCount = int.Parse(input);
            var freeRooms = 0;

            for (int i = 0; i < roomsCount; ++i) {
                input = Console.ReadLine();

                var data = input.Split()
                    .Select(int.Parse)
                    .ToArray();

                if ((data[1] - data[0] - 2) >= 0)
                    freeRooms++;
            }

            Console.WriteLine(freeRooms);
        }
    }
}