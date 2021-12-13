using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculatePathsCount());
            Console.WriteLine(CalculatePathsCountTwice());
            Console.ReadKey();
        }

        static Dictionary<string, int> nameIndex;
        static Dictionary<int, string> indexName;
        static bool[][] graph;
        static bool[] visited;
        static int waysCount;

        static int CalculatePathsCount()
        {
            InitGraph();

            int startIndex = nameIndex["start"];

            visited[startIndex] = true;

            DFS(startIndex);

            return waysCount;
        }
        
        static int CalculatePathsCountTwice()
        {
            InitGraph();

            int startIndex = nameIndex["start"];

            visited[startIndex] = true;

            DFSTwice(startIndex, false);

            return waysCount;
        }


        static void InitGraph()
        {
            var edges = ReadLines("input.txt").Select(x => x.Split('-').ToArray()).ToList();

            nameIndex = new Dictionary<string, int>();
            indexName = new Dictionary<int, string>();
            waysCount = 0;

            foreach (var item in edges)
            {
                if (!nameIndex.ContainsKey(item[0]))
                {
                    nameIndex.Add(item[0], nameIndex.Count);
                    indexName.Add(indexName.Count, item[0]);
                }
                if (!nameIndex.ContainsKey(item[1]))
                {
                    nameIndex.Add(item[1], nameIndex.Count);
                    indexName.Add(indexName.Count, item[1]);
                }
            }

            graph = new bool[nameIndex.Count][];
            visited = new bool[nameIndex.Count];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new bool[nameIndex.Count];
            }

            foreach (var edge in edges)
            {
                var e1 = nameIndex[edge[0]];
                var e2 = nameIndex[edge[1]];

                graph[e1][e2] = true;
                graph[e2][e1] = true;
            }
        }


        static void DFS(int u)
        {
            if (u == nameIndex["end"])
            {
                waysCount++;
                return;
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[u][i] && !visited[i])
                {
                    if (!IsCapital(i))
                    {
                        visited[i] = true;
                    }
                    DFS(i);
                    visited[i] = false;
                }
            }
        }

        static void DFSTwice(int u, bool twice)
        {
            if (u == nameIndex["end"])
            {
                waysCount++;
                return;
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[u][i])
                {
                    if (IsCapital(i))
                    {
                        DFSTwice(i, twice);
                    }
                    else if (!visited[i])
                    {
                        visited[i] = true;
                        DFSTwice(i, twice);
                        visited[i] = false;
                    }
                    else if (!twice && i != nameIndex["start"])
                    {
                        DFSTwice(i, true);
                    }
                }
            }
        }

        static bool IsCapital(int i)
        {
            var c = indexName[i][0];
            return 'A' < c && c < 'Z';
        }

        static List<string> ReadLines(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).Select(x => x).ToList();
        }
    }
}