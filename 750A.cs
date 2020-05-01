
using System;
using System.IO;
using System.Numerics;

namespace Issue_750A
{
    public class Program
    {
        public static void Main(string[] args) {
            const int MinutesInCommon = 4 * 60;

            var n = IO.Read();
            var minutesToParty = IO.Read();

            var availableMinutes = MinutesInCommon - minutesToParty;

            var wastedMinutes = 0;
            var solvedCount = 0;

            for (var i = 0; i < n; ++i) {
                var requiredMinutes = (i + 1) * 5;
                if (wastedMinutes + requiredMinutes <= availableMinutes) {
                    wastedMinutes += requiredMinutes;
                    solvedCount++;
                } else {
                    break;
                }
            }

            Console.WriteLine(solvedCount);
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