using System;


namespace Issue_136A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var guestsCount = int.Parse(input);
            var gifts = new int[guestsCount];

            for (int i = 0; i < guestsCount; ++i) {
                var index = ReadInt() - 1;

                gifts[index] = i;
            }

            for (int i = 0; i < guestsCount; ++i) {
                Console.Write("{0} ", gifts[i] + 1);
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