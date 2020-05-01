using System;
using System.Linq;

namespace Issue_1A
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var nums = input.Split(' ').Select(int.Parse).ToArray();
            var result = 
                Math.Ceiling((double) nums[0] / nums[2]) *
                Math.Ceiling((double) nums[1] / nums[2]);
                
            Console.WriteLine((Int64) result);
        }
    }
}