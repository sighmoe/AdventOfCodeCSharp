using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeCSharp
{
    class Day15
    {

        private static void PrintGraph(int[,] graph, int rows, int cols)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(graph[r, c]);
                }
                Console.WriteLine();
            }

        }

        private static int[,] ParseInput(string[] lines)
        {
            int rows = lines.Length;
            int cols = lines[0].Length;
            int[,] arr = new int[rows, cols];

            int row = 0;
            int col;
            foreach (string line in lines)
            {
                col = 0;
                foreach (char c in line)
                {
                    arr[row, col] = c - '0';
                    col++;
                }
                row++;
            }
            return arr;
        }

        private static List<(int, int)> GetAdjacent(int[,] graph, int row, int col, int rows, int cols)
        {
            List<(int, int)> adjacent = new List<(int, int)>();
            for (int i = -1; i <= 1; i++)
            {
                if (i == 0) continue;
                var ri = row + i;
                var ci = col + i;
                if (ri >= 0 && ri < rows)
                {
                    adjacent.Add((ri, col));
                }

                if (ci >= 0 && ci < cols)
                {
                    adjacent.Add((row, ci));
                }
            }

            return adjacent;
        }

        private static void PrintHashSet(HashSet<(int,int)> set)
        {
            Console.WriteLine("PRINT HASHSET BEGIN");
            foreach ((int,int) tup in set)
            {
                Console.WriteLine("Row: {0} Col: {1}", tup.Item1, tup.Item2);
            }
            Console.WriteLine("PRINT HASHSET END");
        }

        private static void Dijkstra(int[,] graph, (int, int) start, (int, int) end)
        {
            var (rows, cols) = (end.Item1 + 1, end.Item2 + 1);

            PriorityQueue<(int, int, int), int> pq = new PriorityQueue<(int,int, int), int>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            pq.Enqueue((start.Item1, start.Item2, 0), 0);

            while (pq.Count > 0)
            {
                var (row,col, cost) = pq.Dequeue();
                Console.WriteLine("Visiting Row: {0} Col: {1} Cost: {2}", row, col, cost);
                if (visited.Contains((row, col))) continue;
                visited.Add((row, col));
                if ((row,col) == end)
                {
                    Console.WriteLine("Optimal cost: {0}", cost);
                    break;
                }
                var adjacent = GetAdjacent(graph, row, col, rows, cols);
                // PrintHashSet(visited);
                foreach (var adj in adjacent)
                {
                    if (!visited.Contains(adj))
                    {
                        int newCost = cost + graph[adj.Item1, adj.Item2];
                        var newItem = (adj.Item1, adj.Item2, newCost);
                        pq.Enqueue(newItem, newCost);
                    }
                }
            }

        }

        public static void Solve1()
        {
            string[] input = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day15\\input.txt");
            int rows = input.Length;
            int cols = input[0].Length;
            int[,] graph = ParseInput(input);
            PrintGraph(graph, rows, cols);
            Dijkstra(graph, (0, 0), (rows - 1, cols - 1));
        }
        private static int[,] ExpandGraph(int[,] graph, int rows, int cols)
        {
            int newRows = rows * 5;
            int newCols = cols * 5;
            int[,] newGraph = new int[newRows, newCols];
            for (int i = 0; i < newRows; i++)
            {
                int rowScale = i / rows;
                int colScale;
                for (int j = 0; j < newCols; j++)
                {
                    colScale = j / cols;
                    int newVal = (graph[i % rows, j % cols] + rowScale + colScale);
                    if (newVal >= 10)
                    {
                        newGraph[i, j] = (newVal%10)+1;
                    }
                    else
                    {
                        newGraph[i, j] = newVal;
                    }
                }
            }
            return newGraph;
        }

        public static void Solve2()
        {
            string[] input = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day15\\input.txt");
            int rows = input.Length;
            int cols = input[0].Length;
            int[,] graph = ParseInput(input);
            int[,] graph2 = ExpandGraph(graph, rows, cols);
            //PrintGraph(graph, rows, cols);
            //PrintGraph(graph2, rows*5, cols*5);
            Dijkstra(graph2, (0, 0), (rows*5 - 1, cols*5 - 1));
        }
    }
}
