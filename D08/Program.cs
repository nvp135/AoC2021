using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D08
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(ParseDigits("input.txt")));
            Console.WriteLine(Part2(ParseDigits("input.txt")));
        }

        static int Part1(List<string> digits)
        {
            var uniqueNumbers = new List<string>();
            foreach (var digit in digits)
            {
                uniqueNumbers.AddRange(digit.Split(" | ")[1].Split(' ').Where(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7).Select(dg => dg));
            }
            return uniqueNumbers.Count();
        }

        static Dictionary<string, int> DIGITS_CODES = new Dictionary<string, int> {   
            {"01111110", 0},
            {"00110000", 1},
            {"01101101", 2},
            {"01111001", 3},
            {"00110011", 4},
            {"01011011", 5},
            {"01011111", 6},
            {"01110000", 7},
            {"01111111", 8},
            {"01111011", 9}
        };

        static string SEGMENTS_DCODES = "abcdefg";

        static int Part2(List<string> digits)
        {
            var result = 0;
            var arr = new char[8];
            foreach (var digit in digits)
            {
                result += DecodeRow(digit);
            }

            return result;
        }

        static int DecodeRow(string row)
        {
            var mappingDict = new Dictionary<char, int>();
            var mappingOrder = new List<int>() { 2, 3, 1, 7, 6, 4, 5 };
            var allDigits = row.Split(" | ")[0].Split(' ');
            var uniqueNumbers = allDigits
                                    .Where(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7)
                                    .Select(dg => dg)
                                    .OrderBy(dg => dg.Length)
                                    .ToArray();
            var one = uniqueNumbers[0];
            var four = uniqueNumbers[2];
            var seven = uniqueNumbers[1];
            var eight = uniqueNumbers[3];
            string two = null;
            string three = null;
            string five = null;

            for (int i = 0; i < seven.Length; i++)
            {
                if (!one.Contains(seven[i]))
                {
                    mappingDict.Add(seven[i], 1);
                }
            }

            var fiveSegmentsDigits = allDigits.Where(dg => dg.Length == 5).Select(d => d).ToArray();

            for (int i = 0; i < fiveSegmentsDigits.Length; i++)
            {
                var missed = new int[3];
                for (int j = 0; j < uniqueNumbers.Length - 1; j++)
                {
                    int missedCount = 0;

                    for (int k = 0; k < uniqueNumbers[j].Length; k++)
                    {
                        if (!fiveSegmentsDigits[i].Contains(uniqueNumbers[j][k]))
                        {
                            missedCount++;
                        }
                    }
                    missed[j] = missedCount;
                }

                if (missed[0] == 1 && missed[1] == 1 && missed[2] == 2)
                {
                    two = fiveSegmentsDigits[i];
                }
                else if (missed[0] == 0 && missed[1] == 0 && missed[2] == 1)
                {
                    three = fiveSegmentsDigits[i];
                }
                else if (missed[0] == 1 && missed[1] == 1 && missed[2] == 1)
                {
                    five = fiveSegmentsDigits[i];
                }
            }

            if (!String.IsNullOrEmpty(five))
            {
                for (int i = 0; i < five.Length; i++)
                {
                    if (!(four + seven).Contains(five[i]))
                    {
                        mappingDict.Add(five[i], 4);
                    }
                }

                for (int i = 0; i < one.Length; i++)
                {
                    if (!five.Contains(one[i]))
                    {
                        mappingDict.Add(one[i], 2);
                    }
                    else
                    {
                        mappingDict.Add(one[i], 3);
                    }
                }
            }
            if (!String.IsNullOrEmpty(three))
            {
                if (!mappingDict.ContainsValue(4))
                {
                    for (int i = 0; i < three.Length; i++)
                    {
                        if (!(four + seven).Contains(three[i]))
                        {
                            mappingDict.Add(three[i], 4);
                        }
                    }
                }

                for (int i = 0; i < four.Length; i++)
                {
                    if (!three.Contains(four[i]) && !mappingDict.ContainsKey(four[i]))
                    {
                        mappingDict.Add(four[i], 6);
                    }
                }

            }
            else if (!String.IsNullOrEmpty(two))
            {
                if (mappingDict.ContainsValue(4))
                {

                }

                if (!mappingDict.ContainsValue(3) || !mappingDict.ContainsValue(2))
                {
                    for (int i = 0; i < one.Length; i++)
                    {
                        if (!three.Contains(one[i]))
                        {
                            mappingDict.Add(one[i], 3);
                        }
                        else
                        {
                            mappingDict.Add(one[i], 2);
                        }
                    }
                }
            }

            if (mappingDict.ContainsValue(4))
            {
                for (int i = 0; i < SEGMENTS_DCODES.Length; i++)
                {
                    if (!(four + seven).Contains(SEGMENTS_DCODES[i]) && !mappingDict.ContainsKey(SEGMENTS_DCODES[i]))
                    {
                        mappingDict.Add(SEGMENTS_DCODES[i], 5);
                        break;
                    }
                }
            }

            if (mappingDict.ContainsValue(4) && mappingDict.ContainsValue(5))
            {
                for (int i = 0; i < SEGMENTS_DCODES.Length; i++)
                {
                    if (!mappingDict.ContainsKey(SEGMENTS_DCODES[i]))
                    {
                        mappingDict.Add(SEGMENTS_DCODES[i], 7);
                        break;
                    }
                }
            }

            if (mappingDict.Count != 7)
            {
                Console.WriteLine("Mapping doesn't work for: " + row);
                return 0;
            }

            var resDigits = new List<int>();

            foreach (var d in row.Split(" | ")[1].Split(' ').ToArray())
            {
                var resultArr = "00000000".ToArray();
                foreach (var k in d)
                {
                    resultArr[mappingDict[k]] = '1';
                }
                resDigits.Add(DIGITS_CODES[new string(resultArr)]);
            }

            return resDigits[0] * 1000 + resDigits[1] * 100 + resDigits[2] * 10 + resDigits[3];
        }

        static List<string> ParseDigits(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
        }
    }
}