using System;

namespace Issue_509A
{
    class Program
    {
        public static void Main(string[] args) {
            var n = ReadInt();
            
            var matrix = new int[n, n];

            for (int i = 0; i < n; ++i) {
                matrix[0, i] = 1;
                matrix[i, 0] = 1;
            }

            for (int i = 1; i < n; ++i) {
                for (int j = 1; j < n; ++j) {
                    matrix[i, j] = matrix[i, j - 1] + matrix[i - 1, j];
                }
            }

            Console.WriteLine(matrix[n - 1, n - 1]);
        }

        private static bool IsInputError = false;
        private static bool IsEndOfLine = false;

        private static int ReadInt() {
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