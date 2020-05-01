using System;
using System.IO;
using System.Numerics;

namespace Issue_734A
{
    public class Program
    {
        public static void Main(string[] args) {
            const string Anton = "Anton";
            const string Danik = "Danik";
            const string Friendship = "Friendship";
            const int AntonCode = (int) 'A';

            var n = IO.Read();
            var reader = IO.Reader;
            var antonCount = 0;

            for (var i = 0; i < n; ++i) {
                var code = reader.Read();
                
                if (code == AntonCode)
                    antonCount++;
            }

            var danikCount = (n - antonCount);

            Console.WriteLine(antonCount > danikCount
                    ? Anton
                : antonCount == danikCount
                    ? Friendship
                    : Danik);
        }
    }

    public static class IO {
        private const int ZeroCode = (int) '0';

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