using System;
using System.IO;

namespace Issue_230B
{
    internal class List {
        private readonly int[] _numbers;
        private int _count;

        public List(int count) {
            _numbers = new int[count];
            _count = 0;
        }

        public int Count {
            get { return _count; }
        }

        public int[] Numbers { 
            get { return _numbers; }
        }

        public void Add(int number) {
            _numbers[_count++] = number;
        }
    }

    public class Program
    {
        public static void Main(string[] args) {
            var primes = PrecalculatePrimeNumbers();
            var n = IO.Read();

            for (var i = 0; i < n; ++i) {
                var number = IO.ReadLong();

                Console.WriteLine(IsTPrime(primes, number) ? "YES" : "NO");
            }
        }

        private static List PrecalculatePrimeNumbers() {
            const int count = 1000000;
            
            var primes = new List(78500);
            primes.Add(2);
            var numbers = new byte[count];
            
            for (var i = 3; i < numbers.Length; i += 2) {
                if (numbers[i - 1] == 0) {
                    primes.Add(i);
                    
                    for (var j = (i * 1L * i); j < numbers.Length; j += i) {
                        numbers[j - 1] = 1;
                    }
                }
            }

            return primes;
        }

        private static bool IsTPrime(List primes, long number) {
            if ((number & 1) == 0) {
                return number == (2 * 2);
            }
            if (number % 3 == 0) {
                return number == (3 * 3);
            }
            if (number % 5 == 0) {
                return number == (5 * 5);
            }
            var numbers = primes.Numbers;
            var lastElement = numbers[primes.Count - 1];
            if (number > lastElement * 1L * lastElement) {
                return false;
            }

            var bottom = 0;
            var upper = primes.Count;

            while (bottom < upper) {
                var currentIndex = (upper - bottom) / 2 + bottom;
                var element = numbers[currentIndex];
                var root = (element * 1L * element);

                if (number == root) {
                    return true;
                } else if (root > number) {
                    upper = currentIndex;
                } else {
                    bottom = currentIndex + 1;
                }
            }
            
            return false;
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

        public static long ReadLong() {
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

            long result = 0;
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