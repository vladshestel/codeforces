using System;
using System.IO;

namespace Issue_439A
{
    class Program
    {
        public static void Main(string[] args) {
            const int PauseDuration = 10;
            const int JokeDuration = 5;

            var songs_count = IO.Read();
            var event_duration = IO.Read();

            var real_duration = 0;

            for (int i = 0; i < songs_count; ++i)
                real_duration += IO.Read();

            real_duration += (songs_count - 1) * PauseDuration;

            var result = (real_duration <= event_duration
                ? (songs_count - 1) * 2 + (event_duration - real_duration) / JokeDuration
                : -1);

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