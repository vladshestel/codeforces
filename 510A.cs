    using System;

    namespace Issue_469A
    {
        class Program
        {
            public static void Main(string[] args) {
                var height = ReadInt();
                var width = ReadInt();
                
                var map = new Map(height, width);
                map.DrawSnake();
                map.Print();
            }

            private static bool IsInputError = false;
            private static bool IsEndOfLine = false;

            private static int ReadInt() {
                const int zeroCode = (int) '0';

                var result = 0;
                var isNegative = false;
                var symbol = Console.Read();
                IsInputError = false;
                IsEndOfLine = false;

                while (symbol == ' ') {
                    symbol = Console.Read();
                }

                if (symbol == '-') {
                    isNegative = true;
                    symbol = Console.Read();
                }

                for ( ;
                    (symbol != -1) && (symbol != ' ');
                    symbol = Console.Read()
                ) {
                    var digit = symbol - zeroCode;
                    
                    // if symbol == \n
                    if (symbol == 13) {
                        // skip next \r symbol
                        Console.Read();
                        IsEndOfLine = true;
                        break;
                    }

                    if (digit < 10 && digit >= 0) {
                        var cache1 = result << 1;
                        var cache2 = cache1 << 2;
                        result = cache1 + cache2 + digit;
                    } else {
                        IsInputError = true;
                        break;
                    }
                }

                return isNegative ? ~result + 1 : result;
            }
        }

        class Map
        {
            private readonly char[,] _map;
            private readonly int _height;
            private readonly int _width;

            private int _x;
            private int _y;
            private int _xDirection;

            public Map(int height, int width) {
                _map = new char[height, width];

                _height = height;
                _width = width;

                _x = _y = 0;
                _xDirection = 1;
            }

            public void DrawSnake() {
                while (_x < _height) {
                    this.GoByDirection();
                    this.GoDown();
                }
            }

            public void Print() {
                for (int i = 0; i < _height; ++i) {
                    for (int j = 0; j < _width; ++j) {
                        Console.Write(GetTrack(i, j));
                    }

                    Console.WriteLine();
                }
            }

            private char GetTrack(int i, int j) {
                return _map[i, j] != 0 ? '#' : '.';
            }

            private void GoDown() {
                _x++;
                if (_x < _height) {
                    FillCurrentPosition();
                }
                _x++;
            }

            private void GoByDirection() {
                if (_xDirection > 0) {
                    MoveRight();
                } else {
                    MoveLeft();
                }

                _xDirection = -_xDirection;
            }

            private void MoveRight() {
                Move(() => _y < _width);
                _y--;
            }

            private void MoveLeft() {
                Move(() => _y >= 0);
                _y++;
            }

            private void Move(Func<bool> bounder) {
                while (bounder()) {
                    FillCurrentPosition();
                    _y += _xDirection;
                }   
            }

            private void FillCurrentPosition() {
                _map[_x, _y] = '#';
            }
        }
    }