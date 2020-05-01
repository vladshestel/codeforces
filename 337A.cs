    using System;

    namespace Issue_337A
    {
        class Program
        {
            public static void Main(string[] args) {
                var students_count = ReadInt();
                var puzzles_count = ReadInt();
                var puzzles = new int[puzzles_count];

                for (int i = 0; i < puzzles_count; ++i) {
                    puzzles[i] = ReadInt();
                }

                Sort(puzzles);

                var minimum = int.MaxValue;

                for (int i = 0; i + students_count - 1 < puzzles_count; ++i) {
                    var f = Math.Abs(puzzles[i] - puzzles[i + students_count - 1]);
                    
                    if (f < minimum) {
                        minimum = f;
                    }
                }

                Console.WriteLine(minimum);
            }

            private static void Sort(int[] array) {
                for (int i = array.Length / 2 - 1; i >= 0; --i) {
                    ShiftDown(array, i, array.Length);
                }

                for (int i = array.Length - 1; i >= 1; --i) {
                    Swap(ref array[0], ref array[i]);
                    ShiftDown(array, 0, i);
                }
            }

            private static void Swap(ref int a, ref int b) {
                a = a ^ b;
                b = a ^ b;
                a = a ^ b;
            }

            private static void ShiftDown(int[] array, int i, int j) {
                var done = false;
                var maxChild = 0;

                while ((i * 2 + 1 < j) && (!done)) {
                    if (i * 2 + 1 == j - 1)
                        maxChild = i * 2 + 1; 
                    else if (array[i * 2 + 1] > array[i * 2 + 2])
                        maxChild = i * 2 + 1;
                    else
                        maxChild = i * 2 + 2;

                    if (array[i] < array[maxChild]) {
                        Swap(ref array[i], ref array[maxChild]);
                        i = maxChild;
                    } else {
                        done = true;
                    }
                }
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