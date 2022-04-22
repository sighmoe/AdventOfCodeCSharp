using System;
using System.Linq;

namespace AdventOfCodeCSharp
{
    class Day17
    {

        private static (int,int,int,int) ParseInput(string input)
        {
            string relevant = input[13..];
            string[] split = relevant.Split(", ");

            // x coords
            string[] xStr = split[0][2..].Split("..");
            int[] xs = xStr.Select(x => Int32.Parse(x)).ToArray();

            // y coords
            string[] yStr = split[1][2..].Split("..");
            int[] ys = yStr.Select(y => Int32.Parse(y)).ToArray();

            Console.WriteLine("xs: {0}..{1}", xs[0], xs[1]);
            Console.WriteLine("ys: {0}..{1}", ys[0], ys[1]);

            return (xs[0], xs[1], ys[0], ys[1]);
        }

        private static (bool,int) CheckVelocity(int xStart, int xEnd, int yStart, int yEnd, int xVel, int yVel)
        {
            int maxYPos = 0;
            // Console.WriteLine("xStart:{0}, xEnd:{1}, yStart:{2}, yEnd:{3}", xStart, xEnd, yStart, yEnd);
            (int xPos, int yPos) = (0, 0);
            while (xPos <= xEnd && yPos >= yStart)
            {
                // Console.WriteLine("Position x:{0} y:{1} with xVel:{2} yVel:{3}", xPos, yPos, xVel, yVel);
                if (xPos >= xStart && xPos <= xEnd && yPos >= yStart && yPos <= yEnd)
                {
                    return (true, maxYPos);
                }

                xPos += xVel;
                yPos += yVel;
                maxYPos = Math.Max(maxYPos, yPos);
                if (xVel < 0)
                {
                    xVel += 1;
                }
                else if (xVel > 0)
                {
                    xVel -= 1;
                }
                yVel -= 1;
            }
            return (false, 0);
        }

        private static void FindVelocity(int xStart, int xEnd, int yStart, int yEnd)
        {
            int maxYPos = 0;
            int count = 0;
            for (int xVel = -200; xVel <= 200; xVel++)
            {
                for (int yVel = -200; yVel <= 200; yVel++)
                {
                    (bool validVelocity, int yPos) = CheckVelocity(xStart, xEnd, yStart, yEnd, xVel, yVel);
                    if (validVelocity)
                    {
                        count++;
                        maxYPos = Math.Max(maxYPos, yPos);
                        Console.WriteLine("Landed in target probe with initial velocity x:{0} y:{1}", xVel, yVel);
                    }
                }
            }
            Console.WriteLine("Max yPos:{0}", maxYPos);
            Console.WriteLine("Count:{0}", count);
        }

        
        public static void Solve1()
        {
            string input = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day17\\input.txt")[0];
            (int xStart, int xEnd, int yStart, int yEnd) = ParseInput(input);
            FindVelocity(xStart, xEnd, yStart, yEnd);
        }
    }
}
