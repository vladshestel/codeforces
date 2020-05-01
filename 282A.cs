using System;

namespace Issue_282A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var commandsCount = int.Parse(input);
            var x = 0;

            for (int i = 0; i < commandsCount; ++i) {
                input = Console.ReadLine();

                x += ((input[1] == '+') ? 1 : -1);
            }

            Console.WriteLine(x);
        }
    }
}