    using System;

    namespace Issue_466A
    {
        class Program
        {
            public static void Main(string[] args) {
                var required_trips_count = ReadInt();
                var card_trips_count = ReadInt();
                var usual_trip_price = ReadInt();
                var card_price = ReadInt();

                var card_trip_price = (double)card_price / card_trips_count;
                int amount_price;

                if (card_trip_price < usual_trip_price) {
                    amount_price = (required_trips_count / card_trips_count) * card_price 
                                 + (((required_trips_count % card_trips_count) * usual_trip_price < card_price)
                                    ? (required_trips_count % card_trips_count) * usual_trip_price
                                    : card_price);
                } else {
                    amount_price = required_trips_count * usual_trip_price;
                }

                Console.WriteLine(amount_price);
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
    }