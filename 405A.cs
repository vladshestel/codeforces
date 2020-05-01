using System;

namespace Issue_405A
{
    class Program
    {
        public static void Main(string[] args) {
            var columns_count = ReadInt();
            var columns = new int[columns_count];

            for (int i = 0; i < columns_count; ++i) {
                columns[i] = ReadInt();
            }

            Sort.HeapSort(columns);

            for (int i = 0; i < columns.Length; ++i) {
                Console.Write("{0} ", columns[i]);
            }
        }

        private static bool IsInputError = false;
        private static bool IsEndOfLine = false;

        private static int ReadInt() {
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

    static class Sort 
    {
        public static void HeapSort<T>(this T[] array) 
            where T : IComparable<T> 
        {
            // create Heap from unordered array
            BuildHeap(array);

            // sort
            for (var i = array.Length - 1; i >= 0; --i) {
                Swap(ref array[0], ref array[i]);
                FloatUp(array, 0, i);
            }
        }

        private static void BuildHeap<T>(T[] array)
            where T: IComparable<T>
        {
            var size = array.Length;
            var last_index = size - 1;
            
            for (var i = (last_index - 1) / 2; i >= 0; --i) {
                FloatUp(array, i, size);
            }
        }

        private static void FloatUp<T>(T[] array, int index, int size)
            where T: IComparable<T>
        {
            var left_node_index = 2 * (index + 1) - 1;
            var right_node_index = 2 * (index + 1);

            if (left_node_index >= size) {
                return;
            }
            
            var largest = (array[left_node_index].CompareTo(array[index]) > 0)
                ? left_node_index 
                : index;

            if (right_node_index < size) {
                largest = (array[right_node_index].CompareTo(array[largest]) > 0)
                    ? right_node_index
                    : largest;
            }

            if (largest != index) {
                Swap(ref array[index], ref array[largest]);
                FloatUp(array, largest, size);
            }
        }

        private static void Swap<T>(ref T a, ref T b) {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}