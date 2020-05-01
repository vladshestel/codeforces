using System;

namespace Issue_208A
{
    class Program
    {
        public static void Main(string[] args) {
            var symbol = 0;
            var buffer = new int[256];
            var buffer_pointer = 0;
            var wuber = 0;

            while (
                (symbol != '\r') && 
                (symbol != '\n') && 
                (symbol != -1)) 
            {
                symbol = Console.Read();

                buffer[buffer_pointer++] = symbol;

                if (symbol == 'W') {
                    wuber = 1;
                } else if ((symbol == 'U') && (wuber == 1)) {
                    wuber++;
                } else if ((symbol == 'B') && (wuber == 2)) {
                    if (FlushBuffer(buffer, buffer_pointer - 3)) {
                        Console.Write(' ');
                    }

                    wuber = 0;
                    buffer_pointer = 0;
                } else {
                    wuber = 0;
                }
            }

            FlushBuffer(buffer, buffer_pointer);
        }

        private static bool FlushBuffer(int[] buffer, int length) {
            var result = false;

            for (int i = 0; i < length; ++i) {
                Console.Write((char)buffer[i]);
                result = true;
            }

            return result;
        }
    }
}