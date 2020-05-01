using System;

namespace Issue_546A
{
    class Suit
    {
        public int Home;
        public int Guest;
    }

    class Program
    {
        public static void Main(string[] args) {
            var teams_count = ReadInt();
            var teams = new Suit[teams_count];

            for (int i = 0; i < teams_count; ++i) {
                var home_suit = ReadInt();
                var guest_suit = ReadInt();

                teams[i] = new Suit { Home = home_suit, Guest = guest_suit };
            }
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