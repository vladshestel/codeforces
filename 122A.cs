using System;

namespace Issue_122A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var number = int.Parse(input);
            var isAlmostHappy = false;
            var delimeter = 4;

            while(delimeter <= number) {
                if ((number % delimeter) == 0) {
                    isAlmostHappy = true;
                    break;
                }

                delimeter = NextHappyNumber(delimeter);
            }

            Console.WriteLine(isAlmostHappy ? "YES" : "NO");
        }

        private static int NextHappyNumber(int number) {
            var result = number + 1;

            while (!IsHappy(result)) {
                result++;
            }

            return result;
        }

        private static bool IsHappy(int number) {
            var buf = number.ToString();

            for (int i = 0; i < buf.Length; ++i) {
                if (buf[i] != '4' && buf[i] != '7')
                    return false;
            }

            return true;
        }
    }
}