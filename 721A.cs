using System;
using System.IO;
using System.Text;

namespace Issue_721A
{
    public class Program
    {
        public static void Main(string[] args) {
            const int Black = (int) 'B';

            var size = IO.Read();

            var line = new int[(size / 2) + (size & 1)];
            var streak = 0;
            var count = 0;
            var previousWhite = true;

            for (var i = 0; i < size; ++i) {
                var flag = IO.ReadChar();

                if (flag == Black) {
                    previousWhite = false;
                    streak++;
                } else {
                    if (!previousWhite) {
                        line[count++] = streak;
                        streak = 0;
                    } 

                    previousWhite = true;
                }
            }

            if (!previousWhite) {
                line[count++] = streak;
            }

            Console.WriteLine(count);
            var builder = new StringBuilder(count * 2);

            if (count > 0) {
                for (var i = 0; i < count - 1; ++i) {
                    builder.Append(line[i]);
                    builder.Append(" ");
                }
                builder.Append(line[count - 1]);

                Console.WriteLine(builder.ToString());
            }
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

        public static int ReadChar() {
            var symbol = Reader.Read();

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == 13) {
                Reader.Read();
                symbol = Reader.Read();
            }

            return symbol;
        }
    }
}