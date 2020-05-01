using System;

namespace Issue_133A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var result = false;

            for (int i = 0; i < input.Length; ++i) {
                if (IsPrintableCommand(input[i])) {
                    result = true;
                    break;
                }
            }

            Console.WriteLine(result ? "YES" : "NO");
        }

        private static bool IsPrintableCommand(char c) {
            return c == 'H' || c == 'Q' || c == '9';
        }
    }
}