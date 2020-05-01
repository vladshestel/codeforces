using System;

namespace Issue_515A
{
    class Program
    {
        public static void Main(string[] args) {
            var x = Helper.ReadInt();
            var y = Helper.ReadInt();
            var steps = Helper.ReadInt();

            if (x < 0) x = ~x + 1;
            if (y < 0) y = ~y + 1;

            var minimal_steps_count = x + y;
            var canReach = (steps >= minimal_steps_count) 
                && (((steps - minimal_steps_count) & 1) == 0);

            Console.WriteLine(canReach ? "Yes" : "No");
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