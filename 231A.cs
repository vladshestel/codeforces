using System;
using System.Linq;

namespace Issue_231B
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var issues = int.Parse(input);
            var decisions = 0;

            for (int i = 0; i < issues; ++i) {
            	input = Console.ReadLine();

            	var decision = input.Split()
            		.Select(int.Parse)
            		.Sum(); 

            	if (decision > 1) decisions++;
            }

            Console.WriteLine(decisions);
        }
    }
}