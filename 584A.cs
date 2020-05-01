using System;
using System.IO;

namespace Issue_584A
{
    class Program
    {
        public static void Main(string[] args) {
            const string ImpossibleConditionResult = "-1";

            var preferred_length = IO.Read();
            var preferred_divider = IO.Read();

            var isImpossibleCondition =
                preferred_length == 1 && 
                preferred_divider == 10;

            string result;

            if (isImpossibleCondition) {
                result = ImpossibleConditionResult;
            } else {
                var sign = preferred_divider.ToString()[0];
    
                result = (preferred_divider < 10 
                    ? GetByLength(preferred_length, sign, sign)
                    : GetByLength(preferred_length, sign, '0'));
            }

            Console.WriteLine(result);
        }

        private static string GetByLength(int length, char firstSign, char fillSign) {
            var result = new StringBuilder(length);

            result.Add(firstSign);

            for (int i = 1; i < length; ++i)
                result.Add(fillSign);

            return result.ToString();
        }
    }

    public class StringBuilder {
        private const int DefaultBufferSize = 256;

        private char[] _buffer;
        private int _pointer;

        public StringBuilder() 
            : this(DefaultBufferSize) { }

        public StringBuilder(int bufferSize) {
            _buffer = new char[bufferSize];
            _pointer = 0;
        }

        public int Size {
            get { return _buffer.Length; }
        }

        public void Add(char symbol) {
            if (_pointer < Size) {
                _buffer[_pointer++] = symbol;
            } else {
                Array.Resize(ref _buffer, Size * 2);
            }
        }

        public override string ToString() {
            return new string(_buffer);
        }
    }

    public static class IO {
        public static bool IsInputError = false;
        public static bool IsEndOfLine = false;

        static IO() {
            Reader = new StreamReader(
                Console.OpenStandardInput(sizeof(int)),
                System.Text.Encoding.ASCII,
                false, 
                sizeof(int),
                false);
        }

        public static StreamReader Reader { get; set; }

        public static int Read() {
            const int zeroCode = (int) '0';

            int result = 0;
            int digit;
            int cache1;
            int cache2;

            var isNegative = false;
            var symbol = Reader.Read();
            IsInputError = false;
            IsEndOfLine = false;

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
                
                // if symbol == \n
                if (symbol == 13) {
                    // skip next \r symbol
                    Reader.Read();
                    IsEndOfLine = true;
                    break;
                }

                if (digit < 10 && digit >= 0) {
                    cache1 = result << 1;
                    cache2 = cache1 << 2;
                    result = cache1 + cache2 + digit;
                } else {
                    IsInputError = true;
                    break;
                }
            }

            return isNegative ? ~result + 1 : result;
        }
    }
}