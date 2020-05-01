using System;
using System.Linq;

namespace Issue_158B
{
    class Program
    {
        static void Main(string[] args) {
            const int maxWeight = 4; 

            var input = Console.ReadLine();
            var n = int.Parse(input);
            var groups = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var weights = new int[maxWeight];

            for (int i = 0; i < n; ++i) {
                var weight = groups[i];
                weights[weight - 1]++;
            }

            var result = weights[3] // groups with four students
                + weights[2]        // groups with three students
                + weights[1] / 2;   // groups with two students

            var hasLonelyPair = (weights[1] & 1) > 0;
            var leftoverIntroverts = weights[0] - weights[2] - (hasLonelyPair ? 2 : 0);

            result += (hasLonelyPair ? 1 : 0);
    
            if (leftoverIntroverts > 0)
                result += (leftoverIntroverts / 4) + ((leftoverIntroverts % 4) > 0 ? 1 : 0);

            Console.WriteLine(result);
        }
    }
}