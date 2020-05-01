using System;

namespace Issue_119A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine().Split();
            var semen = int.Parse(input[0]);
            var antisemen = int.Parse(input[1]);
            var stones = int.Parse(input[2]);
            var lastSemen = false;
            
            while(true) {
                var g = GCD(semen, stones);
                
                if (g > stones) break;
                stones -= g;

                Swap(ref semen, ref antisemen);
                lastSemen = !lastSemen;
            }

            Console.WriteLine(lastSemen ? '0' : '1');
        }

        private static int GCD(int a, int b) {
            int t;

            while (b != 0) {
                t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        private static void Swap(ref int a, ref int b) {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }
}