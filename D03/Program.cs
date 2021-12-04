using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace D03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculatePowerConsumption("input.txt"));
            Console.WriteLine(CalculateLifeSupportRating("input.txt"));

            Console.ReadKey();
        }

        static int CalculatePowerConsumption(string filePath)
        {
            var report = File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
            int bitsCount = report[0].Length;
            int[] bitsOf1 = new int[bitsCount];
            
            foreach (var val in report)
            {
                for (int i = 0; i < bitsCount; i++)
                {
                    if (val[i] == '1')
                    {
                        bitsOf1[i]++;
                    }
                }
            }

            string gamma = "";
            string epsilon = "";

            foreach (var pos in bitsOf1)
            {
                if (pos > report.Count - pos)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        static int CalculateLifeSupportRating(string filePath)
        {
            var report = File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
            var oxygen = new List<String>(report);
            var co2 = new List<String>(report);

            int bitsCount = report[0].Length;
            report = null;

            for (int i = 0; i < bitsCount; i++)
            {
                if (oxygen.Count == 1)
                {
                    break;
                }

                int countOf1 = oxygen.Where(x => x[i] == '1').Count();

                if (countOf1 >= oxygen.Count - countOf1)
                    oxygen = oxygen.Select(x => x).Where(x => x[i] == '1').ToList();
                else
                    oxygen = oxygen.Where(x => x[i] == '0').Select(x => x).ToList();
                
            }

            for (int i = 0; i < bitsCount; i++)
            {
                if (co2.Count == 1)
                {
                    break;
                }

                int countOf1 = co2.Where(x => x[i] == '1').Count();

                if (countOf1 >= co2.Count - countOf1)
                    co2 = co2.Select(x => x).Where(x => x[i] == '0').ToList();
                else
                    co2 = co2.Where(x => x[i] == '1').Select(x => x).ToList();

            }

            return Convert.ToInt32(oxygen.FirstOrDefault(), 2) * Convert.ToInt32(co2.FirstOrDefault(), 2);
        }
    }
}
