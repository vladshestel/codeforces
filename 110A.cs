using System;

namespace Issue_110A
{
    class Program
    {
        public static void Main(string[] args) {
            var count = 0;
            
            for (int input = Console.Read(); input != -1; input = Console.Read()) {
                if (input == (int)'4' || input == (int)'7')
                    count++;
            }

            var result = true;
            var str = count.ToString();

            for (var i = 0; i < str.Length; ++i) {
                if (str[i] != '4' && str[i] != '7') {
                    result = false;
                    break;
                }
            }

            Console.WriteLine(result ? "YES" : "NO");
        }
    }
}