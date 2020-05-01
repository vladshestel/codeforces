using System;
using System.IO;

namespace Issue_4C
{
    public class HashTable {
        private class Node {
            public Node(string key, int value) {
                Key = key;
                Value = value;
            }

            public string Key;
            public int Value;
        }

        private readonly int _size;
        private readonly Node[] _nodes;

        public HashTable(int size) {
            _size = size;
            _nodes = new Node[size];
        }

        public int AddOrUpdate(string item) {
            for (var i = 0; i < _size; ++i) {
                var node = _nodes[i];
                if (node == null) {
                    _nodes[i] = new Node(item, 1);
                    return 0;
                }
                if (node.Key == item) {
                    return node.Value++;
                }
            }

            return -1;
        }
    }

    public class Program
    {
        public static void Main(string[] args) {
            var count = IO.Read();
            var hashtable = new HashTable(count);

            for (var i = 0; i < count; ++i) {
                var name = IO.ReadLine();
                var number = hashtable.AddOrUpdate(name);
                if (number == 0) {
                    Console.WriteLine("OK");
                } else {
                    Console.WriteLine(name + number);
                }
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

        public static string ReadLine() {
            return Reader.ReadLine();
        }

        public static int Read() {
            const int zeroCode = (int) '0';

            int result = 0;
            int digit;
            int cache1;
            int cache2;

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
                digit = symbol - zeroCode;
                
                if (digit < 10 && digit >= 0) {
                    cache1 = result << 1;
                    cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        Reader.Read();        // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }

        public static char ReadChar() {
            var symbol = Reader.Read();

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == 13) {        // if symbol == \n
                Reader.Read();         // skip next \r symbol
                symbol = Reader.Read();
            }

            return (char) symbol;
        }
    }
}