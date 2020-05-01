using System;
using System.IO;

namespace Issue_731A
{
    public static class Program
    {
        public static void Main(string[] args) {
            var basics = (int) 'a';

            var name = IO.ReadLine();
            var position = 0;
            var steps = 0;

            for (var i = 0; i < name.Length; ++i) {
                var destination = (int) name[i] - basics;

                steps += OptimalDiff(position, destination);
                position = destination;
            }

            Console.WriteLine(steps);
        }

        private static int OptimalDiff(int from, int to) {
            if (from > to) {
                var temp = from;
                from = to;
                to = temp;
            }

            var diff = to - from;
            
            return diff <= 13
                ? diff
                : 13 - (diff - 13);
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