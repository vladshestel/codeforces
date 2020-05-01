using System;
using System.IO;
using System.Numerics;

namespace Issue_489C
{
    public class Program
    {
        private static readonly byte[] NoResult = new byte[0];

        public static void Main(string[] args) {
            var m = IO.Read();
            var s = IO.Read();

            if (!IsBoundaryValues(m, s)) {
                var min = GetMinimal(m, s);
                if (min == NoResult) {
                    Console.WriteLine("-1 -1");
                } else {
                    var max = GetMaximum(m, s);
                    Write(min);
                    Console.Write(' ');
                    Write(max);
                }
            }
        }

        private static bool IsBoundaryValues(int m, int s) {
            if (s == 0) {
                if (m == 1) {
                    Console.WriteLine("0 0");
                    return true;
                } else {
                    Console.WriteLine("-1 -1");
                    return true;
                }
            }

            return false;
        }

        private static byte[] GetMinimal(int m, int s) {
            var digits = new byte[m];
            var sum = 1;
            var pointer = m - 1;
            digits[0] = 1;
            
            while ((sum < s) && (digits[0] < 9)) {
                if (digits[pointer] == 9) {
                    pointer--;
                }
                digits[pointer]++;
                sum++;
            }

            return sum == s ? digits : NoResult;
        }

        private static byte[] GetMaximum(int m, int s) {
            var digits = new byte[m];
            var sum = 9 * m;
            var pointer = m - 1;

            for (var i = 0; i < m; ++i)
                digits[i] = 9;
            
            while ((sum > s) && (digits[0] > 1)) {
                if (digits[pointer] == 0) {
                    pointer--;
                }
                digits[pointer]--;
                sum--;
            }

            return sum == s ? digits : NoResult;
        }

        private static void Write(byte[] digits) {
            for (var i = 0; i < digits.Length; ++i) {
                Console.Write((char)(digits[i] + IO.ZeroCode));
            }
        }
    }

    public static class IO {
        public const int ZeroCode = (int) '0';

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(int)),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }

        public static StreamReader Reader { 
            get; set;
        }

        public static string ReadLine() {
            return Reader.ReadLine();
        }

        public static int Read() {
            var reader = Reader;
            var symbol = reader.Read();

            while (symbol == ' ') {
                symbol = reader.Read();
            }

            var isNegative = false;
            if (symbol == '-') {
                isNegative = true;
                symbol = reader.Read();
            }

            int result = 0;
            for ( ;
                (symbol != -1) && (symbol != ' ');
                symbol = reader.Read()
            ) {
                var digit = symbol - ZeroCode;
                
                if (digit < 10 && digit >= 0) {
                    result = (result << 1) + ((result << 1) << 2) + digit;
                } else {
                    if (symbol == 13)         // if symbol == \n
                        reader.Read();        // skip next \r symbol
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }

        public static char ReadChar() {
            var symbol = Reader.Read();

            while (symbol == ' ') {
                symbol = Reader.Read();
            }

            if (symbol == 13) {        // if symbol == \n
                Reader.Read();         // skip next \r symbol
                symbol = Reader.Read();
            }

            return (char) symbol;
        }
    }
}