using System;
using System.IO;

namespace Issue_313B
{
    internal class Peace {
        public int Start;
        public int End;
        public int AggregatedSum;

        public Peace(int start) {
            Start = start;
        }
    }

    internal class Node<T> {
        public T Value;
        public Node<T> Next;

        public Node(T value) {
            Value = value;
        }
    }

    internal class Lookup {
        private Node<Peace> _head;
        private Node<Peace> _last;

        private int _count;
        private Node<Peace>[] _index;

        public Lookup() {
            _count = 0;
        }

        public int Count 
            => _count;

        public void Add(Peace item) {
            var node = new Node<Peace>(item);

            if (_head == null) {
                _head = node;
                _last = node;
            } else {
                _last.Next = node;
                _last = node;
            }

            _count++;
        }

        public Peace this[int index] {
            get {
                var current = _head;

                for (var i = 0; i < index; ++i) {
                    current = current.Next;
                }

                return current.Value;
            }
        }

        public void BuildIndex() {
            var index = new Node<Peace>[_count];
            var node = _head;

            for (var i = 0; i < _count; ++i) {
                index[i] = node;
                node = node.Next;
            }

            _index = index;
        }

        public Node<Peace> Find(int value) {
            var low = 0;
            var high = _count - 1;

            while (low <= high) {
                var middle = (low + high) / 2;

                var node = _index[middle];
                var item = node.Value;

                if ((item.Start <= value) && (item.End >= value)) {
                    return node;
                } else if (item.Start < value) {
                    low = middle + 1;
                } else {
                    high = middle - 1;
                }
            }

            return null;
        }
    }

    public static class Program
    {
        public static void Main(string[] args) {
            var line = IO.ReadLine();
            var statistic = CalculateStatistic(line);
            statistic.BuildIndex();

            var queries_count = IO.Read();

            for (var i = 0; i < queries_count; ++i) {
                var low_bound = IO.Read();
                var high_bound = IO.Read();

                var streaks = StreaksCount(statistic, low_bound, high_bound);

                Console.WriteLine(streaks);
            }
        }

        private static Lookup CalculateStatistic(string line) {
            var peaces = new Lookup();

            var last_symbol = line[0];
            var current_peace = new Peace(1);

            for (var i = 1; i < line.Length; ++i) {
                var current_symbol = line[i];

                if (current_symbol != last_symbol) {
                    current_peace.End = i;
                    peaces.Add(current_peace);
                    
                    var previousSum = current_peace.AggregatedSum;
                    var new_length = current_peace.End - current_peace.Start;
                    current_peace = new Peace(i + 1);
                    current_peace.AggregatedSum = previousSum + new_length;
                    last_symbol = current_symbol;
                }
            }

            current_peace.End = line.Length;
            peaces.Add(current_peace);

            return peaces;
        }

        private static int StreaksCount(Lookup statistic, int low_bound, int high_bound) {
            var low_node = statistic.Find(low_bound);
            var low_peace = low_node.Value;

            var high_node = statistic.Find(high_bound);
            var high_peace = high_node.Value;

            if (low_node == high_node) {
                return high_bound - low_bound;
            }

            var middle_aggregation = high_peace.AggregatedSum - low_peace.AggregatedSum;

            var low_length = low_peace.End - low_peace.Start;
            var real_low_length = Min(low_peace.End, high_bound) - Max(low_peace.Start, low_bound);
            var low_diff = low_length - real_low_length;

            var high_length = Min(high_peace.End, high_bound) - Max(high_peace.Start, low_bound);

            return middle_aggregation - low_diff + high_length;
        }

        private static bool InBound(Peace peace, int high) {
            return peace.Start <= high;
        }

        private static int Max(int a, int b) {
            return a > b ? a : b;
        }

        private static int Min(int a, int b) {
            return a < b ? a : b;
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
}