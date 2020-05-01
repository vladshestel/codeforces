using System;
using System.IO;

namespace Issue_621A
{
    public static class Program
    {
        public static void Main(string[] args) {
            var count = IO.Read();
            var numbers = new int[count];
            var sum = 0L;
            var saved = 0;

            for (var i = 0; i < count; ++i) {
                var number = IO.Read();

                if ((number & 1) == 0) {
                    sum += number;
                } else {
                    numbers[saved++] = number;
                }
            }

            var odds = new int[saved];
            for (var i = 0; i < saved; ++i) {
                odds[i] = numbers[i];
            }

            odds.Heapsort();

            for (var i = (saved - 1); (i - 1) >= 0; i -= 2) {
                sum += odds[i] + odds[i - 1];
            }

            Console.WriteLine(sum);
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
}