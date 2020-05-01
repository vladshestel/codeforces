using System;

namespace Issue_733A
{
    public class Program
    {
        public static void Main(string[] args) {
            var allowed = new char[] { 'A', 'E', 'I', 'O', 'U', 'Y' };
            var line = Console.ReadLine();
            
            var max = 0;
            var current = 1;

            for (var i = 0; i < line.Length; ++i) {
                var symbol = line[i];

                if (Contains(allowed, symbol)) {
                    if (current > max)
                        max = current;
                    current = 1;
                } else {
                    current++;
                }
            }

            if (current > max)
                max = current;

            Console.WriteLine(max);
        }

        private static bool Contains(char[] allowed, char symbol) {
            for (var i = 0; i < allowed.Length; ++i) {
                if (allowed[i] == symbol)
                    return true;
            }

            return false;
        }
    }
}