using System;

namespace Issue_131A
{
    class Program
    {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            var result = new char[input.Length];
            var isUppercasedHead = char.IsUpper(input[0]);
            var isUppercasedTail = true;
            
            for (int i = 1; i < input.Length; ++i) {
                if (!char.IsUpper(input[i])) {
                    isUppercasedTail = false;
                    break;
                }
            }

            result[0] = isUppercasedTail ? 
                    isUppercasedHead
                        ? char.ToLower(input[0])
                        : char.ToUpper(input[0]) 
                    : input[0];

            for (int i = 1; i < input.Length; ++i) {
                result[i] = isUppercasedTail 
                    ? char.ToLower(input[i])
                    : input[i];
            }

            Console.WriteLine(result);
        }
    }
}