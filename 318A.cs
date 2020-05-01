using System;

namespace Issue_318A
{
    class Program
    {
        public static void Main(string[] args) {
            var n = ReadLong();
            var k = ReadLong();

            var odds_bound = n / 2 + (n & 1);
            var is_even = k > odds_bound;

            if (is_even) {
                k = k - odds_bound;
            }
            
            var result = k * 2 - (is_even ? 0 : 1);
            
            Console.WriteLine(result);
        }

        private static bool IsInputError = false;
        private static bool IsEndOfLine = false;

        private static long ReadLong() {
            const int zeroCode = (int) '0';

            long result = 0;
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