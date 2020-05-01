using System;

namespace Issue_4A
{
    class Program
    {
        static void Main(string[] args) {
            var w = int.Parse(Console.ReadLine());
            var canSplitWatermelon = (w > 2) && ((w & 1) == 0);
            
            Console.WriteLine(canSplitWatermelon ? "YES" : "NO");
        }
    }
}