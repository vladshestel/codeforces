using System;
using System.IO;
using System.Collections.Generic;

namespace Issue_327A
{
    class Program
    {
        public static void Main(string[] args) {
            var numbers_count = IO.Read();
            var groups = ReadGroups(numbers_count);
            
            Console.WriteLine(CountOnes(groups));
        }

        private static int CountOnes(List<Pair> groups) {
            var isZerosGroupsFirst = groups[0].Value == 0;
            
            if (groups.Count == 1) {
                if (isZerosGroupsFirst)
                    return groups[0].Count;
                else 
                    return groups[0].Count - 1;
            }

            var index = isZerosGroupsFirst ? 0 : 1;
            var max_zeros = 0;

            int max_index_start = 0;
            int max_index_end = 0;

            while (index < groups.Count) {
                if (groups[index].Count > max_zeros) {
                    max_index_start = max_index_end = index;
                    max_zeros = groups[index].Count;
                }

                if (IsGroupGoal(groups, index)) {
                    var zeros = groups[index].Count + groups[index + 2].Count;
                    var ones  = groups[index + 1].Count;
                    var goal  = zeros - ones;
                    var max_goal  = goal;
                    var max_goal_index = index + 2;

                    // check if A + C + E is still more than lost of ones in [... A, B, C, D, E ...]
                    for (int i = index + 3; i < groups.Count; i += 2) {
                        if (groups.Count <= (i + 1))
                            break;
                        
                        var delta = groups[i + 1].Count - groups[i].Count;
                        // if no goal -> just stop
                        if (goal + delta < 0)
                            break;

                        goal += delta;

                        if (goal > max_goal) {
                            max_goal = goal;
                            max_goal_index = i + 1;
                        }
                    }

                    if (max_goal > max_zeros) {
                        max_zeros = max_goal;
                        max_index_start = index;
                        max_index_end = max_goal_index;
                    }
                }

                index += 2;
            }

            var amount = 0;

            for (int i = 0; i < max_index_start; ++i) {
                if (groups[i].Value == 1)
                    amount += groups[i].Count;
            }

            for (int i = max_index_start; i <= max_index_end; i += 2) {
                amount += groups[i].Count;
            }

            for (int i = max_index_end + 1; i < groups.Count; ++i) {
                if (groups[i].Value == 1)
                    amount += groups[i].Count;
            }

            return amount;
        }

        // checks that inveted count of two nearest groups of zeroz more then lost ones
        private static bool IsGroupGoal(List<Pair> groups, int index) {
            return (groups.Count > index + 2) && 
                // if A + C group of zeros > B group of 1 in [... A, B, C ...]
                (groups[index].Count + groups[index + 2].Count) > (groups[index + 1].Count);
        }

        private static List<Pair> ReadGroups(int numbers_count) {
            var value  = IO.Read();
            var groups = new List<Pair>();
            var group  = new Pair { 
                Value  = value,
                Count  = 1
            };

            for (int i = 1; i < numbers_count; ++i) {
                value = IO.Read();

                if (value == group.Value) {
                    group.Count++;
                } else {
                    groups.Add(group);
                    group = new Pair {
                        Value = value,
                        Count = 1
                    };
                }
            }

            // add last group
            groups.Add(group);

            return groups;
        }
    }

    public struct Pair {
        public int Value;
        public int Count;
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