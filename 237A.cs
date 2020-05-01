using System;

namespace Issue_237A
{
    class Program
    {
        public static void Main(string[] args) {
            var last_time = Time.Create();
            var current_time = Time.Create();
            
            var current_cashboxes_count = 1;
            var maximum_cashboxes_count = 1;

            var guests_count = Helper.ReadInt();

            for (int i = 0; i < guests_count; ++i) {
                current_time.Hour = Helper.ReadInt();
                current_time.Minute = Helper.ReadInt();

                if (current_time.Equals(last_time)) {
                    current_cashboxes_count++;
                } else {
                    if (current_cashboxes_count > maximum_cashboxes_count) {
                        maximum_cashboxes_count = current_cashboxes_count;
                    }

                    current_cashboxes_count = 1;
                    last_time = current_time;
                }
            }

            if (current_cashboxes_count > maximum_cashboxes_count) {
                maximum_cashboxes_count = current_cashboxes_count;
            }

            Console.WriteLine(maximum_cashboxes_count);
        }
    }

    public struct Time : IEquatable<Time> {
        public int Hour;
        public int Minute;

        public bool Equals(Time t) {
            return (this.Hour == t.Hour) && (this.Minute == t.Minute);
        }

        public static Time Create() {
            return new Time { Hour = -1, Minute = -1 };
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