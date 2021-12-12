using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateCountFlashes(100));
            Console.WriteLine(CalculateSyncStep());
            Console.ReadKey();
        }

        static int CalculateSyncStep()
        {
            var octupuses = ReadOctopuses("input.txt");
            int result = 1;
            int octupsCount = octupuses.Length * octupuses[0].Length;

            while (Step(octupuses) != octupsCount)
            {
                result++;
            }

            return result;
        }

        static int CalculateCountFlashes(int steps)
        {
            var octupuses = ReadOctopuses("input.txt");
            int result = 0;

            for (int i = 0; i < steps; i++)
            {
                result += Step(octupuses);
            }

            return result;
        }

        static void Print(double[][] array)
        {
            foreach (var row in array)
            {
                foreach (var col in row)
                {
                    Console.Write(col);
                }
                Console.WriteLine();
            }
            Console.WriteLine(Environment.NewLine);
        }

        static int Step(double[][] octupuses)
        {
            int result = 0;

            int rows = octupuses.Length;
            int cols = octupuses[0].Length;
            var queue = new Queue<int[]>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (octupuses[i][j] == 9)
                    {
                        octupuses[i][j] = 0;
                        queue.Enqueue(new int[2] { i, j });
                        result++;
                    }
                    else
                    {
                        octupuses[i][j]++;
                    }
                }
            }

            while (queue.Count > 0)
            {
                var octupus = queue.Dequeue();
                foreach (var direction in DIRECTIONS)
                {
                    int r = octupus[0] + direction[0];
                    int c = octupus[1] + direction[1];

                    if (r >= 0 && r < rows && c >= 0 && c < cols && octupuses[r][c] != 0)
                    {
                        if (octupuses[r][c] == 9)
                        {
                            octupuses[r][c] = 0;
                            queue.Enqueue(new int[] { r, c });
                            result++;
                        }
                        else
                        {
                            octupuses[r][c]++;
                        }
                    }

                }
            }

            return result;
        }

        static int[][] DIRECTIONS = new int[][] { 
                new int[] { -1, -1 }, new int[] { -1, 0 }, new int[] { -1, 1 },
                new int[] { 0, -1 }, new int[] { 0, 1 },
                new int[] { 1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 },
        };

        static double[][] ReadOctopuses(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).Select(x => x.Select(Char.GetNumericValue).ToArray()).ToArray();
        }
    }
}
