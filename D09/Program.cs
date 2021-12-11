using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D09
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateLowPoints());
            Console.WriteLine(CalculateLowAreas());
            Console.ReadKey();
        }

        static int ROWS = 0;
        static int COLS = 0;

        static int[][] DIRECTIONS = new int[][] { new int[] { -1, 0 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 0, 1 } };
        static double[][] coordinates;

        static double CalculateLowPoints()
        {
            ParseHeightMap("input.txt");
            double res = 0;

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    if (IsLowPoint(row, col))
                    {
                        res += coordinates[row][col] + 1;
                    }
                }
            }

            return res;
        }

        static bool IsLowPoint(int row, int col)
        {
            for (int i = 0; i < DIRECTIONS.Length; i++)
            {
                int r = row + DIRECTIONS[i][0];
                int c = col + DIRECTIONS[i][1];
                if (0 <= r && r < ROWS && 0 <= c && c < COLS)
                {
                    if (coordinates[row][col] >= coordinates[r][c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static double CalculateLowAreas()
        {
            ParseHeightMap("input.txt");
            var areasCount = new List<int>();

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    if (IsLowPoint(row, col))
                    {
                        var visited = new bool[ROWS][];
                        for (int i = 0; i < ROWS; i++)
                        {
                            visited[i] = new bool[COLS];
                        }
                        areasCount.Add(LowAreaPointsCount(row, col, visited));
                    }
                }
            }
            areasCount = areasCount.OrderByDescending(x => x).ToList();
            return areasCount[0] * areasCount[1] * areasCount[2];
        }

        static int LowAreaPointsCount(int row, int col, bool[][] visited)
        {
            if (visited[row][col])
            {
                return 0;
            }
            visited[row][col] = true;

            int result = 1;
            
            for (int i = 0; i < DIRECTIONS.Length; i++)
            {
                var r = row + DIRECTIONS[i][0];
                var c = col + DIRECTIONS[i][1];
                
                if (0 <= r && r < ROWS && 0 <= c && c < COLS)
                {
                    if (coordinates[r][c] != 9 && coordinates[row][col] <= coordinates[r][c])
                    {
                        result += LowAreaPointsCount(r, c, visited);
                    }
                }
            }
            return result;
        }

        static void ParseHeightMap(string filePath)
        {
            coordinates = File.ReadAllText(filePath).Split(Environment.NewLine).Select(x => x.Select(Char.GetNumericValue).ToArray()).ToArray();
            ROWS = coordinates.Length;
            COLS = coordinates[0].Length;
        }
    }
}
