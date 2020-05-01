using System;
using System.IO;

namespace Issue_489B
{
    public class Program
    {
        public static void Main(string[] args) {
            var n = IO.Read();
            var boys = Fill(n);
            boys.Heapsort();

            var m = IO.Read();
            var girls = Fill(m);
            girls.Heapsort();

            var pairs = CountPairs(boys, girls);

            Console.WriteLine(pairs);
        }

        private static int[] Fill(int count) {
            var levels = new int[count];

            for (var i = 0; i < count; ++i) {
                var level = IO.Read();
                levels[i] = level;
            }

            return levels;
        }

        private static int CountPairs(int[] boys, int[] girls) {
            var pairs = 0;

            var boysPointer = 0;
            var girlsPointer = 0;

            while ((boysPointer < boys.Length) && (girlsPointer < girls.Length)) {
                var boy = boys[boysPointer];
                var girl = girls[girlsPointer];

                if (Abs(boy - girl) > 1) {
                    if (boy < girl) {
                        Shift(boys, ref boysPointer, girl);
                    } else if (girl < boy) {
                        Shift(girls, ref girlsPointer, boy);
                    }
                } else {
                    pairs++;
                    girlsPointer++;
                    boysPointer++;
                }
            }

            return pairs;
        }

        private static int Abs(int number) {
            return number < 0 ? -number : number;
        }

        private static void Shift(int[] array, ref int index, int bound) {
            while (index < array.Length && Abs(array[index] - bound) > 1 && array[index] < bound) {
                index++;
            }
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