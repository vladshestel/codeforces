using System;
using System.IO;

namespace Issue_716A
{
    public class Program
    {
        public static void Main(string[] args) {
            var words_count = IO.Read();
            var timing = IO.Read();

            var previous_time = 0;
            var remining_words_count = 0;

            for (var i = 0; i < words_count; ++i) {
                var time = IO.Read();
                if (time - previous_time > timing) {
                    remining_words_count = 0;
                }
                previous_time = time;
                remining_words_count++;
            }

            Console.WriteLine(remining_words_count);
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