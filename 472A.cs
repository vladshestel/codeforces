using System;

namespace Issue_472A
{
    class Program
    {
        public static void Main(string[] args) {
            const int MinimalCompositeNumber = 4;

            var input = Console.ReadLine();
            var number = int.Parse(input);
            
            for (int i = MinimalCompositeNumber; i < number; ++i) {
                if (!IsPrimeNumber(i)) {
                    if (!IsPrimeNumber(number - i)) {
                        Console.WriteLine("{0} {1}", i, number - i);
                        return;
                    }
                }
            }
        }

        private static bool IsPrimeNumber(int number) {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if ((number & 1) == 0) return false;

            var boundary = Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2) {
                if ((number % i) == 0)
                    return false;
            }

            return true;
        }
    }
}