using System;
using System.IO;

namespace Issue_459B
{
    public class Program
    {
        public static void Main(string[] args) {
            var flowers_count = IO.Read();
            
            var min = int.MaxValue;
            var mins_count = 0;
            var max = int.MinValue;
            var max_count = 0;

            for (var i = 0; i < flowers_count; ++i) {
                var flower = IO.Read();
                
                if (flower < min) {
                    min = flower;
                    mins_count = 1;
                } else if (flower == min) {
                    mins_count++;
                }
                if (flower > max) {
                    max = flower;
                    max_count = 1;
                } else if (flower == max) {
                    max_count++;
                }
            }

            var median = max - min;
            long diffsCount;

            if (median == 0) {
                var square = (long) mins_count * mins_count;
                diffsCount = square - (long) (square + mins_count) / 2;
            } else {
                diffsCount = (long) mins_count * max_count;
            }

            Console.WriteLine("{0} {1}", median, diffsCount);
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

        public static StreamReader Reader { 
            get; set;
        }

        public static string ReadLine() {
            return Reader.ReadLine();
        }

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
                        Reader.Read();        // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }

        public static char ReadChar() {
            var symbol = Reader.Read();

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == 13) {        // if symbol == \n
                Reader.Read();         // skip next \r symbol
                symbol = Reader.Read();
            }

            return (char) symbol;
        }
    }
}