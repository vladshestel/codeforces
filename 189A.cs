using System;
using System.Collections.Generic;
using System.IO;

namespace Issue_189A
{
    class Program
    {
        public static void Main(string[] args) {
            var n = IO.Read();
            var first = IO.Read();
            var second = IO.Read();
            var third = IO.Read();

            Sort(ref first, ref second, ref third);

            var dp = new int[n + 1];

            for (var i = first; i <= n; ++i) {
                var previous_index = i - first;
                var previous = dp[previous_index];
                if ((previous_index == 0 || previous != 0)) {
                    dp[i] = previous + 1;
                }
                if (i >= second) {
                    previous_index = i - second;
                    previous = dp[previous_index];
                    if ((previous_index == 0 || previous != 0) && (previous + 1 > dp[i])) {
                        dp[i] = previous + 1;
                    }
                }
                if (i >= third) {
                    previous_index = i - third;
                    previous = dp[previous_index];
                    if ((previous_index == 0 || previous != 0) && (previous + 1 > dp[i])) {
                        dp[i] = previous + 1;
                    }
                }
            }

            Console.WriteLine(dp[n]);
        }

        private static void Sort(ref int first, ref int second, ref int third) {
            if (third < first) {
                if (third < second) {
                    Swap(ref first, ref third);
                    if (third < second) {
                        Swap(ref second, ref third);
                    }
                } else {
                    Swap(ref first, ref second);
                    Swap(ref second, ref third);
                }
            } else if (second < first) {
                Swap(ref first, ref second);
            } else if (third < second) {
                Swap(ref second, ref third);
            }
        }

        private static void Swap(ref int a, ref int b) {
            var temp = a;
            a = b;
            b = temp;
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