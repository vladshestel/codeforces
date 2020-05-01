using System;

namespace Issue_112A
{
    class Program
    {
        public static void Main(string[] args) {
            var first = Console.ReadLine();
            var second = Console.ReadLine();

            var result = StrCmp(first, second);

            Console.WriteLine(result);
        }
        
        private static int StrCmp(string a, string b){
            for (int i = 0; i < a.Length; ++i) {
                var lowA = char.ToLower(a[i]);
                var lowB = char.ToLower(b[i]);
                
                if (lowA != lowB) {
                    return lowA > lowB ? 1 : -1;
                }
            }

            return 0;
        }
    }
}