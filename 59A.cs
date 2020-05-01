using System;

namespace Issue_59A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();

            var lowers_count = 0;
            var uppers_count = 0;

            for (int i = 0; i < input.Length; ++i) {
                if (char.IsUpper(input[i]))
                    uppers_count++;
                else
                    lowers_count++;
            }

            Func<char, char> to_lower = char.ToLower;
            Func<char, char> to_upper = char.ToUpper;

            PrintTransformed(input, lowers_count >= uppers_count ? to_lower : to_upper);
        }

        private static void PrintTransformed(string word, Func<char, char> transform) {
            for (int i = 0; i < word.Length; ++i)
                Console.Write(transform(word[i]));
        }
    }
}