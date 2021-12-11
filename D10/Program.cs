using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateSyntaxError(ReadLines("input.txt")));
            Console.WriteLine(CompleteLines(ReadLines("input.txt")));
            Console.ReadKey();
        }

        static int CalculateSyntaxError(IEnumerable<string> lines)
        {
            int result = 0;

            foreach (var line in lines)
            {
                result += CheckLine(line).ErrorScore;
            }

            return result;
        }

        static Dictionary<char, int> pairs = new Dictionary<char, int>()
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };

        static DTOLineResult CheckLine(string line)
        {
            var result = new DTOLineResult();
            Stack<char> stack = new Stack<char>();
            result.LineStack = stack;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                switch (c)
                {
                    case '[':
                        stack.Push(c);
                        break;
                    case '(':
                        stack.Push(c);
                        break;
                    case '{':
                        stack.Push(c);
                        break;
                    case '<':
                        stack.Push(c);
                        break;
                    case ']':
                        if (stack.Peek() == '[')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            result.ErrorScore = 57;
                            return result;
                        }
                        break;
                    case ')':
                        if (stack.Peek() == '(')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            result.ErrorScore = 3;
                            return result;
                        }
                        break;
                    case '}':
                        if (stack.Peek() == '{')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            result.ErrorScore = 1197;
                            return result;
                        }
                        break;
                    case '>':
                        if (stack.Peek() == '<')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            result.ErrorScore = 25137;
                            return result;
                        }
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        static long CompleteLines(IEnumerable<string> lines)
        {
            List<long> result = new List<long>();

            foreach (var line in lines)
            {
                var checkLine = CheckLine(line);
                if (checkLine.ErrorScore == 0)
                {
                    long lineResult = 0;
                    while (checkLine.LineStack.Count > 0)
                    {
                        lineResult = lineResult * 5 + pairs[checkLine.LineStack.Pop()];
                    }
                    result.Add(lineResult);
                }
            }

            result.Sort();

            return result[result.Count / 2];
        }

        static IEnumerable<string> ReadLines(string filePath)
        {
            return File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
        }
        
        class DTOLineResult
        {
            public int ErrorScore { get; set; }

            public Stack<char> LineStack { get; set; }
        }
    }
}
