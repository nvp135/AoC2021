using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace D16
{
    public class Part2
    {
        static Func<char, string> DecodeToBin = ch => Convert.ToString(Convert.ToInt16(ch.ToString(), 16), 2).PadLeft(4, '0');

        public Part2(string data)
        {
            var sb = new StringBuilder();

            foreach (var c in data)
            {
                sb.Append(DecodeToBin(c));
            }

            buffer = sb.ToString();
        }

        readonly string buffer;

        public long Calculate()
        {
            int pointer = 0;
            return Decode(ref pointer);
        }

        long Decode(ref int pointer)
        {
            var version = DecodeBits(ref pointer, 3);
            var typeId = DecodeBits(ref pointer, 3);

            if (typeId == 4)
            {
                return CalculateLiterally(ref pointer);
            }

            return CalculateOperator(ref pointer, typeId);

        }

        long CalculateOperator(ref int pointer, long typeId)
        {
            var result = new List<long>();
            int j = 0;
            var lengthTypeID = DecodeBits(ref pointer, 1);

            if (lengthTypeID == 0)
            {
                var totalLength = DecodeBits(ref pointer, 15);
                
                var endBitOfTheOperator = pointer + totalLength;
                while (pointer < endBitOfTheOperator)
                {
                    result.Add(Decode(ref pointer));
                }
            }
            else
            {
                var numberOfSubPackets = DecodeBits(ref pointer, 11);
                for (int i = 0; i < numberOfSubPackets; i++)
                {
                    result.Add(Decode(ref pointer));
                }
            }

            switch (typeId)
            {
                case 0:
                    return result.Sum(x => x);
                case 1:
                    return result.Aggregate((x, y) => x * y);
                case 2:
                    return result.Min(x => x);
                case 3:
                    return result.Max(x => x);
                case 5:
                    return result[0] > result[1] ? 1 : 0;
                case 6:
                    return result[0] < result[1] ? 1 : 0;
                case 7:
                    return result[0] == result[1] ? 1 : 0;
            }
            return 0;
        }

        long CalculateLiterally(ref int pointer)
        {
            long result = 0;
            long start = 1;
            while (start == 1)
            {
                start = DecodeBits(ref pointer, 1);
                result = result << 4 | DecodeBits(ref pointer, 4);
            }

            return result;
        }

        long DecodeBits(ref int start, int count)
        {
            var result = Convert.ToInt64(buffer.Substring(start, count), 2);
            start += count;
            return result;
        }
    }
}
