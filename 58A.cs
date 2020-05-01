using System;

namespace Issue_58A
{
    class Program
    {
        public static void Main(string[] args) {
            var wordToFind = "hello";
            var index = 0;
            
            for (var input = Console.Read(); input != -1; input = Console.Read()) {
                if (index < wordToFind.Length && wordToFind[index] == input)
                    index++;
            }
            
            Console.WriteLine(index == wordToFind.Length ? "YES" : "NO");
        }
    }
}