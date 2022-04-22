using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeCSharp
{
    class Day3
    {

        private static int FindMostCommonBit(List<string> vals, int pos)
        {
            int onesCount = 0;
            int zeroesCount = 0;
            foreach (string s in vals)
            {
                int bit = s[pos] - '0';
                if (bit == 1) 
                    onesCount++;
                else 
                    zeroesCount++;
            }

            if (onesCount >= zeroesCount)
                return 1;
            else 
                return 0;
        }

        private static void FilterVals(List<string> vals, int pos, int targetBit)
        {
            // Console.WriteLine("Filtering vals that don't have bit {0} at pos {1}", targetBit, pos);
            List<string> stringsToRemove = new List<string>();
            for (int i = 0; i < vals.Count; i++)
            {
                string curr = vals[i];
                if ((curr[pos] - '0') != targetBit)
                    stringsToRemove.Add(curr);
            }

            foreach (string s in stringsToRemove)
                vals.Remove(s);
        }

        public static int GetOxygenGeneratorRating(List<string> vals)
        {
            int pos = 0;
            do
            {
                // Console.WriteLine(String.Format("Starting itearation...vals: {0}", string.Join(", ", vals)));
                int mostCommon = FindMostCommonBit(vals, pos);
                FilterVals(vals, pos, mostCommon);
                pos++;
            } while (vals.Count != 1);

            return Util.FromBinary(vals[0]);
        }

        public static int GetCO2ScrubberRating(List<string> vals)
        {
            int pos = 0;
            do
            {
                // Console.WriteLine(String.Format("Starting itearation...vals: {0}", string.Join(", ", vals)));
                int leastCommon = FindMostCommonBit(vals, pos) == 0 ? 1 : 0;
                FilterVals(vals, pos, leastCommon);
                pos++;
            } while (vals.Count != 1);

            return Util.FromBinary(vals[0]);
        }
        public static void Solve()
        {
            string[] vals = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day3\\input1.txt");
            int oxygenGeneratorRating = GetOxygenGeneratorRating(new List<string>(vals));
            Console.WriteLine(String.Format("OxygenGeneratorRating {0}", oxygenGeneratorRating));
            int co2ScrubberRating = GetCO2ScrubberRating(new List<string>(vals));
            Console.WriteLine(String.Format("CO2ScrubberRating {0}", co2ScrubberRating));

        }
    }
}
