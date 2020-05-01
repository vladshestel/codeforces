using System;
using System.IO;

namespace Issue_734B
{
    public class Program
    {
        public static void Main(string[] args) {
            var twos = IO.Read();
            var threes = IO.Read();
            var fives = IO.Read();
            var sixs = IO.Read();

            var count_256 = Min(Min(fives, sixs), twos);
            var count_32 = Min(IfNegative(twos - count_256, 0), threes);

            Console.WriteLine(count_256 * 256 + count_32 * 32);
        }

        private static int Min(int a, int b) {
            return a < b ? a : b;
        }

        private static int IfNegative(int a, int b) {
            return a < 0 ? b : a;
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