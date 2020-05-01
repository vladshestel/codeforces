using System;

namespace Issue_41A
{
    class Program
    {
        public static void Main(string[] args) {
            var word = Console.ReadLine();
            var translation = Console.ReadLine();

            var result = word.Length == translation.Length && 
                         CheckTranslation(word, translation);

            Console.WriteLine(result ? "YES" : "NO");
        }

        private static bool CheckTranslation(string word, string translation) {
            var l = word.Length - 1;

            for (int i = 0; i < word.Length; ++i) {
                if (word[i] != translation[l - i])
                    return false;
            }

            return true;
        }
    }
}