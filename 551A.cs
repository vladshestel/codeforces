using System;

namespace Issue_551A
{
    class Program
    {
        public static void Main(string[] args)
        {
            var students_count = Helper.ReadInt();
            var students = new Student[students_count];

            for (int i = 0; i < students_count; ++i)
            {
                students[i].Rating = Helper.ReadInt();
                students[i].Id = i + 1;
            }

            students.HeapSort(true);

            var last_rating = -1;
            var last_place = 0;

            for (int i = 0; i < students_count; ++i) {
                if (last_rating == students[i].Rating) {
                    students[i].Place = last_place;
                } else {
                    students[i].Place = 1 + i;
                    last_place = 1 + i;
                    last_rating = students[i].Rating;
                }
            }

            var places = new int[students_count];
            int buffer;

            for (int i = 0; i < students_count; ++i) {
               buffer = students[i].Id;
               places[buffer - 1] = students[i].Place;
            }

            for (int i = 0; i < students_count; ++i)
            {
                Console.Write("{0} ", places[i]);
            }
        }
    }

    struct Student : IComparable<Student>
    {
        public int Id;
        public int Rating;
        public int Place;

        public int CompareTo(Student other)
        {
            return this.Rating.CompareTo(other.Rating);
        }
    }

    static class Sort
    {
        public static void HeapSort<T>(this T[] array, bool isReversed = false)
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

    public static class Helper
    {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        public static int ReadInt()
        {
            const int zeroCode = (int)'0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;
            IsEndOfLine = false;

            while (symbol == ' ')
            {
                symbol = Console.Read();
            }

            if (symbol == '-')
            {
                isNegative = true;
                symbol = Console.Read();
            }

            for (;
                (symbol != -1) && (symbol != ' ');
                symbol = Console.Read()
            )
            {
                var digit = symbol - zeroCode;

                // if symbol == \n
                if (symbol == 13)
                {
                    // skip next \r symbol
                    Console.Read();
                    IsEndOfLine = true;
                    break;
                }

                if (digit < 10 && digit >= 0)
                {
                    var cache1 = result << 1;
                    var cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                }
                else
                {
                    IsInputError = true;
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}