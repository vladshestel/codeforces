using System;
using System.IO;

namespace Issue_349A
{
    internal struct Cash
    {
        public int Note25;
        public int Note50;
    }

    public static class Program
    {
        public static void Main(string[] args) {
            var count = IO.Read();
            var result = CanCalculate(count);

            Console.WriteLine(result ? "YES" : "NO");
        }

        private static bool CanCalculate(int count) {
            const int Price = 25;
            var cash = new Cash();

            for (var i = 0; i < count; ++i) {
                var note = IO.Read();

                if (note == Price) {
                    cash.Note25++;
                } else if (note == 2 * Price) {
                    if (cash.Note25 == 0) {
                        return false;
                    }

                    cash.Note25--;
                    cash.Note50++;
                } else {
                    if (cash.Note50 == 0 || cash.Note25 == 0) {
                        if (cash.Note25 < 3) {
                            return false;
                        }

                        cash.Note25 -= 3;
                    } else {
                        cash.Note50--;
                        cash.Note25--;
                    }
                }
            }

            return true;
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

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