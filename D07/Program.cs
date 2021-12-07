using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateMinFuelCostP1(ParseCrabs("input.txt")));
            Console.WriteLine(CalculateMinFuelCostP2(ParseCrabs("input.txt")));

            Console.ReadKey();
        }

        static long CalculateMinFuelCostP1(long[] crabsSubmarines)
        {
            long minCost = Int64.MaxValue;

            for (long i = 0; i < crabsSubmarines.Length; i++)
            {
                if (crabsSubmarines[i] > 0)
                {
                    minCost = Math.Min(minCost, CalculateCostP1(crabsSubmarines, i));
                }
            }

            return minCost;
        }

        static long CalculateMinFuelCostP2(long[] crabsSubmarines)
        {
            long minCost = Int64.MaxValue;            

            for (long i = 0; i < crabsSubmarines.Length; i++)
            {
                minCost = Math.Min(minCost, CalculateCostP2(crabsSubmarines, i));
            }

            return minCost;
        }


        public static long CalculateCostP1(long[] crabsSubmarines, long position)
        {
            long result = 0;
            
            for (long i = 0; i < crabsSubmarines.Length; i++)
            {
                result += crabsSubmarines[i] * Math.Abs(position - i);
            }

            return result;
        }

        
        public static long CalculateCostP2(long[] crabsSubmarines, long position)
        {
            long result = 0;

            for (long i = 0; i < crabsSubmarines.Length; i++)
            {
                var stepsCount = 0;
                for (int j = 1; j <= Math.Abs(position - i); j++)
                {
                    stepsCount += j;
                }
                result += crabsSubmarines[i] * stepsCount;
            }

            return result;
        }
        

        static long[] ParseCrabs(string filePath)
        {
            var crabs = File.ReadAllText(filePath).Split(',').Select(Int64.Parse).ToList();

            var submarinesCount = new long[crabs.Max() + 1];
            
            foreach (var crab in crabs)
            {
                submarinesCount[crab]++;
            }

            return submarinesCount;
        }
    }
}
