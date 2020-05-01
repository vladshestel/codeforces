using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Issue_1B
{
    class Program
    {
        private const int MaxLiteralsCount = 4;
        
        static void Main(string[] args) {
            var n = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < n; ++i) {
                var address = Console.ReadLine();
                var notation = ParseNotation(address);
                var reversed = ReverseNotation(notation);
                Console.WriteLine(string.Join("", reversed));
            }
        }
        
        private static string[] ParseNotation(string s) {
            var result = new List<string>(MaxLiteralsCount);
            var buf = new StringBuilder();
            var isLetterBuf = true;
            var i = 0;
            
            while (i < s.Length) {
               var isLetter = char.IsLetter(s[i]);
               var isLiteralChanged = 
                   (isLetter && !isLetterBuf) || 
                   (!isLetter && isLetterBuf);

               if (isLiteralChanged) {
                   result.Add(buf.ToString());
                   buf.Clear();
                   isLetterBuf = !isLetterBuf;
               }
               
               buf.Append(s[i]);
               i++;
            }
            
            result.Add(buf.ToString());
            
            return result.ToArray();
        }
        
        private static string[] ReverseNotation(string[] s) {
            return s.Length == 2 
                ? ToRXCYNotation(s)
                : ToXYNotation(s);
        }
        
        private static string[] ToRXCYNotation(string[] xy) {
            return new [] { "R", int.Parse(xy[1]).ToString(), "C", AliasToNum(xy[0]) };
        }

        private static string[] ToXYNotation(string[] rxcy) {
            return new [] { NumToAlias(rxcy[3]), int.Parse(rxcy[1]).ToString() };
        }

        private static string AliasToNum(string alias) {
            var buffer = 0;

            for (int i = 0; i < alias.Length; ++i) {
                buffer *= 26;
                buffer += (alias[i] - 'A' + 1);
            }

            return buffer.ToString();
        }

        private static string NumToAlias(string s) {
            var sb = new StringBuilder();
            var num = int.Parse(s);
            
            while(num > 0) {
                var symbolNum = (num - 1) % 26;
                sb.Insert(0, Convert.ToChar('A' + (char)symbolNum).ToString());
                num = (num - symbolNum) / 26;
            }

            return sb.ToString();
        }
    }
}