using System;
using System.Text;
using System.Threading;

namespace D16
{
    public class Part1
    {
        static Func<char, string> DecodeToBin = ch => Convert.ToString(Convert.ToInt16(ch.ToString(), 16), 2).PadLeft(4, '0');

        public Part1() { }

        public int Calculate(string data)
        {
            var sb = new StringBuilder();

            foreach (var c in data)
            {
                sb.Append(DecodeToBin(c));
            }

            var binData = sb.ToString();

            int pointer = 0;

            return Decode(ref binData, ref pointer);
        }

        int Decode(ref string transmission, ref int pointer)
        {
            int result = 0;

            var version = DecodeBits(ref transmission, ref pointer, 3);

            result += version;
            var typeId = DecodeBits(ref transmission, ref pointer, 3);

            switch (typeId)
            {
                case 4:
                    result += CalculateLiterally(ref transmission, ref pointer);
                    break;
                default:
                    result += CalculateOperator(ref transmission, ref pointer);
                    break;
            }
            return result;

        }

        int CalculateOperator(ref string transmission, ref int pointer)
        {
            int result = 0;
            var lengthTypeID = DecodeBits(ref transmission, ref pointer, 1);

            if (lengthTypeID == 0)
            {
                var totalLength = DecodeBits(ref transmission, ref pointer, 15);
                
                var endBitOfTheOperator = pointer + totalLength;
                while (pointer < endBitOfTheOperator)
                {
                    result += Decode(ref transmission, ref pointer);
                }
            }
            else
            {
                var numberOfSubPackets = DecodeBits(ref transmission, ref pointer, 11);
                for (int i = 0; i < numberOfSubPackets; i++)
                {
                    result += Decode(ref transmission, ref pointer);
                }
            }

            return result;
        }

        int CalculateLiterally(ref string transmission, ref int pointer)
        {
            int result = 0;
            int start = 1;
            while (start == 1)
            {
                start = DecodeBits(ref transmission, ref pointer, 1);
                result = result << 4 | DecodeBits(ref transmission, ref pointer, 4);
            }

            return 0;// result;
        }

        int DecodeBits(ref string transmission, ref int start, int count)
        {
            var result = Convert.ToInt32(transmission.Substring(start, count), 2);
            start += count;
            return result;
        }
    }
}
