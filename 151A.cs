using System;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main( )
        {
            string input = Console.ReadLine();
            string[] separ = new string[] {" "};
            string[] resulting = input.Split(separ, StringSplitOptions.None);
            uint[] mas = new uint[8];
            for (int i = 0; i < 8; i++)
            {
                mas[i] = uint.Parse(resulting[i]);
            }
            Console.WriteLine(Math.Min(Math.Min(mas[1]*mas[2]/mas[6],mas[3]*mas[4]), mas[5]/mas[7])/mas[0]);
            Console.ReadLine();
        }
    }
}