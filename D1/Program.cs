using System;
using System.Linq;
using System.Collections.Generic;

namespace D01
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] measurements = System.IO.File.ReadAllText("input.txt").Split(Environment.NewLine).Select(Int32.Parse).ToArray();
            Console.WriteLine(NumberOfIncreases(measurements));
            Console.WriteLine(NumberOfThreeIncreases(measurements));
        }

        static int NumberOfIncreases(int[] measurements)
        {
            int count = 0;
            for (int i = 1; i < measurements.Length; i++)
            {
                if (measurements[i] > measurements[i - 1])
                    count++;
            }
            return count;
        }

        static int NumberOfThreeIncreases(int[] measurements)
        {
            int count = 0;
            //int a = measurements[0], b = measurements[1], c = measurements[2];
            for (int i = 3; i < measurements.Length; i++)
            {
                //int d = measurements[i];
                if (measurements[i-3] + measurements[i-2] + measurements[i-1] < measurements[i - 2] + measurements[i - 1] + measurements[i])
                    count++;
            }
            return count;
        }
    }
}
