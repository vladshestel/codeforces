using System;

namespace Issue_61A
{
    class Program
    {
        public static void Main(string[] args) {
            var first = Console.ReadLine();
            var second = Console.ReadLine();

            for (int i = 0; i < first.Length; ++i) {
                var c = first[i] ^ second[i];
                Console.Write(c);
            }
        }
    }
}