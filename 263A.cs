using System;

namespace Issue_263A
{
    class Program
    {
        public static void Main(string[] args) {
            var row = 0;
            var column = 0;

            for ( ; row < 5; row++) {
                var input = Console.ReadLine();
                var index = IndexOf(input);

                if (index != -1) {
                    column = index;
                    break;
                }
            }
            
            Console.WriteLine(Math.Abs(2 - row) + Math.Abs(2 - column));
        }

        private static int IndexOf(string input) {
            var index = 0;

            for (int i = 0; i < input.Length; ++i) {
                if (input[i] == '1')
                    return index;
                if (input[i] == ' ')
                    index++;
            }

            return -1;
        }
    }
}