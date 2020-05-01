using System;
using System.IO;

namespace Issue_474B
{
    internal struct Heap {
        public int Count;
        public int Collected;

        public Heap(int count, int collected) {
            Count = count;
            Collected = collected;
        }
    }

    public class Program
    {
        public static void Main(string[] args) {
            var heaps_count = IO.Read();
            var heaps = new Heap[heaps_count];

            var collected = 0;
            for (var i = 0; i < heaps_count; ++i) {
                var count = IO.Read();
                collected += count;

                heaps[i] = new Heap(count, collected);
            }

            var yummy_count = IO.Read();
            for (var i = 0; i < yummy_count; ++i) {
                var number = IO.Read();
                var heap = BinarySearch(heaps, number);

                Console.WriteLine(heap);
            }
        }

        private static int BinarySearch(Heap[] heaps, int number) {
            var lower = 0;
            var upper = heaps.Length - 1;

            while (lower < upper) {
                var index = (upper - lower) / 2 + lower;
                var current = heaps[index];

                if (current.Collected >= number) {
                    if ((current.Collected - current.Count) < number) {
                        return index + 1;
                    } else {
                        upper = index;
                    }
                } else {
                    lower = index + 1;
                }
            }

            return upper + 1;
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