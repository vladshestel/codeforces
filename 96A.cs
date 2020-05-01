using System;

namespace Issue_96A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var lastTeam = input[0];
            var maxX = 0;
            var currentX = 0;

            for (int i = 0; i < input.Length; ++i) {
                if (input[i] == lastTeam) {
                    currentX++;
                } else {
                    if (currentX > maxX)
                        maxX = currentX;

                    lastTeam = input[i];
                    currentX = 1;
                }
            }

            if (currentX > maxX)
                maxX = currentX;
                
            Console.WriteLine(maxX >= 7 ? "YES" : "NO");
        }
    }
}