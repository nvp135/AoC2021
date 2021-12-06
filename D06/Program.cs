using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CountFishes(ParseFishes("input.txt"), 80));
            Console.WriteLine(CountFishes(ParseFishes("input.txt"), 256));

            Console.ReadKey();
        }

        static long CountFishes(List<int> fishes, int daysCount)
        {
            long[] days = new long[9];
            for (int i = 0; i < fishes.Count; i++)
            {
                days[fishes[i]]++;
            }
            for (int i = 0; i < daysCount; i++)
            {
                long newFishes = days[0];
                for (int j = 0; j < days.Length - 1; j++)
                {
                    days[j] = days[j+1];
                }
                days[6] += newFishes;
                days[8] = newFishes;
            }

            long result = 0;
            foreach (var day in days)
            {
                result += day;
            }

            return result;
        }

        static long BadSolution(List<int> fishes, int daysCount)
        {
            for (int i = 0; i < daysCount; i++)
            {
                Console.WriteLine(i);
                int fishesCount = fishes.Count;
                for (int j = 0; j < fishesCount; j++)
                {
                    fishes[j]--;
                    if (fishes[j] == -1)
                    {
                        fishes[j] = 6;
                        fishes.Add(8);
                    }
                }
            }

            return fishes.Count;
        }

        static List<int> ParseFishes(string filePath)
        {
            return File.ReadAllText(filePath).Split(',').Select(Int32.Parse).ToList();
        }
    }
}
