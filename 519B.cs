using System;
using System.IO;

namespace Issue_519B
{
    class Program
    {
        public static void Main(string[] args) {
            var errors_count = IO.Read();

            var xor_first = 0;

            for (int i = 0; i < errors_count; ++i)
                xor_first ^= IO.Read();

            var xor_second = 0;
            int buffer;

            for (int i = 0; i < errors_count - 1; ++i) {
                buffer = IO.Read();
                xor_first ^= buffer;
                xor_second ^= buffer;
            }

            for (int i = 0; i < errors_count - 2; ++i) {
                xor_second ^= IO.Read();
            }

            Console.WriteLine(xor_first);
            Console.WriteLine(xor_second);
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

        public static int[] Array(int size) {
            var array = new int[size];
            
            for (int i = 0; i < size; ++i)
                array[i] = IO.Read();

            return array;
        }
    }
}