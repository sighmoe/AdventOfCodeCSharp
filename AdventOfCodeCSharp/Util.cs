using System;

namespace AdventOfCodeCSharp
{
    class Util
    {
        public static string[] ReadLines(String fpath)
        {
            return System.IO.File.ReadAllLines(fpath);
        }

        public static int FromBinary(String bits)
        {
            int acc = 0;
            for (int i = bits.Length-1; i >= 0; i--)
            {
                acc += (bits[i] - '0') * (int) Math.Pow(2, (bits.Length-i-1));
            }
            return acc;
        }
        
        public static int FromBinaryImg(String bits)
        {
            int acc = 0;
            for (int i = bits.Length-1; i >= 0; i--)
            {
                int bit = bits[i] == '.' ? 0 : 1;
                acc += (bit) * (int) Math.Pow(2, (bits.Length-i-1));
            }
            return acc;
        }

        public static ulong FromBinaryUL(String bits)
        {
            ulong acc = 0;
            for (int i = bits.Length-1; i >= 0; i--)
            {
                ulong val = (ulong) bits[i] - '0';
                ulong exponent = (ulong) Math.Pow(2, (bits.Length-i-1));
                acc += (val * exponent); 
            }
            return acc;
        }

        private static string decodeHex(char hex)
        {
            switch (hex) {
                case '0':
                    return "0000";
                case '1':
                    return "0001";
                case '2':
                    return "0010";
                case '3':
                    return "0011";
                case '4':
                    return "0100";
                case '5':
                    return "0101";
                case '6':
                    return "0110";
                case '7':
                    return "0111";
                case '8':
                    return "1000";
                case '9':
                    return "1001";
                case 'A':
                    return "1010";
                case 'B':
                    return "1011";
                case 'C':
                    return "1100";
                case 'D':
                    return "1101";
                case 'E':
                    return "1110";
                case 'F':
                    return "1111";
                default:
                    return "CORRUPTED";
            }
        }

        public static string HexToBinary(String hex)
        {
            string acc = "";
            foreach (char c in hex)
            {
                acc += decodeHex(c);
            }
            return acc;
        }
    }
}
