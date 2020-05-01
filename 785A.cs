using System;
using System.IO;

namespace Issue_785A
{
    public static class Program
    {
        public static void Main(string[] args) {
            var count = IO.Read();
            
            var edges = 0;

            for (var i = 0; i < count; ++i) {
                var figure = IO.ReadLine();

                edges += EdgesByFigure(figure);
            }

            Console.WriteLine(edges);
        }

        private static int EdgesByFigure(string figure) {
            var letter = figure[0];

            switch (letter)
            {
                case 'T': return 4;
                case 'C': return 6;
                case 'O': return 8;
                case 'D': return 12;
                case 'I': return 20;
            }
            
            throw new Exception("What the fuck");
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