using System;
using System.IO;

namespace Issue_478B
{
    public static class Program
    {
        public static void Main(string[] args) {
            var n = IO.Read();
            var m = IO.Read();

            Console.WriteLine("{0} {1}", 
                CalculateMinimum(n, m), 
                CalculateMaximum(n, m)
            );
        }

        private static long CalculateMinimum(int n, int m) {
            var base_count = n / m;
            var big_commands = n % m;

            return CombinationsCount(base_count) * (m - big_commands) + CombinationsCount(base_count + 1) * big_commands;
        }

        private static long CalculateMaximum(int n, int m) {
            var capacity = (n - (m - 1));

            return CombinationsCount(capacity);
        }

        private static long CombinationsCount(int elements) {
            return (long) elements * (elements - 1) / 2;
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(int)),
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