using System;

namespace Issue_339B
{
    class Program
    {
        public static void Main(string[] args) {
            var homes_count = Helper.ReadInt();
            var deals_count = Helper.ReadInt();
            var current_position = 1;
            long steps = 0;

            for (int i = 0; i < deals_count; ++i) {
                var home_number = Helper.ReadInt();

                if (home_number < current_position) {
                    steps += homes_count - current_position + home_number;
                } else {
                    steps += home_number - current_position;
                }

                current_position = home_number;
            }

            Console.WriteLine(steps);
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