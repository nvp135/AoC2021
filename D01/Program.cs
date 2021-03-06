using System;
using System.IO;
using System.Linq;

namespace D01
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "input.txt";
            int[] measurements = File.ReadAllText(filePath).Split(Environment.NewLine).Select(Int32.Parse).ToArray();
            Console.WriteLine(CalculateNumberOfIncreases(measurements));
            Console.WriteLine(CalculateNumberOfIncreases(filePath));
            Console.WriteLine(CalculateNumberOfThreeIncreases(measurements));
            Console.WriteLine(CalculateNumberOfThreeIncreases(filePath));
        }

        /// <summary>
        /// Number of increases (array)
        /// </summary>
        /// <param name="measurements">Array of values</param>
        /// <returns></returns>
        static int CalculateNumberOfIncreases(int[] measurements)
        {
            int count = 0;
            for (int i = 1; i < measurements.Length; i++)
            {
                if (measurements[i] > measurements[i - 1])
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Number of increases (read line by line)
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns></returns>
        static int CalculateNumberOfIncreases(string filePath)
        {
            int count = 0;
            using (var reader = new StreamReader(filePath)) 
            {
                int current;

                int previous = 0;
                if(Int32.TryParse(reader.ReadLine(), out current))
                {
                    previous = current;
                    while(Int32.TryParse(reader.ReadLine(), out current)) 
                    {
                        if (current > previous)
                        {
                            count++;
                        }
                        previous = current;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Number of increases sum of three values (array)
        /// </summary>
        /// <param name="measurements">Array of numbers</param>
        /// <returns></returns>
        static int CalculateNumberOfThreeIncreases(int[] measurements)
        {
            int count = 0;
            for (int i = 3; i < measurements.Length; i++)
            {
                if (measurements[i-3] + measurements[i-2] + measurements[i-1] < measurements[i - 2] + measurements[i - 1] + measurements[i])
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Number of increases sum of three values (read line by line)
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns></returns>
        static int CalculateNumberOfThreeIncreases(string filePath)
        {
            int count = 0;
            using (var reader = new StreamReader(filePath))
            {
                int a, b, c, d;
                Int32.TryParse(reader.ReadLine(), out a);
                Int32.TryParse(reader.ReadLine(), out b);
                Int32.TryParse(reader.ReadLine(), out c);
                while (Int32.TryParse(reader.ReadLine(), out d))
                {
                    if (a + b + c < b + c + d)
                    {
                        count++;
                    }
                    a = b;
                    b = c;
                    c = d;
                }
            }
            return count;
        }
    }
}
