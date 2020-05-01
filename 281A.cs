using System;

namespace Issue_281A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            
            Console.Write(char.ToUpper(input[0]));

            for (int i = 1; i < input.Length; ++i) {
                Console.Write(input[i]);
            }
        }
    }
}