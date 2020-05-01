using System;
using System.IO;

namespace Issue_507A
{
    class Program
    {
        public static void Main(string[] args) {
            var instruments_count = IO.Read();
            var amount_days_count = IO.Read();

            var requested_days = new List(instruments_count);

            for (int i = 0; i < instruments_count; ++i)
                requested_days[i] = IO.Read();

            requested_days.Heapsort();

            var wasted_days = 0;
            var available_instruments = 0;

            for (int i = 0; i < instruments_count; ++i) {
                wasted_days += requested_days[i];

                if (wasted_days > amount_days_count)
                    break;

                available_instruments++;
            }

            Console.WriteLine(available_instruments);

            for (int i = 0; i < available_instruments; ++i) {
                Console.Write("{0} ", requested_days.OldIndex(i));
            }
        }
    }

    public class List {
        struct Node {
            public int Index;
            public int Value;

            public Node(int index, int value) {
                Index = index;
                Value = value;
            }
        }

        private Node[] _nodes;

        public List(int count) {
            _nodes = new Node[count];
        }

        public int Length {
            get { return _nodes.Length; }
        }

        public int this[int index] {
            get {
                return _nodes[index].Value;
            }

            set { 
                _nodes[index] = new Node(index, value);
            }
        }

        public void Swap(int a, int b) {
            var temp = _nodes[a];
            _nodes[a] = _nodes[b];
            _nodes[b] = temp;
        }

        public int OldIndex(int index) {
            return _nodes[index].Index + 1;
        }
    }

    public static class Sort {
        public static void Heapsort(this List array) {
            // create Heap from unordered array
            BuildHeap(array);

            // sort
            for (var i = array.Length - 1; i >= 0; --i) {
                array.Swap(0, i);
                FloatUp(array, 0, i);
            }
        }

        private static void BuildHeap(List array) {
            var size = array.Length;
            var last_index = size - 1;
            
            for (var i = (last_index - 1) / 2; i >= 0; --i) {
                FloatUp(array, i, size);
            }
        }

        private static void FloatUp(List array, int index, int size) {
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
                array.Swap(index, largest);
                FloatUp(array, largest, size);
            }
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
                var digit = symbol - zeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = result * 10 + digit;
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