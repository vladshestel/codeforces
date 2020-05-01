using System;
using System.Collections.Generic;
using System.IO;

// General solve
namespace Issue_711A
{
    class Program
    {
        private const char EmptyChair = 'O';
        private const char Delimeter = '|';

        public static void Main(string[] args) {
            var n = IO.Read();

            var tiers = new List<string>(n + 1);
            var onFirst = false;
            var tierNumber = -1;
            
            for (int i = 0; i < n; ++i) {
                var tier = IO.ReadLine();
                tiers.Add(tier);

                if (tierNumber == -1) {
                    if (IsFirstPairEmpty(tier)) {
                        onFirst = true;
                        tierNumber = i;
                    } else if (IsSecondPairEmpty(tier)) {
                        onFirst = false;
                        tierNumber = i;
                    }
                }
            }

            if (tierNumber != -1) {
                Console.WriteLine("YES");

                for (var i = 0; i < n; ++i) {
                    var tier = tiers[i];

                    if (i == tierNumber) {
                        Console.WriteLine(onFirst 
                            ? "++|" + tier[3] + tier[4]
                            : "" + tier[0] + tier[1] + "|++");
                    } else {
                        Console.WriteLine(tier);
                    }
                }
            } else {
                Console.WriteLine("NO");
            }
        }

        private static bool IsFirstPairEmpty(string tier) {
            return (tier[0] == EmptyChair && tier[1] == EmptyChair);
        }

        private static bool IsSecondPairEmpty(string tier) {
            return (tier[3] == EmptyChair && tier[4] == EmptyChair);
        }
    }

    public static class IO {
        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(int)),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }

        public static StreamReader Reader { get; set; }

        public static string ReadLine() {
            return Reader.ReadLine();
        }

        public static int Read() {
            const int zeroCode = (int) '0';

            int result = 0;
            int digit;
            int cache1;
            int cache2;

            var isNegative = false;
            var symbol = Reader.Read();

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == '-') {
                isNegative = true;
                symbol = Reader.Read();
            }

            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = Reader.Read()
            ) {
                digit = symbol - zeroCode;
                
                if (digit < 10 && digit >= 0) {
                    cache1 = result << 1;
                    cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        Reader.Read();          // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}