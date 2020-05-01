using System;

namespace Issue_25A
{
    class Program
    {
        public static void Main(string[] args) {
            var nums_count = Helper.ReadInt();
            var odds_count = 0;
            var even_count = 0;
            var last_even = -1;
            var last_odd = -1;

            for (int i = 0; i < nums_count; ++i) {
                var num = Helper.ReadInt();

                if ((num & 1) == 0) {
                    even_count++;
                    last_even = i;
                } else {
                    odds_count++;
                    last_odd = i;
                }
            }

            Console.WriteLine(odds_count == 1 
                ? last_odd + 1
                : last_even + 1);
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