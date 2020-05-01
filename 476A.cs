using System;

namespace Issue_476A
{
    class Program
    {
        public static void Main(string[] args) {
            var stairs_count = Helper.ReadInt();
            var desired_multiplier = Helper.ReadInt();

            var min_count = (stairs_count / 2) + (stairs_count % 2);
            var suitable_count = -1;

            while (min_count <= stairs_count) {
                if ((min_count % desired_multiplier) == 0) {
                    suitable_count = min_count;
                    break;
                }

                min_count++;
            }

            Console.WriteLine(suitable_count);
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