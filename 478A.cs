using System;

namespace Issue_478A
{
    class Program
    {
        public static void Main(string[] args) {
            const int CoinsCount = 5;
            
            var coins = new int[CoinsCount];
            
            for (int i = 0; i < CoinsCount; ++i) {
                coins[i] = ReadInt();
            }

            var balanced = IsBalanced(coins);

            while (!balanced) {
                var max = coins.MaxIndex();
                var min = coins.MinIndex();

                if (coins[max] - coins[min] == 1) {
                    break;
                }

                coins[max]--;
                coins[min]++;

                balanced = IsBalanced(coins);
            }

            Console.WriteLine((balanced && (coins[0] > 0)) ? coins[0] : -1);
        }

        public static bool IsBalanced(int[] array) {
            if (array.Length == 0) 
                return true;

            var value = array[0];

            for (int i = 1; i < array.Length; ++i) {
                if (array[i] != value) {
                    return false;
                }
            }

            return true;
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

    public static class Extensions 
    {
        public static int MaxIndex(this int[] array) {
            if (array.Length == 0)
                return -1;

            var value = array[0];
            var index = 0;

            for (int i = 1; i < array.Length; ++i) {
                if (array[i] > value) {
                    value = array[i];
                    index = i;
                }
            }

            return index;
        }

        public static int MinIndex(this int[] array) {
            if (array.Length == 0)
                return -1;

            var value = array[0];
            var index = 0;

            for (int i = 1; i < array.Length; ++i) {
                if (array[i] < value) {
                    value = array[i];
                    index = i;
                }
            }

            return index;
        }

        public static void Print(this int[] array) {
            for (int i = 0; i < array.Length; ++i) {
                Console.Write("{0} ", array[i]);
            }

            Console.Write(Environment.NewLine);
        }
    }
}