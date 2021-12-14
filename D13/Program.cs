using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
            Console.ReadKey();
        }

        static int Part1()
        {
            InitData();

            var fold = folds[0].Split('=').ToArray();
            switch (fold[0])
            {
                case "x":
                    FoldX(Convert.ToInt32(fold[1]));
                    break;
                case "y":
                    FoldY(Convert.ToInt32(fold[1]));
                    break;
                default:
                    break;
            }

            return pointsDict.Count;
        }

        static int Part2()
        {
            InitData();

            foreach (var f in folds)
            {
                var fold = f.Split('=').ToArray();
                switch (fold[0])
                {
                    case "x":
                        FoldX(Convert.ToInt32(fold[1]));
                        break;
                    case "y":
                        FoldY(Convert.ToInt32(fold[1]));
                        break;
                    default:
                        break;
                }
            }

            var maxXPos = Int32.MinValue;
            var maxYPos = Int32.MinValue;

            foreach (var p in points)
            {
                if (p.X > maxXPos)
                {
                    maxXPos = p.X + 1;
                }
                if (p.Y > maxYPos)
                {
                    maxYPos = p.Y + 1;
                }
            }

            var result = new char[maxYPos][];

            for (int i = 0; i < maxYPos; i++)
                result[i] = Enumerable.Range(0, maxXPos+1).Select(i => '.').ToArray();

            foreach (var val in pointsDict.Values)
                result[val.Y][val.X] = '#';

            foreach (var r in result)
            {
                foreach (var c in r)
                {
                        Console.Write(c);
                }
                Console.WriteLine();
            }


            return pointsDict.Count;
        }

        static void FoldX(int x) 
        {
            var result = new List<Point>(points);
            
            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                if (p.X >= x)
                {
                    result.Remove(p);
                    pointsDict.Remove(p.ToString());
                    
                    p.X = x - (p.X - x);

                    if (!pointsDict.ContainsKey(p.ToString()))
                    {
                        pointsDict.Add(p.ToString(), p);
                        result.Add(p);
                    }
                }
            }

            points = result;
        }

        static void FoldY(int y) 
        {
            var result = new List<Point>(points);
            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];

                if (p.Y >= y)
                {
                    result.Remove(p);
                    pointsDict.Remove(p.ToString());
                    p.Y = y - (p.Y - y);

                    if (!pointsDict.ContainsKey(p.ToString()))
                    {
                        pointsDict.Add(p.ToString(), p);
                        result.Add(p);
                    }
                }
            }

            points = result;
        }

        static List<Point> points;
        static List<string> folds;
        static Dictionary<string, Point> pointsDict;

        static void InitData()
        {
            points = new List<Point>();
            folds = new List<string>();
            pointsDict = new Dictionary<string, Point>();

            var data = ReadData("input.txt");
            int i = 0;
            for (; i < data.Count; i++)
            {
                if (data[i]=="")
                {
                    i++;
                    break;
                }
                var coord = data[i].Split(',').Select(Int32.Parse).ToArray();
                var point = new Point(coord[0], coord[1]);
                if (!pointsDict.ContainsKey(point.ToString()))
                {
                    pointsDict.Add(point.ToString(), point);
                }
                points.Add(point);
            }

            for (; i < data.Count; i++)
            {
                folds.Add(data[i].Split(' ')[2]);
            }
        }


        static List<string> ReadData(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
        }

        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }
    }
}
