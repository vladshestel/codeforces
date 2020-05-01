using System;
using System.IO;

namespace Issue_527A
{
    class Program
    {
        public static void Main(string[] args) {
            var width = IO.Read();
            var height = IO.Read();

            long buff;

            
            if (width < height) {
                buff = width;
                width = height;
                height = buff;
            }

            long count = 0;

            while (height > 0) {
                count += width / height;
                buff = width % height;
                width = height;
                height = buff;
            }

            Console.WriteLine(count);
        }
    }

    public static class IO {
        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(long)),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(long),
                false);
        }

        public static StreamReader Reader { get; set; }

        public static long Read() {
            const int zeroCode = (int) '0';

            long result = 0;
            int digit;
            long cache1;
            long cache2;

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

        public static string Line() {
            return Reader.ReadLine();
        }
    }
}