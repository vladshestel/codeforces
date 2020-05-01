using System;

namespace Issue_567A
{
    class Program
    {
        public static void Main(string[] args) {
            var cities_count = Helper.ReadInt();
            var cities = new int[cities_count];

            for (int i = 0; i < cities_count; ++i) {
                cities[i] = Helper.ReadInt();
            }

            PrintPair(
                Math.Abs(cities[0] - cities[1]),
                Math.Abs(cities[0] - cities[cities_count - 1]));

            for (int i = 1; i < cities_count - 1; ++i) {
                var minimal = Math.Min(Math.Abs(cities[i] - cities[i - 1]), Math.Abs(cities[i] - cities[i + 1]));
                var maximal = Math.Max(Math.Abs(cities[i] - cities[0]), Math.Abs(cities[i] - cities[cities_count - 1]));

                PrintPair(minimal, maximal);
            }

            PrintPair(
                Math.Abs(cities[cities_count - 1] - cities[cities_count - 2]), 
                Math.Abs(cities[cities_count - 1] - cities[0]));
        }

        private static void PrintPair(int first, int second) {
            Console.WriteLine("{0} {1}", first, second);
        }
    }

    public static class Helper {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        public static int ReadInt() {
            const int zeroCode = (int) '0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;
            IsEndOfLine = false;

            while (symbol == ' ') {
                symbol = Console.Read();
            }

            if (symbol == '-') {
                isNegative = true;
                symbol = Console.Read();
            }

            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = Console.Read()
            ) {
                var digit = symbol - zeroCode;
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Console.Read();
                    IsEndOfLine = true;
                    break;
                }

                if (digit < 10 && digit >= 0) {
                    var cache1 = result << 1;
                    var cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    IsInputError = true;
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}