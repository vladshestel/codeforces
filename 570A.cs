using System;
using System.IO;

namespace Issue_570A
{
    class Program
    {
        public static void Main(string[] args) {
            var candidates_count = IO.Read();
            var cities_count = IO.Read();

            var results = new int[candidates_count];

            int max_wins = 0;

            int local_winner = 0;
            int max_votes;
            int current;

            for (int i = 0; i < cities_count; ++i) {
                max_votes = -1;
                
                for (int j = 0; j < candidates_count; ++j) {
                    current = IO.Read();

                    if (current > max_votes) {
                        max_votes = current;
                        local_winner = j;
                    }
                }

                results[local_winner]++;
            }

            int global_winner = 0;
            
            for (int i = 1; i < candidates_count; ++i) {
                if (results[i] > results[global_winner]) {
                    global_winner = i;
                }
            }

            Console.WriteLine(global_winner + 1);
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