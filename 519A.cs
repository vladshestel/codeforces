using System;

namespace Issue_519A
{
    class Program
    {
        public static void Main(string[] args) {
            var white = 0;
            var black = 0;

            for (int i = 0; i < 8; ++i) {
                var input = Console.ReadLine();

                for (int j = 0; j < 8; ++j) {
                    var symbol = input[j];
                    var weight = GetWeight(symbol);
                    
                    if (char.IsLower(symbol)) {
                        black += weight;
                    } else {
                        white += weight;
                    }
                }
            }

            Console.WriteLine(
                white == black ? "Draw"
                    : white > black 
                        ? "White"
                        : "Black");
        }

        private static int GetWeight(char figure) {
            var f = char.ToUpper(figure);

            switch (f) {
                case 'Q' : return 9;
                case 'R' : return 5;
                case 'B' : return 3;
                case 'N' : return 3;
                case 'P' : return 1;
                case 'K' :
                case '.' : return 0;
                default : {
                    Console.WriteLine(f);
                    throw new Exception();
                }
            }
        }
    }
}