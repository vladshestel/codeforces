using System;
using System.IO;

namespace Issue_577A
{
    class Program
    {
        public static void Main(string[] args) {
            var n = IO.Read();
            var x = IO.Read();

            var count = 0;

            for (int i = 1; i <= n; ++i)
                if ((x % i == 0) && (x / i) <= n)
                    count++;

            Console.WriteLine(count);
        }
    }

    public static class IO {
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
                
                if (digit < 10 && digit >= 0) {
                    cache1 = result << 1;
                    cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        Reader.Read();          // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}