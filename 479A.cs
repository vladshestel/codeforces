using System;

namespace Issue_479A
{
    class Program
    {
        public static void Main(string[] args) {
            var a = ReadInt();
            var b = ReadInt();
            var c = ReadInt();
            
            var first = 0;
            var second = 0;

            if (c < a) {
                Swap(ref a, ref c);
            }

            first = (a * b) > (a + b) ? (a * b) : (a + b);
            second = (first * c) > (first + c) ? (first * c) : (first + c);
            
            Console.WriteLine(second);
        }
    
        private static void Swap(ref int a, ref int b) {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
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