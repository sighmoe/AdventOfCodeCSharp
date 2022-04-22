using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeCSharp
{
    class Day4
    {
        private static int[] nums;
        private static List<int[][]> boards = new List<int[][]>();
        
        private static int[][] ReadBingoBoard(StreamReader sr)
        {
            int[][] board = new int[5][];
            int c;
            for (int r = 0; r < 5; r++)
            {
                c = 0;
                board[r] = new int[5];
                string[] row = sr.ReadLine().Trim(' ').Split(" ");
                foreach (string s in row)
                {
                    if (s.Equals("")) 
                        continue;
                    board[r][c++] = Int32.Parse(s);
                }
            }

            PrintBoard(board);
            return board;
        }

        private static void ReadBingoInput(string fpath)
        {
            StreamReader sr = new StreamReader(fpath);
            nums = sr.ReadLine().Split(",").Select(v => Int32.Parse(v)).ToArray();
            
            while (sr.ReadLine() != null) // each bingo card is preceeded by an empty line
            {
                boards.Add(ReadBingoBoard(sr));
            }
        }

        private static void PrintBoard(int[][] board)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", board[i][0], board[i][1], board[i][2], board[i][3], board[i][4]);
            }
            Console.WriteLine();
        }

        private static void MarkVal(int[][] board, int val)
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (board[r][c] == val)
                    {
                        board[r][c] = -1;
                    }
                }
            }
        }

        private static bool CheckBingo(int[][] board)
        {
            // check cols 
            int rCount;
            int cCount;
            for (int r = 0; r < 5; r++)
            {
                rCount = 0;
                cCount = 0;
                for (int c = 0; c < 5; c++)
                {
                    if (board[r][c] == -1) cCount++;
                    if (board[c][r] == -1) rCount++;

                    if (rCount == 5 || cCount == 5) return true;
                }
            }

            return false;
        }

        private static int AddUnmarked(int[][] board)
        {
            int acc = 0;
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (board[r][c] != -1)
                    {
                        acc += board[r][c];
                    }
                }
            }
            return acc;
        }

        private static bool MarkValAndCheckBingo(int[][] board, int val)
        {
            MarkVal(board, val);
            return CheckBingo(board);
        }

        public static void Solve1()
        {
            ReadBingoInput("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day4\\input1.txt");
            int unmarkedSum;
            foreach (int num in nums)
            {
                foreach (int[][] board in boards)
                {
                    if (MarkValAndCheckBingo(board, num))
                    {
                        unmarkedSum = AddUnmarked(board);
                        Console.WriteLine("BINGO! Unmarked sum: {0}. Num: {1}. Answer: {2}", unmarkedSum, num, (unmarkedSum * num));
                        PrintBoard(board);
                        return; 
                    }
                }
            }
        }
        
        public static void Solve2()
        {
            ReadBingoInput("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day4\\input1.txt");
            int unmarkedSum;
            List<int[][]> solved = new List<int[][]>();
            foreach (int num in nums)
            {
                foreach (int[][] board in boards)
                {
                    if (MarkValAndCheckBingo(board, num))
                    {
                        solved.Add(board);
                        unmarkedSum = AddUnmarked(board);
                        Console.WriteLine("BINGO! Unmarked sum: {0}. Num: {1}. Answer: {2}", unmarkedSum, num, (unmarkedSum * num));
                        PrintBoard(board);
                    }
                }

                foreach (int[][] board in solved)
                {
                    boards.Remove(board);
                }
            }
        }
    }
}
