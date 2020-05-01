using System;
using System.IO;

namespace Issue_583A
{
    class Program
    {
        public static void Main(string[] args) {
            var roads_count = IO.Read();
            var h_roads = new bool[roads_count];
            var v_roads = new bool[roads_count];

            int horizontal;
            int vertical;

            for (int i = 0; i < roads_count * roads_count; ++i) {
                horizontal = IO.Read() - 1;
                vertical = IO.Read() - 1;

                if (!h_roads[horizontal] && !v_roads[vertical]) {
                    h_roads[horizontal] = v_roads[vertical] = true;
                    Console.Write("{0} ", i + 1);
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