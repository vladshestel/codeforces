using System;

namespace Issue_490A
{
    class Program
    {
        public static void Main(string[] args) {
            var students_count = Helper.ReadInt();
            
            var programmers = new Stack();
            var mathematicians = new Stack();
            var idiots = new Stack();

            int student_buffer;

            for (int i = 0; i < students_count; ++i) {
                student_buffer = Helper.ReadInt();

                switch (student_buffer) {
                    case 1 : programmers.Push(i + 1); break;
                    case 2 : mathematicians.Push(i + 1); break;
                    case 3 : idiots.Push(i + 1); break;
                }
            }

            var teams_count = Math.Min(programmers.Count, mathematicians.Count);
            teams_count = Math.Min(teams_count, idiots.Count);

            Console.WriteLine(teams_count);

            for (int i = 0; i < teams_count; ++i) {
                Console.WriteLine("{0} {1} {2}",
                    programmers.Pop(),
                    mathematicians.Pop(),
                    idiots.Pop());
            }
        }
    }

    public class Stack {
        class Node {
            public Node Next;
            public int Value;
        }

        private Node head;
        private int count;

        public Stack() {
            count = 0;
        }

        public void Push(int value) {
            var node = new Node { Value = value };

            if (head == null) {
                head = node;
            } else {
                node.Next = head;
                head = node;
            }

            count++;
        }

        public int Pop() {
            if (head == null) {
                throw new Exception();
            }

            var value = head.Value;
            head = head.Next;
            count--;
            return value;
        }

        public int Count {
            get { return count; }
        }
    }

    public static class Helper {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        public static int ReadInt() {
            const int zeroCode = (int) '0';

            var result = 0;
            var isNegative = false;
            var symbol = Console.Read();
            IsInputError = false;
            IsEndOfLine = false;

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
                    IsEndOfLine = true;
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