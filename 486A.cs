using System;

namespace Issue_486A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var num = long.Parse(input);

            var even = (num & 1) == 0;
            var fN = num / 2 + (even ? 0 : 1);

            fN = even ? fN : (~fN + 1);

            Console.WriteLine(fN);
        }
    }
}