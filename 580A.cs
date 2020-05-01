using System;

namespace Issue_580A
{
    class Program
    {
        public static void Main(string[] args) {
            var days_count = Helper.ReadInt();

            var max_streak = 0;
            var current_streak = 0;
            
            var previous = 0;
            int current;

            for (int i = 0; i < days_count; ++i) {
                current = Helper.ReadInt();

                if (current >= previous) {
                    current_streak++;
                } else {
                    if (current_streak > max_streak) {
                        max_streak = current_streak;
                    }

                    current_streak = 1;
                }

                previous = current;
            }

            if (current_streak > max_streak) {
                max_streak = current_streak;
            }

            Console.WriteLine(max_streak);
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