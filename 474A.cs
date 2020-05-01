    using System;

    namespace Issue_474A
    {
        class Program
        {
            public static void Main(string[] args) {
                const string TopLine = "qwertyuiop";
                const string MidLine = "asdfghjkl;";
                const string BotLine = "zxcvbnm,./";

                var direction =(char) Console.Read(); 
                Console.ReadLine(); // skip redundant symbols
                var message = Console.ReadLine();

                var modifier = direction == 'R' ? -1 : 1;

                for (int i = 0; i < message.Length; ++i) {
                    var symbol = message[i];
                    var topIndex = IndexOf(TopLine, symbol);
                    if (topIndex != -1) {
                        Console.Write(TopLine[topIndex + modifier]);
                        continue;
                    }
                    var midIndex = IndexOf(MidLine, symbol);
                    if (midIndex != -1) {
                        Console.Write(MidLine[midIndex + modifier]);
                        continue;
                    }
                    var botIndex = IndexOf(BotLine, symbol);
                    if (botIndex != -1) {
                        Console.Write(BotLine[botIndex + modifier]);
                        continue;
                    }
                }
            }

            private static int IndexOf(string s, char c) {
                for (int i = 0; i < s.Length; ++i)
                    if (s[i] == c) return i;
                
                return -1;
            }
        }
    }