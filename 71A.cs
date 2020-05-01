using System;

namespace Issue_71A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var n = int.Parse(input);
            
            for (int i = 0; i < n; ++i) {
                input = Console.ReadLine();

                var length = input.Length;

                if (length > 10) {
                    input = input[0] + (length - 2).ToString() + input[length - 1];
                }

                Console.WriteLine(input);
            }
        }
    }
}