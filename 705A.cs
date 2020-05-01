using System;
using System.IO;

namespace Issue_705A
{
    class Program
    {
        private const string HateEmotion = "I hate";
        private const string LoveEmotion = "I love";

        public static void Main(string[] args) {
    		
            const string Connector = " that ";
            const string Thing = " it";

            var n = IO.Read();
            
            for (var i = 0; i < n; ++i) {
                var message = ((i % 2) == 0)
                    ? HateEmotion
                    : LoveEmotion;

                Console.Write(message);

                if (i + 1 < n) {
                    Console.Write(Connector);
                }
            }
            
            Console.Write(Thing);
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
                        Reader.Read();          // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}