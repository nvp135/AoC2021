using System;
using System.IO;

namespace D16
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Part1();
            var data = File.ReadAllText("input.txt").ToString(); 
            Console.WriteLine(p1.Calculate(data));
            //Console.WriteLine(p1.Calculate("38006F45291200"));
            //Console.WriteLine(p1.Calculate("8A004A801A8002F478"));
            //Console.WriteLine(p1.Calculate("620080001611562C8802118E34"));
            //Console.WriteLine(p1.Calculate("C0015000016115A2E0802F182340"));
            //Console.WriteLine(p1.Calculate("A0016C880162017C3686B18A3D4780"));
            var p2 = new Part2(data);
            Console.WriteLine(p2.Calculate());
        }

    }
}
