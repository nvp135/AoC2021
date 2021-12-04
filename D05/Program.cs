using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D05
{
    class Program
    {
        private const int FIELD_SIZE = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(Part1(ParseCoordinates("input.txt")));
            Console.WriteLine(Part2(ParseCoordinates("input.txt")));

            Console.ReadKey();  
        }

        static int Part1(List<Line> coordinates)
        {
            int[][] field = new int[FIELD_SIZE][];

            for (int i = 0; i < FIELD_SIZE; i++)
            {
                field[i] = new int[FIELD_SIZE];
            }

            foreach (var line in coordinates)
            {
                if (line.x1 == line.x2 || line.y1 == line.y2)
                {
                    DrawLine(field, line);
                }
            }

            int result = 0;

            foreach (var y in field)
            {
                foreach (var x in y)
                {
                    if (x > 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        static int Part2(List<Line> coordinates)
        {
            int[][] field = new int[FIELD_SIZE][];

            for (int i = 0; i < FIELD_SIZE; i++)
            {
                field[i] = new int[FIELD_SIZE];
            }

            foreach (var line in coordinates)
            {
                DrawLine(field, line);
            }

            int result = 0;

            foreach (var y in field)
            {
                foreach (var x in y)
                {
                    if (x > 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        private static void DrawLine(int[][] field, Line line)
        {
            int n = Math.Max(Math.Abs(line.x2 - line.x1) + 1, Math.Abs(line.y2 - line.y1) + 1);

            for (int i = 0; i < n; i++)
            {
                int x = line.x1 + Math.Sign(line.x2 - line.x1) * i;
                int y = line.y1 + Math.Sign(line.y2 - line.y1) * i;
                field[y][x]++;
            }

        }

        static List<Line> ParseCoordinates(string fileName)
        {
            var rows = File.ReadAllText(fileName)
                            .Split(Environment.NewLine)
                            .ToList();

            var lines = new List<Line>();

            foreach (var line in rows)
            {
                var coords = line.Split(" -> ").Select(x => x.Split(',').Select(Int32.Parse).ToList()).ToList();
                int x1 = coords[0][0], x2 = coords[1][0], y1 = coords[0][1], y2 = coords[1][1];
                lines.Add(new Line(x1, y1, x2, y2));
            }

            return lines;
        }

        class Line
        {
            public Line(int x1, int y1, int x2, int y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }

            public int x1 { get; set; }
            public int y1 { get; set; }
            public int x2 { get; set; }
            public int y2 { get; set; }
        }
    }
}
