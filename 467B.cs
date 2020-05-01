using System;
using System.IO;

namespace Issue_467B
{
    class Program
    {
        public static void Main(string[] args) {
            var soldiers_types = IO.Read();
            var players_count = IO.Read() + 1;
            var delta = IO.Read();

            var armies = new int[players_count];

            for (int i = 0; i < players_count; ++i)
                armies[i] = IO.Read();

            var fedyas_army = armies[players_count - 1];
            var friends_count = 0;

            for (int i = 0; i < players_count - 1; ++i) {
                if (Delta(armies[i], fedyas_army) <= delta)
                    friends_count++;
            }

            Console.WriteLine(friends_count);
        }

        private static int Delta(int a, int b) {
            var diff = a ^ b;
            var delta = 0;

            while (diff > 0) {
                if ((diff & 1) == 1)
                    delta++;

                diff >>= 1;
            }

            return delta;
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