using System;
using System.IO;

namespace Issue_550A
{
    public static class Program
    {
        public static void Main(string[] args) {
            var line = IO.ReadLine();
            var contains = Contains(line);

            Console.WriteLine(contains ? "YES" : "NO");
        }

        private static bool Contains(string line) {
            var lastAb = -1;
            var abIndex = -1;
            var lastBa = -1;
            var baIndex = -1;

            for (var i = 0; i < line.Length - 1; ++i) {
                var isAb = (line[i] == 'A') && (line[i + 1] == 'B');
                var isBa = (line[i] == 'B') && (line[i + 1] == 'A');
                
                if (isAb) {
                    var isBaDefined = baIndex != -1;
                    var isNotInBound = (i > baIndex + 1) || (lastBa != -1 && i > lastBa + 1);

                    if (isBaDefined && isNotInBound) {
                        return true;
                    } else {
                        lastAb = abIndex;
                        abIndex = i;
                    }
                    
                } else if (isBa) {
                    var isAbDefined = abIndex != -1;
                    var isNotInBound = (i > abIndex + 1) || (lastAb != -1 && i > lastAb + 1);

                    if (isAbDefined && isNotInBound) {
                        return true;
                    } else {
                        lastBa = baIndex;
                        baIndex = i;
                    }
                }
            }

            return false;
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