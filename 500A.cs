using System;

namespace Issue_500A
{
    class Program
    {
        public static void Main(string[] args) {
            var cells_count = ReadInt();
            var destination = ReadInt();
            var cells = new int[cells_count - 1];

            for (int i = 0; i < cells_count - 1; ++i) {
                cells[i] = ReadInt();
            }
            
            var current_cell = 0;

            while (true) {
                if ((current_cell + 1) == destination) {
                    Console.WriteLine("YES");
                    return;
                }
                if ((current_cell + 1) > destination) {
                    Console.WriteLine("NO");
                    return;
                }

                current_cell += cells[current_cell];
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