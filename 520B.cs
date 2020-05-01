using System;
using System.IO;

namespace Issue_520B
{
    public static class Program
    {
        public static void Main(string[] args) {
            var display_number = IO.Read();
            var desired_number = IO.Read();

            if (display_number == desired_number) {
                Console.WriteLine(0);
            } else if (display_number > desired_number) {
                Console.WriteLine(display_number - desired_number);
            } else {
                var count = CountSteps(display_number, desired_number);
                Console.WriteLine(count);
            }
        }

        private static int CountSteps(int display_number, int desired_number) {
            var steps = 0;
            var current = desired_number;

            while(current > display_number) {
                if ((current & 1) == 0) {
                    current = current / 2;
                    steps++;
                } else {
                    current = (current + 1) / 2;
                    steps += 2;
                }
            }

            return steps + display_number - current;
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }

        public static StreamReader Reader { 
            get; set;
        }

        public static string ReadLine() {
            return Reader.ReadLine();
        }

        public static int Read() {
            var reader = Reader;
            var symbol = reader.Read();

            while (symbol == ' ') {
                symbol = reader.Read();
            }

            var isNegative = false;
            if (symbol == '-') {
                isNegative = true;
                symbol = reader.Read();
            }

            int result = 0;
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                var digit = symbol - ZeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = (result << 1) + ((result << 1) << 2) + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        reader.Read();        // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}