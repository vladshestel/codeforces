using System;
using System.IO;

namespace Issue_581A
{
    class Program
    {
        public static void Main(string[] args) {
            var reds = IO.Read();
            var blues = IO.Read();

            if (reds < blues) {
                Swap(ref reds, ref blues);
            }

            var different = blues;
            var equal = (reds - blues) >> 1;

            Console.WriteLine("{0} {1}", different, equal);
        }

        public static void Swap(ref int a, ref int b) {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }

    public static class IO {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(int)),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }

        public static StreamReader Reader { get; set; }

        public static int Read() {
            const int zeroCode = (int) '0';

            int result = 0;
            int digit;
            int cache1;
            int cache2;

            var isNegative = false;
            var symbol = Reader.Read();
            IsInputError = false;
            IsEndOfLine = false;

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == '-') {
                isNegative = true;
                symbol = Reader.Read();
            }

            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = Reader.Read()
            ) {
                digit = symbol - zeroCode;
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Reader.Read();
                    IsEndOfLine = true;
                    break;
                }

                if (digit < 10 && digit >= 0) {
                    cache1 = result << 1;
                    cache2 = cache1 << 2;
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