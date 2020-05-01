using System;

namespace Issue_82A
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var iterations = int.Parse(input);

            var queue = new [] { "Sheldon", "Leonard", "Penny", "Rajesh", "Howard" };

            var queueSize = queue.Length;
            var doubles = 1;

            while(queueSize < iterations) {
                iterations -= queueSize;
                queueSize = queueSize << 1;
                doubles = doubles << 1;
            }

            var index = (iterations - 1) / doubles;

            Console.WriteLine(queue[index]);
        }
    }
}