using System;

namespace Issue_427A
{
    class Program
    {
        public static void Main(string[] args) {
            var events_count = Helper.ReadInt();
            var policemans = 0;
            var crimes = 0;

            for (int i = 0; i < events_count; ++i) {
                var event_code = Helper.ReadInt();

                if (event_code == -1) {
                    if (policemans > 0) {
                        policemans--;
                    } else {
                        crimes++;
                    }
                } else {
                    policemans += event_code;
                }
            }

            Console.WriteLine(crimes);
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