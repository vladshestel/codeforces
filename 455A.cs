using System;
using System.IO;

namespace Issue_455A
{
    class Program
    {
        public static void Main(string[] args) {
            var numbers_count = (int) Helper.Read();
            var numbers = new long[numbers_count];

            for (int i = 0; i < numbers_count; ++i) {
                numbers[i] = Helper.Read();
            }

            numbers.HeapSort();

            long amount = 0;
            int first_block_count;
            long first_block_score;
            int second_block_index;
            int second_block_count;
            long second_block_score;
            int third_block_index;
            int third_block_count;
            long third_block_score;

            for (int i = numbers_count - 1; i >= 0; i--) {
                i = numbers.MarkBlock(i, out first_block_count);
                first_block_score = numbers[i] * first_block_count;

                if (!numbers.IsNextBlockMarkable(i)) {
                    amount += first_block_score;
                } else {
                    second_block_index = numbers.MarkBlock(i - 1, out second_block_count);
                    second_block_score = numbers[second_block_index] * second_block_count;

                    if (!numbers.IsNextBlockMarkable(second_block_index)) {
                        i = second_block_index;
                        amount += (second_block_score > first_block_score) 
                            ? second_block_score
                            : first_block_score;
                    } else {
                        third_block_index = numbers.MarkBlock(
                                second_block_index - 1, 
                                out third_block_count);

                        third_block_score = numbers[third_block_index] * third_block_count;

                        if (second_block_score > first_block_score + third_block_score) {
                            amount += second_block_score;
                            i = third_block_index;
                        } else {
                            amount += first_block_score;
                            i = second_block_index;
                        }
                    }
                }
            }

            Console.WriteLine(amount);
        }
    }

    public static class Extensions {
        // out: new position
        public static int MarkBlock(this long[] numbers, int position, out int count) {
            count = 0;

            if (position >= numbers.Length || position < 0)
                return -1;

            var size = 1;
            var i = position;
            var current = numbers[i];

            while ((i > 0) && (current == numbers[i - 1])) {
                i--;
                size++;
            }

            count = size;
            return i;
        }

        public static bool IsNextBlockMarkable(this long[] numbers, int i) {
            return (i > 0) && (numbers[i] == numbers[i - 1] + 1);
        }
    }

    public static class Helper {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        static Helper() {
            /*Reader = new StreamReader(
                Console.OpenStandardInput(1024 * 10), 
                System.Text.Encoding.ASCII, 
                false, 
                1024 * 10);*/
            Reader = new StreamReader("input.txt");
        }

        public static StreamReader Reader { get; set; }

        public static long Read() {
            const int zeroCode = (int) '0';

            long result = 0;
            var isNegative = false;
            var symbol = Reader.Read();
            IsInputError = false;
            IsEndOfLine = false;

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
                var digit = symbol - zeroCode;
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Reader.Read();
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
        public static void HeapSort(this int[] array) {
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

        private static void Swap(ref int a, ref int b) {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }
}