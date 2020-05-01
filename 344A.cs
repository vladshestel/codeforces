using System;

namespace Issue_344A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var count = int.Parse(input);
            var islands = 0;
            var previous = string.Empty;

            for (int i = 0; i < count; ++i) {
                var polarity = Console.ReadLine();

                if (!IsSamePolarity(polarity, previous)) {
                    islands++;
                    previous = polarity;
                }
            }

            Console.WriteLine(islands);
        }

        private static bool IsSamePolarity(string a, string b) {
            return b != string.Empty && a[0] == b[0];
        }
    }
}