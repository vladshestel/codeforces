using System;
using System.Linq;

namespace Issue_158A
{
    class Program
    {
        static void Main(string[] args) {
        try {
            var input = Console.ReadLine().Split();
            var n = int.Parse(input[0]);
            var k = int.Parse(input[1]);

            input = Console.ReadLine().Split();

            var scores = input.Select(int.Parse).ToArray();
            var count = 0;

            if (scores[k - 1] == 0) {
                var index = k - 1 == 0 ? -1 : FindLastNotZero(scores, k - 1 - 1);
                count = index == -1 ? 0 : index + 1;
            } else {
                var index = k == n ? k - 1 : FindLastEqual(scores, scores[k -1], k);
                count = index == -1 ? k : index + 1;
            }

            Console.WriteLine(count);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        
        private static int FindLastNotZero(int[] array, int bound) {
            return FindByCondition(array, 0, bound, i => i > 0);
        }

        private static int FindLastEqual(int[] array, int val, int bound) {
            return FindByCondition(array, bound, array.Length - 1, i => i == val);
        }
    
        private static int FindByCondition(int[] array, int start, int end, Func<int, bool> eq) {
            var startIndex = start;
            var endIndex = end;
            var lastFound = -1;
            
            if (!eq(array[startIndex])) return -1;
            if (eq(array[endIndex])) return endIndex;
            if (eq(array[startIndex])) lastFound = startIndex;
            
            while (startIndex + 1 < endIndex) {
                var anotherIndex = (endIndex + startIndex) / 2;

                if (eq(array[anotherIndex])) {
                    lastFound = startIndex = anotherIndex;
                } else {
                    endIndex = anotherIndex;
                }
            }
            
            return lastFound;
        }
    }
}