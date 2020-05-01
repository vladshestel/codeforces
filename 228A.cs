using System;

namespace Issue_228A
{
    class List 
    {
        private class Node 
        {
            public Node Next;
            public int Value;
        }

        private Node _head;
        private Node _tail;

        public int Count;

        public void Add(int value) {
            var node = new Node { Value = value };

            if (_head == null) {
                _head = _tail = node;
            } else {
                _tail.Next = node;
                _tail = node;
            }

            Count++;
        }

        public bool Contains(int value) {
            var node = _head;

            while (node != null) {
                if (node.Value == value)
                    return true;

                node = node.Next;
            }

            return false;
        }
    }

    class Program
    {
        public static void Main(string[] args) {
            var shoes = new int[] { ReadInt(), ReadInt(), ReadInt(), ReadInt() };
            var list = new List();

            for (int i = 0; i < shoes.Length; ++i) {
                var shoe = shoes[i];
                
                if (!list.Contains(shoe))
                    list.Add(shoe);
            }


            Console.WriteLine(4 - list.Count);
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
                
                if (symbol == 13) {
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