using System;
using System.Collections.Generic;
using System.IO;

namespace Issue_723A
{
    class Program
    {
        public static void Main(string[] args) {
            var first = IO.Read();
            var second = IO.Read();
            var third = IO.Read();
            
            var result = 0;

            if (first > second) {
                if (first > third) {
                    if (second > third) {
                        result = first - third;
                    } else {
                        result = first - second;
                    }
                } else {
                    result = third - second;
                }
            } else if (second > third) {
                if (first > third) {
                    result = second - third;
                } else {
                    result = second - first;
                }
            } else {
                result = third - first;
            }

            Console.WriteLine(result);
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
                        Reader.Read();          // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}