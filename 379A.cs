using System;

namespace Issue_379A
{
    class Program
    {
        public static void Main(string[] args) {
            var candles = ReadInt();
            var requiredDeads = ReadInt();
            var deads = candles;

            while(deads >= requiredDeads) {
                var zombies = deads / requiredDeads;

                candles += zombies;
                deads = zombies + deads % requiredDeads;
            }

            Console.WriteLine(candles);
        }

        private static bool IsInputError = false;

        private static int ReadInt() {
            const int zeroCode = (int) '0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;

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
                
                if (symbol == 13) {
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