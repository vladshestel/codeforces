using System;

namespace Issue_266B
{
    class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine();
            var nums = input.Split();
            
            var count = int.Parse(nums[0]);
            var time = int.Parse(nums[1]);

            input = Console.ReadLine();
            var pupils = input.ToCharArray();

            for (int i = 0; i < time; ++i) {
                var isSwapped = Swap(pupils);

                if (!isSwapped)
                    break;
            }

            Console.WriteLine(pupils);
        }

        private static bool Swap(char[] array) {
            var swapped = false;

            for (int i = 0; i < array.Length - 1; ++i) {
                var needReorder = (array[i] == 'B') && (array[i + 1] == 'G');

                if (needReorder) {
                    swapped = true;
                    array[i] = 'G';
                    array[i + 1] = 'B';
                    i++;
                }
            }

            return swapped;
        }
    }
}