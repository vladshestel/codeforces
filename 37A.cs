using System;
using System.IO;

namespace Issue_37A
{
    public static class Program
    {
        public static void Main(string[] args) {
            var count = IO.Read();
            var woods = new int[1000];

            for (var i = 0; i < count; ++i) {
                var wood = IO.Read();

                woods[wood - 1]++;
            }

            var towers = 0;
            var highest = 0;

            for (var i = 0; i < 1000; ++i) {
                var blocks = woods[i];

                if (blocks > 0) {
                    towers++;

                    if (blocks > highest) {
                        highest = blocks;
                    }
                }
            }

            Console.WriteLine($"{highest} {towers}");
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(),
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
            var reader = Reader;
            var symbol = reader.Read();

            while (symbol == ' ') {
                symbol = reader.Read();
            }

            var isNegative = false;
            if (symbol == '-') {
                isNegative = true;
                symbol = reader.Read();
            }

            int result = 0;
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                var digit = symbol - ZeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = (result << 1) + ((result << 1) << 2) + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        reader.Read();        // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}