using System;

namespace Issue_459A
{
    class Program
    {
        public static void Main(string[] args) {
            var x1 = Helper.ReadInt();
            var y1 = Helper.ReadInt();
            var x2 = Helper.ReadInt();
            var y2 = Helper.ReadInt();

            int x3 = 0, y3 = 0, x4 = 0, y4 = 0;
            var isError = false;

            if (x1 == x2) {         // let left side was inputted
                x3 = x4 = x1 + Math.Abs(y1 - y2);
                y3 = y1;
                y4 = y2;
            } else if (y1 == y2) {  // let bottom side was inputted
                y3 = y4 = y1 + Math.Abs(x1 - x2);
                x3 = x1;
                x4 = x2;
            } else if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2)) {
                // let x3 -- left coordinate,
                // x4 -- right coordinate
                if (x1 < x2) {
                    x3 = x1;
                    y3 = y2;
                    
                    x4 = x2;
                    y4 = y1;
                } else {
                    x3 = x2;
                    y3 = y1;

                    x4 = x1;
                    y4 = y2;
                }
            } else {
                isError = true;
            }

            if (isError)
                Console.WriteLine(-1);
            else
                Console.WriteLine("{0} {1} {2} {3}",
                    x3, y3,
                    x4, y4);
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