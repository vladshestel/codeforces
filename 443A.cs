using System;

namespace Issue_443A
{
    class Program
    {
        public static void Main(string[] args) {
            var symbol = 0;
            var symbols = new List<int>();

            while (symbol != '}') {
                symbol = Console.Read();
                
                switch (symbol) {
                    case '{' :
                    case ' ' : 
                    case ',' :
                    case '}' :
                        break; // skip
                    default: {
                        if (!symbols.Contains(symbol))
                            symbols.Add(symbol);
                        break;
                    }
                }
            }

            Console.WriteLine(symbols.Count);
        }
    }

    class List<T> 
        where T : IComparable<T>
    {
        class Node
        {
            public Node Next;
            public T Value;
        }

        private Node _head;
        private Node _tail;
        private int _count;

        public List() {
            _count = 0;
        }

        public int Count { get { return _count; } }

        public bool Contains(T value) {
            var node = _head;

            while (node != null) {
                if (node.Value.CompareTo(value) == 0) return true;

                node = node.Next;
            }

            return false;
        }

        public void Add(T value) {
            var new_node = new Node { Value = value };

            if (_head == null) {
                _head = _tail = new_node;
            } else {
                _tail = _tail.Next = new_node;
            }

            _count++;
        }
    }
}