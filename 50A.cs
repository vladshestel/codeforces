using System;

namespace Issue_50A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine().Split();
            var m = int.Parse(input[0]);
            var n = int.Parse(input[1]);
            
            if (n > m) {
                Swap(ref n, ref m);
            }

            var horizontal = m / 2;
            var vertical = n / 2;
            var result = (horizontal * n) + (m % 2 > 0 ? vertical : 0);

            Console.WriteLine(result);
        }

        private static void Swap(ref int a, ref int b) {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }
}