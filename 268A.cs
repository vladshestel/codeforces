using System;

namespace Issue_268A
{
    class KeyValue<TK, TV> 
        where TK : IComparable<TK>
    {
        public TK Key;
        public TV Value;
    }

    class Node<TK, TV> : KeyValue<TK, TV> 
        where TK : IComparable<TK> 
    {
        public Node<TK, TV> Next;
    }

    class PairList<TK, TV> 
        where TK : IComparable<TK> 
    {
        private Node<TK, TV> _head;
        private Node<TK, TV> _tail;
        private int _count;

        public PairList() {
            _count = 0;
        }

        public void Add(TK key, TV val) {
            var new_node = new Node<TK, TV> { 
                Key = key, 
                Value = val
            };

            if (_head == null) {
                _head = _tail = new_node;
            } else {
                _tail.Next = new_node;
                _tail = new_node;
            }

            _count++;
        }

        public bool Contains(TK key) {
            var node = _head;

            while (node != null) {
                if (node.Key.CompareTo(key) == 0)
                    return true;

                node = node.Next;
            }

            return false;
        }

        public int Count {
            get { return _count; }
        }

        public TK[] Keys {
            get {
                var result = new TK[_count];
                var node = _head;

                for (int i = 0; i < _count; ++i) {
                    result[i] = node.Key;
                    node = node.Next;
                }

                return result;
            }
        }

        public TV this[TK key] {
            get {
                var node = _head;

                while (node != null) {
                    if (node.Key.CompareTo(key) == 0) 
                        return node.Value;
                    
                    node = node.Next;
                }

                throw new Exception();
            }

            set {
                var node = _head;

                while (node != null) {
                    if (node.Key.CompareTo(key) == 0) 
                        break;

                    node = node.Next;
                }

                node.Value = value;
            }
        }
    }

    class Program
    {
        public static void Main(string[] args) {
            var teams_count = ReadInt();
            var home_colors = new PairList<int, int>();
            var guest_colors = new PairList<int, int>();

            for (int i = 0; i < teams_count; ++i) {
                var home_suit = ReadInt();
                var guest_suit = ReadInt();

                if (!home_colors.Contains(home_suit)) {
                    home_colors.Add(home_suit, 0);
                }
                if (!guest_colors.Contains(guest_suit)) {
                    guest_colors.Add(guest_suit, 0);
                }

                home_colors[home_suit] += 1;
                guest_colors[guest_suit] += 1;
            }

            var colors = home_colors.Keys;
            var result = 0;

            for (int i = 0; i < home_colors.Count; ++i) {
                var color = colors[i];

                if (guest_colors.Contains(color)) {
                    result += home_colors[color] * guest_colors[color];
                }
            }

            Console.WriteLine(result);
        }

        private static bool IsInputError = false;

        private static int ReadInt() {
            const int zeroCode = (int) '0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;

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