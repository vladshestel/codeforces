using System;
using System.IO;

namespace Issue_706B
{
    public static class Program
    {
        public static void Main(string[] args) {
            var markets_count = IO.Read();
            var prices = new int[markets_count];

            for (var i = 0; i < markets_count; ++i) {
                prices[i] = IO.Read();
            }

            prices.Heapsort();

            var days_count = IO.Read();

            for (var i = 0; i < days_count; ++i) {
                var money = IO.Read();
                var available_markets = CountMarkets(prices, money);

                Console.WriteLine(available_markets);
            }
        }

        private static int CountMarkets(int[] prices, int money) {
            var left = 0;
            var right = prices.Length;
            var current = right / 2 + (right % 2);

            while (current != right) {
                var price = prices[current];

                if (price > money) {
                    right = current;
                } else if (price <= money) {
                    left = current;
                }

                current = (right + left) / 2 + ((right + left) % 2);
            }

            if (money < prices[left]) {
                return 0;
            }

            return current;
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(),
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
        public static void Heapsort<T>(this T[] array, bool isReversed = false)
            where T : IComparable<T>
        {
            Func<T[], int, int, int> comparer;

            if (isReversed) 
                comparer = Extensions.Min;
            else 
                comparer = Extensions.Max;

            // create Heap from unordered array
            BuildHeap(array, comparer);

            // sort
            for (var i = array.Length - 1; i >= 0; --i)
            {
                Swap(ref array[0], ref array[i]);
                FloatUp(array, 0, i, comparer);
            }
        }

        private static void BuildHeap<T>(T[] array, Func<T[], int, int, int> comparer)
            where T : IComparable<T>
        {
            var size = array.Length;
            var last_index = size - 1;

            for (var i = (last_index - 1) / 2; i >= 0; --i)
            {
                FloatUp(array, i, size, comparer);
            }
        }

        private static void FloatUp<T>(T[] array, int index, int size, Func<T[], int, int, int> comparer)
            where T : IComparable<T>
        {
            var left_node_index = 2 * (index + 1) - 1;
            var right_node_index = 2 * (index + 1);

            if (left_node_index >= size)
            {
                return;
            }

            var largest = comparer(array, left_node_index, index);

            if (right_node_index < size)
            {
                largest = comparer(array, right_node_index, largest);
            }

            if (largest != index)
            {
                Swap(ref array[index], ref array[largest]);
                FloatUp(array, largest, size, comparer);
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    public static class Extensions {
        public static int Max<T>(this T[] array, int firstIndex, int secondIndex)
            where T : IComparable<T>
        {
            return (array[firstIndex].CompareTo(array[secondIndex]) > 0)
                ? firstIndex
                : secondIndex;
        }

        public static int Min<T>(this T[] array, int firstIndex, int secondIndex)
            where T : IComparable<T>
        {
            return (array[firstIndex].CompareTo(array[secondIndex]) <= 0)
                ? firstIndex
                : secondIndex;
        }
    }
}