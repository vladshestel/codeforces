using System;
using System.IO;

namespace Issue_149A
{
    class Program
    {
        public static void Main(string[] args) {
            const int months_count = 12;

            var height = IO.Read();
            var months = new int[months_count];

            for (int i = 0; i < months_count; ++i)
                months[i] = IO.Read();

            months.Heapsort();

            var needed_months = 0;
            var current_height = 0;

            for (int i = months_count - 1; i >= 0; --i) {
                if (current_height >= height)
                    break;

                current_height += months[i];
                needed_months++;
            }

            if (current_height < height)
                needed_months = -1;

            Console.WriteLine(needed_months);
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