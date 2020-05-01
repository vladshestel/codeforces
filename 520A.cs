using System;

namespace Issue_520A
{
    class Program
    {
        public static void Main(string[] args) {
            const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
                                    
            var count = ReadInt();

            if (count < Alphabet.Length) {
                Console.WriteLine("NO");
                return;
            }

            var input = Console.ReadLine();
            var flags = new bool[Alphabet.Length];

            for (int i = 0; i < input.Length; ++i) {
                var index = IndexOf(Alphabet, input[i]);

                if (index != -1)
                    flags[index] = true;
            }

            var result = true;

            for (int i = 0; i < Alphabet.Length; ++i) {
                result = result && flags[i];
            }

            Console.WriteLine(result ? "YES" : "NO");
        }

        private static int IndexOf(string s, char symbol) {
            var index = -1;

            for (int i = 0; i < s.Length; ++i) {
                if (char.ToLower(s[i]) == char.ToLower(symbol)) {
                    index = i;
                    break;
                }
            }

            return index;
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