    using System;

    namespace Issue_448A
    {
        class Program
        {
            public static void Main(string[] args) {
                const byte Places = 3;

                var cups = new int[Places];
                var medals = new int[Places];

                for (int i = 0; i < Places; ++i) {
                    cups[i] = ReadInt();
                }

                for (int i = 0; i < Places; ++i) {
                    medals[i] = ReadInt();
                }

                var shelfs_count = ReadInt();

                var required_shelfs = 
                    cups[0] / 5 + 
                    cups[1] / 5 +
                    cups[2] / 5 +
                    (cups[0] % 5 + cups[1] % 5 + cups[2] % 5) / 5 +
                    (((cups[0] % 5 + cups[1] % 5 + cups[2] % 5) % 5) > 0 ? 1 : 0) +
                    medals[0] / 10 +
                    medals[1] / 10 +
                    medals[2] / 10 +
                    (medals[0] % 10 + medals[1] % 10 + medals[2] % 10) / 10 +
                    ((((medals[0] % 10 + medals[1] % 10 + medals[2] % 10) % 10) % 10) > 0 ? 1 : 0);

                Console.WriteLine(required_shelfs <= shelfs_count ? "YES" : "NO");
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