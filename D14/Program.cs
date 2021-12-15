using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Part1(40));
            Console.ReadKey();
        }

        static ulong Part1(int stepsCount)
        {
            var input = ReadInput("input.txt");

            const int wordsCount = 26;

            var matrix = new ulong[wordsCount][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new ulong[wordsCount];
            }

            var subs = new List<Sub>();

            foreach (var line in input[1].Split(Environment.NewLine))
            {
                var lineparts = line.Split(" -> ");
                subs.Add(new Sub {  a =  lineparts[0][0] - 'A', 
                                    b =  lineparts[0][1] - 'A',
                                    c =  lineparts[1][0] - 'A' });
            }

            var polymer = input[0].ToCharArray();

            for (int i = 0; i < polymer.Length-1; i++)
            {
                int a = polymer[i] - 'A';
                int b = polymer[i + 1] - 'A';

                matrix[a][b]++;
            }

            for (int i = 0; i < stepsCount; i++)
            {
                matrix = Step(subs, matrix);
            }

            return CalculateDifferenceMinMax(matrix, polymer[polymer.Length-1]);
        }

        static ulong[][] Step(List<Sub> subs, ulong[][] matrix)
        {
            var resultMatrix = new ulong[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                resultMatrix[i] = new ulong[matrix.Length];
            }

            foreach (var sub in subs)
            {
                var count = matrix[sub.a][sub.b];
                resultMatrix[sub.a][sub.c] += count;
                resultMatrix[sub.c][sub.b] += count;
            }
            return resultMatrix;
        }

        class Sub
        {
            public int a, b, c;
        }

        static ulong CalculateDifferenceMinMax(ulong[][] matrix, char lastChar)
        {
            var arr = new ulong[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    arr[i] += matrix[i][j];
                }
            }

            arr[lastChar - 'A']++;

            ulong max = UInt64.MinValue;
            ulong min = UInt64.MaxValue;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    if (arr[i] > max)
                    {
                        max = arr[i];
                    }
                    if (arr[i] < min)
                    {
                        min = arr[i];
                    }
                }
            }

            return max - min;
        }

        static List<string> ReadInput(string filePath)
        {
            return File.ReadAllText(filePath).Split($"{Environment.NewLine}{Environment.NewLine}").ToList();
        }
    }
}
