using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace D04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculateFirstWin("input.txt"));
            Console.WriteLine(CalculateLastWin("input.txt"));
            Console.ReadKey();
        }

        static int CalculateFirstWin(string filePath)
        {
            var inputStrings = File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
            var numbers = inputStrings[0].Split(',').Select(Int32.Parse).ToList();
            inputStrings = inputStrings.Skip(1).ToList();

            var boardsList = CreateBoards(inputStrings);

            foreach (var number in numbers)
            {
                foreach (var board in boardsList)
                {
                    if (board.RemoveNumber(number))
                    {
                        return number * board.GetSumOfNumbers;
                    }
                }
            }

            return 0;
        }

        static int CalculateLastWin(string filePath)
        {
            var inputStrings = File.ReadAllText(filePath).Split(Environment.NewLine).ToList();
            var numbers = inputStrings[0].Split(',').Select(Int32.Parse).ToList();
            inputStrings = inputStrings.Skip(1).ToList();

            var boardsList = CreateBoards(inputStrings);

            foreach (var number in numbers)
            {
                for (int i = 0; i < boardsList.Count; i++)
                {
                    if (boardsList[i].RemoveNumber(number))
                    {
                        if (boardsList.Count == 1)
                        {
                            return number * boardsList[i].GetSumOfNumbers;
                        }
                        boardsList.RemoveAt(i);
                        i--;
                    }
                }
            }

            return 0;
        }

        static List<Board> CreateBoards(IEnumerable<string> strings)
        {
            var boardsList = new List<Board>();

            for (int i = 0; i < strings.Count(); i += 6)
            {
                var b = new Board(strings.Skip(i + 1).Take(5).ToArray());
                boardsList.Add(b);
            }

            return boardsList;
        }

        class Board
        {
            private int FIELD_SIZE = 5;

            int?[][] field;

            public Board(string[] fieldLines)
            {
                field = new int?[FIELD_SIZE][];
                for (int i = 0; i < FIELD_SIZE; i++)
                {
                    var line = new int?[FIELD_SIZE];
                    for (int j = 0; j < FIELD_SIZE; j++)
                    {
                        if (Int32.TryParse(fieldLines[i].Substring(j * 3, 2).Trim(), out int val))
                        {
                            line[j] = val;
                        }
                    }
                    field[i] = line;
                }


            }

            public bool RemoveNumber(int number)
            {
                for (int i = 0; i < field.Length; i++)
                {
                    for (int j = 0; j < field[i].Length; j++)
                    {
                        if (field[i][j] == number)
                        {
                            field[i][j] = null;
                            if (IsWon(i, j))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            private bool IsWon(int x, int y)
            {
                bool xWon = true;
                for (int i = 0; i < FIELD_SIZE; i++)
                {
                    if (field[x][i] != null)
                    {
                        xWon = false;
                        break;
                    }
                }

                bool yWon = true;
                for (int i = 0; i < FIELD_SIZE && !xWon; i++)
                {
                    if (field[i][y] != null)
                    {
                        yWon = false;
                        break;
                    }
                }

                return xWon || yWon;
            }

            public int GetSumOfNumbers
            {
                get
                {
                    int result = 0;
                    foreach (var x in field)
                    {
                        foreach (var y in x)
                        {
                            result += y.HasValue ? y.Value : 0;
                        }
                    }

                    return result;
                }
            }
        }
    }
}
