using System;
using System.IO;
using System.Globalization;

namespace Issue_492B
{
    class Program
    {
        public static void Main(string[] args) {
            var lamps_count = IO.Read();
            var streat_length = IO.Read();

            var coordinates = new int[lamps_count];

            for (int i = 0; i < lamps_count; ++i)
                coordinates[i] = IO.Read();
            
            coordinates.Heapsort();

            var previous = coordinates[0];
            var max = 0;
            int distance;

            for (int i = 1; i < lamps_count; ++i) {
                distance = coordinates[i] - previous;

                if (distance > max)
                    max = distance;

                previous = coordinates[i];
            }

            var start_distance = coordinates[0];
            var end_distance = streat_length - coordinates[lamps_count - 1];
            
            distance = Math.Max(start_distance, end_distance);

            if ((double) distance > ((double) max / 2)) {
                max = distance * 2;
            }

            var result = ((double) max / 2).ToString("F10", new CultureInfo("en-US"));

            Console.WriteLine(result);
        }
    }

    public static class Sort {
        public static void Heapsort(this int[] array) {
            // create Heap from unordered array
            BuildHeap(array);

            // sort
            for (var i = array.Length - 1; i >= 0; --i) {
                Swap(ref array[0], ref array[i]);
                FloatUp(array, 0, i);
            }
        }

        private static void BuildHeap(int[] array) {
            var size = array.Length;
            var last_index = size - 1;
            
            for (var i = (last_index - 1) / 2; i >= 0; --i) {
                FloatUp(array, i, size);
            }
        }

        private static void FloatUp(int[] array, int index, int size) {
            var left_node_index = 2 * (index + 1) - 1;
            var right_node_index = 2 * (index + 1);

            if (left_node_index >= size) {
                return;
            }
            
            var largest = (array[left_node_index] > array[index])
                ? left_node_index 
                : index;

            if (right_node_index < size) {
                largest = (array[right_node_index] > array[largest])
                    ? right_node_index
                    : largest;
            }

            if (largest != index) {
                Swap(ref array[index], ref array[largest]);
                FloatUp(array, largest, size);
            }
        }

        private static void Swap(ref int a, ref int b) {
            var temp = a;
            a = b;
            b = temp;
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