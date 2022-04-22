using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeCSharp
{
    class Day20
    {
        private static string algorithm;

        private static void PrintImg(string[,] img, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write("{0}", img[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static (string[,], int, int) PadImg(string[,] img, int rows, int cols)
        {
            string[,] newImg = new string[rows + 6, cols + 6];
            for (int r = 0; r < rows+6; r++)
            {
                for (int c = 0; c < cols+6; c++)
                {
                    newImg[r, c] = ".";
                }
            }
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    newImg[r + 3, c + 3] = img[r, c];

                }
            }
            return (newImg, rows+6, cols+6);
        }

        private static string GetSurroundingPixels(string[,] img, int r, int c, int rows, int cols)
        {
            string acc = "";
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int ri = r + i;
                    int cj = c + j;
                    if (ri >= 0 && ri < rows && cj >= 0 && cj < cols)
                    {
                        acc += img[ri, cj];
                    } 
                    else
                    {
                        acc += ".";
                    }

                }
            }
            return acc;
        }

        private static int GetEnhancementIndex(string s)
        {
            return Util.FromBinaryImg(s);
        }

        private static void UpdateImg(string[,] img, string[,] oldImg, int r, int c, int rows, int cols)
        {
            string surrounding = GetSurroundingPixels(oldImg, r-3, c-3, rows-6, cols-6);
            // Console.WriteLine("Surrounding pixels:{0}", surrounding);
            int index = GetEnhancementIndex(surrounding);
            // Console.WriteLine("r:{0} c:{1} index:{2}", r, c, index);
            img[r, c] = algorithm[index].ToString();
        }

        private static (string, string[,], int, int) ParseInput(string[] input)
        {
            string imgEnhancement = input[0];

            int rows = input.Length - 2;
            int cols = input[2].Length;
            string[,] img = new string[rows, cols];
            for (int i = 2; i < input.Length; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    img[i-2,j] = input[i][j].ToString();
                }
            }
            Console.WriteLine("Img Enhancement Algorithm: {0}\nImg:\n", imgEnhancement);
            PrintImg(img, rows, cols);
            return (imgEnhancement, img, rows, cols);
        }

        private static (string[,], int, int) EnhanceImage (string[,] img, int oldRows, int oldCols)
        {
            (string[,] paddedImg, int rows, int cols) = PadImg(img, oldRows, oldCols);
            Console.WriteLine("PADDED IMG:\n");
            PrintImg(paddedImg, rows, cols);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    UpdateImg(paddedImg, img, r, c, rows, cols);
                }
            }
            return (paddedImg, rows, cols);
        }

        private static int CountLitPixels(string[,] img, int rows, int cols)
        {
            int count = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (img[r, c].Equals("#"))
                        count++;
                }
            }
            return count;
        }
        public static void Solve1()
        {
            string[] input = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day20\\input.txt");
            (string imgEnhancement, string[,] img, int rows, int cols) = ParseInput(input);
            algorithm = imgEnhancement;
            string surrounding = GetSurroundingPixels(img, 2, 2, rows, cols);
            Console.WriteLine("Surrounding pixels for (2,2):{0}\nIndex for enhancement algorithm:{1}", surrounding, GetEnhancementIndex(surrounding));

            for (int i = 0; i < 2; i++)
            {
                (string[,] newImg, int newRows, int newCols) = EnhanceImage(img, rows, cols);
                img = newImg;
                rows = newRows;
                cols = newCols;
                PrintImg(img, rows, cols);
                Console.WriteLine("Lit pixels after iteration {0}: {1}", (i + 1), CountLitPixels(newImg, rows, cols));
            }
        }
    }
}
