using System;

namespace Issue_271A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var year = int.Parse(input);
            
            do {
                year++;
            } while (!IsBeauty(year));

            Console.WriteLine(year);
        }

        private static bool IsBeauty(int year) {
            var buf = year.ToString();

            for (int i = 0; i < buf.Length - 1; ++i) {
                for (int j = i + 1; j < buf.Length; ++j) {
                    if (buf[i] == buf[j])
                        return false;
                }
            }

            return true;
        }
    }
}