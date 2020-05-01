using System;
using System.IO;

namespace Issue_499B
{
    internal class Entry : IComparable<Entry> {
        public string Word;
        public string Replacement;

        public Entry(string word) {
            Word = word;
            Replacement = null;
        }

        public Entry(string word, string replacement) {
            Word = word;
            Replacement = replacement;
        }

        public int CompareTo(Entry other) {
            return this.Word.CompareTo(other.Word);
        }
    }

    public static class Program
    {
        public static void Main(string[] args) {
            var lecture_length = IO.Read();
            var words_count = IO.Read();

            var dictionary = FillDictionary(words_count);

            dictionary.Heapsort();
            
            for (var i = 0; i < lecture_length; ++i) {
                var word = IO.ReadWord();

                var translation = dictionary.BinarySearch(new Entry(word));
                var output = translation?.Replacement ?? word;

                Console.WriteLine(output);
            }
        }

        private static Entry[] FillDictionary(int words_count) {
            var dictionary = new Entry[words_count];

            for (var i = 0; i < words_count; ++i) {
                var words = IO.ReadLine().Split();
                
                if (words[0].Length > words[1].Length) {
                    dictionary[i] = new Entry(words[0], words[1]);
                } else {
                    dictionary[i] = new Entry(words[1], words[0]);
                }
            }

            return dictionary;
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

        public static string ReadWord() {
            var buffer = new char[10];
            var length = 0;
            
            var reader = Reader;
            var symbol = reader.Read();

            while (symbol == ' ') {
                symbol = reader.Read();
            }
            
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                if (symbol == 13 || symbol == 10) {
                    reader.Read();
                    break;
                } else {
                    buffer[length++] = (char) symbol;
                }
            }
            
            return new string(buffer, 0, length);
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

    public static class Search {
        public static T BinarySearch<T>(this T[] array, T item) 
            where T : IComparable<T> 
        {
            var left = 0;
            var right = array.Length - 1;

            while (left <= right) {
                var middle = (right + left) / 2;
                var current = array[middle];
                var comparision = current.CompareTo(item);

                if (comparision == 0) {
                    return current;
                } else if (comparision < 0) {
                    left = middle + 1;
                } else {
                    right = middle - 1;
                }

            }

            return default(T);
        }
    }

    public static class Extensions
    {
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