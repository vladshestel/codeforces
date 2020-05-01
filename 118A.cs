using System;
using System.Text;

namespace Issue_118A
{
    class Program
    {
        static void Main(string[] args) {
            var vowels = new []{ 'a', 'o', 'y', 'e', 'u', 'i' };
            var input = Console.ReadLine();
            var result = new StringBuilder();
            
            for (int i = 0; i < input.Length; ++i) {
                var c = char.ToLower(input[i]);

                if (Array.IndexOf(vowels, c) == -1) {
                    result.Append('.');
                    result.Append(c);
                }
            }

            Console.WriteLine(result.ToString());
        }
    }
}