using System;

namespace Issue_69A
{
    class Program
    {
        public static void Main(string[] args) {
            var cells_count = ReadInt();
            var x_sum = 0;
            var y_sum = 0;
            var z_sum = 0;

            for (int i = 0; i < cells_count; ++i) {
                var x = ReadInt();
                var y = ReadInt();
                var z = ReadInt();

                x_sum += x;
                y_sum += y;
                z_sum += z;
            }

            var isZeros = (x_sum | y_sum | z_sum) == 0;

            Console.WriteLine(isZeros ? "YES" : "NO");
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
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Console.Read();
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