using System;

namespace Issue_236A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var symbols = new int[26];
            var count = 0;

            for (int i = 0; i < input.Length; ++i) {
                var code = input[i] - 'a';
                
                if (symbols[code] == 0)
                    count++; 

                symbols[code]++;
            }

            Console.WriteLine((count & 1) == 0 ? "CHAT WITH HER!" : "IGNORE HIM!");
        }
    }
}