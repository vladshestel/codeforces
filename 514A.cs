using System;
using System.IO;

namespace Issue_514A
{
    class Program
    {
        public static void Main(string[] args) {
            var the_number = IO.Line();

            char current;

            for (int i = 0; i < the_number.Length; ++i) {
                current = the_number[i];

                if (current > '4' && (current != '9' || current == '9' && i != 0)) {
                    Console.Write('9' - current);
                } else {
                    Console.Write(current);
                }
            }
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

        public static string Line() {
            return Reader.ReadLine();
        }

        public static char Symbol() {
            var symbol = Reader.Read();

            // if symbol == \n
            if (symbol == 13) {
                // skip next \r symbol
                Reader.Read();
                return (char) Reader.Read();
            } else {
                return (char) symbol;
            }
        }
    }
}