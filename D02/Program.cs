using System;
using System.IO;

namespace D02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CalculatePosition("input.txt"));
            Console.WriteLine(CalculatePositionPartTwo("input.txt"));
            Console.ReadKey();
        }

        static int CalculatePosition(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                int horizontal = 0, depth = 0;
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    var values = str.Split(' ');
                    switch (values[0])
                    {
                        case "forward":
                            horizontal += Convert.ToInt32(values[1]);
                            break;
                        case "down":
                            depth += Convert.ToInt32(values[1]);
                            break;
                        case "up":
                            depth -= Convert.ToInt32(values[1]);
                            break;
                        default:
                            break;
                    }
                }
                return horizontal * depth;
            }
        }
        
        static int CalculatePositionPartTwo(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                int horizontal = 0, depth = 0, aim = 0;
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    var values = str.Split(' ');
                    switch (values[0])
                    {
                        case "forward":
                            int units = Convert.ToInt32(values[1]);
                            horizontal += units;
                            depth += aim * units;
                            break;
                        case "down":
                            aim += Convert.ToInt32(values[1]);
                            break;
                        case "up":
                            aim -= Convert.ToInt32(values[1]);
                            break;
                        default:
                            break;
                    }
                }
                return horizontal * depth;
            }
        }
    }
}
