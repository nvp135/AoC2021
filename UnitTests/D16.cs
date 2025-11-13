using D16;
using NUnit.Framework.Legacy;

namespace UnitTests
{
    public class Tests
    {
        D16.Part1 d16p1;
        D16.Part2 d16p2;

        [SetUp]
        public void Setup()
        {
            d16p1 = new D16.Part1();
        }

        [Test]
        public void Test1()
        {
            //Assert.That(d16p1.Calculate("D2FE28"), Is.EqualTo(2021));
            Assert.That(d16p1.Calculate("8A004A801A8002F478"), Is.EqualTo(16));
            Assert.That(d16p1.Calculate("620080001611562C8802118E34"), Is.EqualTo(12));
            Assert.That(d16p1.Calculate("C0015000016115A2E0802F182340"), Is.EqualTo(23));
            Assert.That(d16p1.Calculate("A0016C880162017C3686B18A3D4780"), Is.EqualTo(31));

            d16p2 = new D16.Part2("C200B40A82");
            Assert.That(d16p2.Calculate(), Is.EqualTo(3));
            d16p2 = new D16.Part2("04005AC33890");
            Assert.That(d16p2.Calculate(), Is.EqualTo(54));
            d16p2 = new D16.Part2("880086C3E88112");
            Assert.That(d16p2.Calculate(), Is.EqualTo(7));
            d16p2 = new D16.Part2("CE00C43D881120");
            Assert.That(d16p2.Calculate(), Is.EqualTo(9));
            d16p2 = new D16.Part2("D8005AC2A8F0");
            Assert.That(d16p2.Calculate(), Is.EqualTo(1));
            d16p2 = new D16.Part2("F600BC2D8F");
            Assert.That(d16p2.Calculate(), Is.EqualTo(0));
            d16p2 = new D16.Part2("9C005AC2F8F0");
            Assert.That(d16p2.Calculate(), Is.EqualTo(0)); 
            d16p2 = new D16.Part2("9C0141080250320F1802104A08");
            Assert.That(d16p2.Calculate(), Is.EqualTo(1));

            Assert.Pass();
        }
    }
}
