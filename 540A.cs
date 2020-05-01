using System;

namespace Issue_540A
{
    class Program
    {
        public static void Main(string[] args) {
            var n = Helper.ReadInt();
            var initial = Console.ReadLine();
            var destination = Console.ReadLine();
            var distance = 0;
            
            for (int i = 0; i < n; ++i) {
                distance += GetDistance(
                        initial[i].ToInt(), 
                        destination[i].ToInt());
            }

            Console.WriteLine(distance);
        }

        private static int GetDistance(int from, int to) {
            var distance = Math.Abs(from - to);

            return (distance > 5) 
                ? 10 - (distance) 
                : distance;
        }
    }

    static class Helper {
        private static bool IsInputError = false;
        private static bool IsEndOfLine = false;

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

        public static int ToInt(this char c) {
            if (c < '0' || c > '9')
                throw new Exception();

            return c - '0';
        }
    }
}