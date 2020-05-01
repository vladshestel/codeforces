    using System;

    namespace Issue_469A
    {
        class Program
        {
            public static void Main(string[] args) {
                var levels_count = ReadInt();
                var levels = new bool[levels_count];

                for (int players = 0; players < 2; ++players) {
                    var wins = ReadInt();
                    
                    for (int i = 0; i < wins; ++i) {
                        var level = ReadInt() - 1;

                        levels[level] = true;
                    }
                }

                var game_ended = true;

                for (int i = 0; i < levels_count; ++i) {
                    game_ended = game_ended && levels[i];
                }

                Console.WriteLine(game_ended ? "I become the guy." : "Oh, my keyboard!");
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