using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//using EdgeList = System.Collections.Generic.List<(int node, double weight)>;

namespace D15
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
            var graph = GenerateGraph(ReadInput("sample.txt"));
            return Dijkstra(graph, (0, 0));
            //return 0;
        }

        static int Part2()
        {
            var data = ReadInput("input.txt");
            var tailCount = 5;
            var size = data.Length;
            var fullGraph = new double[data.Length * tailCount][];

            Func<double, double> calculateIndex = x => (x - 1) % 9 + 1;

            for (int y = 0; y < size * tailCount; y++)
            {
                fullGraph[y] = new double[size * tailCount];
            }

            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    for (int i = 0; i < tailCount; i++)
                    {
                        var yi = y + (i * size);
                        fullGraph[yi][x] = calculateIndex(data[y][x] + i);
                    }
                }
            }

            for (int y = 0; y < fullGraph.Length; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int i = 1; i < tailCount; i++)
                    {
                        var xi = x + (i * size);
                        fullGraph[y][xi] = calculateIndex(fullGraph[y][x] + i);
                    }
                }
            }

            /*foreach (var y in fullGraph)
            {
                foreach (var x in y)
                {
                    Console.Write(x);
                }
                Console.WriteLine();
            }*/

            var graph = GenerateGraph(fullGraph);
            return Dijkstra(graph, (0, 0));
            
            //return 0;
        }

        static int Dijkstra(Dictionary<(int x, int y), Edge> graph, (int x, int y) source)
        {
            var dist = new Dictionary<(int x, int y), int>();
            var prev = new Dictionary<(int x, int y), Edge>();

            var edges = new List<Edge>();

            foreach (var e in graph)
            {
                dist.Add(e.Key, int.MaxValue);
                prev.Add(e.Key, null);
                edges.Add(e.Value);
            }

            var startEdge = edges.Find(x => x.x == source.x && x.y == source.y);
            startEdge.MinWeight = 0;
            dist[(startEdge.x, startEdge.y)] = 0;

            while (edges.Count > 0)
            {
                edges.Sort((x, y) => x.MinWeight.CompareTo(y.MinWeight));
                var u = edges.First();
                edges.Remove(u);
                foreach (var n in u.Neighbours)
                {
                    if (edges.Contains(n))
                    {
                        var alt = n.Weight + dist[(u.x, u.y)];
                        if (alt < n.MinWeight)
                        {
                            n.MinWeight = alt;
                            dist[(n.x, n.y)] = alt;
                            prev[(n.x, n.y)] = u;
                        }
                    }
                }
            }

            return dist.Last().Value;
        }

        static Dictionary<(int x, int y), Edge> GenerateGraph(double[][] array)
        {
            var dict = new Dictionary<(int x, int y), Edge>();
            for (int y = 0; y < array.Length; y++)
            {
                for (int x = 0; x < array[y].Length; x++)
                {
                    var edge = new Edge(x, y, (int)array[y][x]);
                    dict.Add((x, y), edge);
                }
            }

            foreach (var edge in dict.Values)
            {
                foreach (var way in ways)
                {
                    var x = way.x + edge.x;
                    var y = way.y + edge.y;
                    if (y >= 0 && y < array.Length && x >= 0 && x < array[edge.x].Length)
                    {
                        edge.Neighbours.Add(dict[(x, y)]);
                    }
                }
            }

            return dict;
        }

        static List<(int x, int y)> ways = new List<(int, int)> {
            ( 1, 0 ),
            ( 0, 1 ),
            //( -1, 0),
            //(0, -1)
            };

        class Edge
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="x">x</param>
            /// <param name="y">y</param>
            /// <param name="weight">Weight</param>
            public Edge(int x, int y, int weight)
            {
                this.x = x;
                this.y = y;
                Weight = weight;
            }

            public int x { get; set; }
            public int y { get; set; }
            public int Weight { get; set; }
            public int MinWeight { get; set; } = int.MaxValue;
            public List<Edge> Neighbours { get; set; } = new List<Edge>();
        }

		static double[][] ReadInput(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).Select(x => x.Select(Char.GetNumericValue).ToArray()).ToArray();
        }
    }
}
