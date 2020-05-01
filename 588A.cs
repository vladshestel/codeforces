using System;
using System.IO;

namespace Issue_588A
{
    class Program
    {
        public static void Main(string[] args) {
            var days_count = IO.Read();
            
            var inputs = new Pair[days_count];

            for (int i = 0; i < days_count; ++i) {
                inputs[i].Kg = IO.Read();
                inputs[i].Price = IO.Read();
            }

            var amount_price = 0;

            for (int i = 0; i < days_count; ++i) {
                var current_price = inputs[i].Price;
                var amount_kg = inputs[i].Kg;

                for (int j = i + 1; j < days_count; ++j) {
                    if (inputs[j].Price >= current_price) {
                        amount_kg += inputs[j].Kg;
                        i++;
                    } else {
                        break;
                    }
                }

                amount_price += amount_kg * current_price;
            }

            Console.WriteLine(amount_price);
        }
    }

    public struct Pair {
        public int Kg;
        public int Price;
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