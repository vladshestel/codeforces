using System;

namespace Issue_461A
{
    class Program
    {
        public static void Main(string[] args) {
            var numbers_count = Helper.ReadInt();
            
            var numbers = new int[numbers_count];
            long sum = 0;

            for (int i = 0; i < numbers_count; ++i) {
                numbers[i] = Helper.ReadInt();
                sum += numbers[i];
            }

            Sort.HeapSort(numbers);

            for (int i = 0; i < numbers_count; ++i) {
                sum += ((long)numbers[i] * (long)(i + 1));
            }

            sum -= numbers[numbers_count - 1];

            Console.WriteLine(sum);
        }
    }

    public static class Sort
    {
        public static void HeapSort(int[] array)
        {
            // create Heap from unordered array
            BuildHeap(array);

            // sort                                                    
            for (var i = array.Length - 1; i >= 0; --i) {
                var temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                FloatUp(array, 0, i);
            }
        }

        private static void BuildHeap(int[] array)
        {
            var size = array.Length;
            var last_index = size - 1;
            
            for (var i = (last_index - 1) / 2; i >= 0; --i) {
                FloatUp(array, i, size);
            }
        }

        private static void FloatUp(int[] array, int index, int size)
        {
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
                var temp = array[index];
                array[index] = array[largest];
                array[largest] = temp;
                FloatUp(array, largest, size);
            }
        }
    }

    public static class Helper {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        public static int ReadInt() {
            const int zeroCode = (int) '0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;
            IsEndOfLine = false;

            while (symbol == ' ') {
                symbol = Console.Read();
            }

            if (symbol == '-') {
                isNegative = true;
                symbol = Console.Read();
            }

            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = Console.Read()
            ) {
                var digit = symbol - zeroCode;
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Console.Read();
                    IsEndOfLine = true;
                    break;
                }

                if (digit < 10 && digit >= 0) {
                    var cache1 = result << 1;
                    var cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    IsInputError = true;
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}