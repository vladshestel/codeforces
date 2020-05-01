using System;

namespace Issue_141A
{
    class Program
    {
        public static void Main(string[] args) {
            var first = Console.ReadLine();
            var second = Console.ReadLine();
            var letters = Console.ReadLine();

            var letters_count = letters.Length;
            var flags = new bool[letters_count];

            var isAllRequiredLetters = 
                MarkWord(letters, flags, first) && 
                MarkWord(letters, flags, second);
            
            var noRedundantLetters = true;
            
            for (int i = 0; i < flags.Length; ++i) {
                noRedundantLetters = noRedundantLetters && flags[i];
            }

            Console.WriteLine(
                isAllRequiredLetters && noRedundantLetters 
                ? "YES" 
                : "NO");
        }

        private static bool MarkWord(string letters, bool[] flags, string word) {
            for (int i = 0; i < word.Length; ++i) {
                if (!MarkLetter(letters, flags, word[i])) {
                    return false;
                }
            }

            return true;
        }

        private static bool MarkLetter(string letters, bool[] flags, char letter) {
            for (int i = 0; i < letters.Length; ++i) {
                if ((letters[i] == letter) && (!flags[i])) {
                    flags[i] = true;
                    return true;
                }
            }

            return false;
        }
    }
}