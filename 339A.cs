using System;
using System.Text;

namespace Issue_339A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var summands = input.Split('+');

            Array.Sort(summands);

            var builder = new StringBuilder();

            for (int i = 0; i < summands.Length - 1; ++i) {
                builder.Append(summands[i].ToString() + "+");
            }

            builder.Append(summands[summands.Length - 1]);

            Console.WriteLine(builder.ToString());
        }
    }
}