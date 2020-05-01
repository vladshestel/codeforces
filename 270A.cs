using System;
using System.IO;

namespace Issue_270A
{
    class Program
    {
        public static void Main(string[] args) {
            var tests_count = IO.Read();
            
            for (int i = 0; i < tests_count; ++i)
                Console.WriteLine(IsRegularAngle(IO.Read()) 
                    ? "YES"
                    : "NO");
        }

        private static bool IsRegularAngle(int angle) {
            const double eps = 1e-9;

            var sides = 2 / (1 - ((double) angle / 180));
            var err = Math.Abs(sides - Math.Round(sides));

            return err <= eps;
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
                var digit = symbol - zeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = result * 10 + digit;
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