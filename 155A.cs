    using System;

    namespace Issue_155A
    {
        class Program
        {
            public static void Main(string[] args) {
                var contests_count = ReadInt();
                var score = ReadInt();
                
                var max = score;
                var min = score;
                var count = 0;
                
                for (int i = 1; i < contests_count; ++i) {
                    score = ReadInt();

                    if (score < min) {
                        min = score;
                        count++;
                    } else if (score > max) {
                        max = score;
                        count++;
                    }
                }

                Console.WriteLine(count);
            }

            private static bool IsInputError = false;
            private static bool IsEndOfLine = false;

            private static int ReadInt() {
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