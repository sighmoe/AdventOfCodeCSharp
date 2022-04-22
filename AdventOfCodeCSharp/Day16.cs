using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCodeCSharp
{
    class Day16
    {
        private static int versionSum = 0;

        public static void Solve1()
        {
            string input = Util.ReadLines("C:\\Users\\kevin\\source\\repos\\AdventOfCode\\Input\\Day16\\input.txt")[0];
            string binary = Util.HexToBinary(input);
            Console.WriteLine("Hex: {0} Binary: {1}", input, binary);
            (ulong accum, int read) = ParsePacket(binary);
            Console.WriteLine("Version sum: {0}", versionSum);
            Console.WriteLine("Accum: {0}", accum);
        }

        private static ulong processOperator(List<ulong> values, int typeId)
        {
            switch (typeId)
            {
                case 0:
                    ulong sum = 0;
                    foreach (ulong i in values)
                    {
                        sum += i;
                    }
                    return sum;
                case 1:
                    ulong prod = 1;
                    foreach (ulong i in values)
                    {
                        prod *= i;
                    }
                    return prod;
                case 2:
                    return values.Min();
                case 3:
                    return values.Max();
                case 5:
                    Debug.Assert(values.Count == 2);
                    (ulong item1, ulong item2) = (values[0], values[1]);
                    return item1 > item2 ? 1UL : 0UL;
                case 6:
                    Debug.Assert(values.Count == 2);
                    (ulong item3, ulong item4) = (values[0], values[1]);
                    return item3 < item4 ? 1UL : 0UL;
                case 7:
                    Debug.Assert(values.Count == 2);
                    (ulong item5, ulong item6) = (values[0], values[1]);
                    return item5 == item6 ? 1UL : 0UL;
                default:
                    Console.WriteLine("CORRUPTED");
                    return 99999UL;
            }
        }

        private static (ulong,int) ParsePacket(string binary)
        {
            string payload;
            (int version, int typeId) = ParseHeader(binary);
            payload = binary[6..];

            if (typeId == 4)
            {
                (ulong literal, int read) = ParseLiteral(payload);
                return (literal,6+read);
            }
            else
            {
                (ulong ret, int read) = ParseOperator(payload, typeId);
                return (ret, 6 + read);
            }

        }

        private static (ulong, int) ParseSubpacketByLength(string binary, int length, int typeId)
        {
            Console.WriteLine("Parsing operator subpacket by length with length {0} and payload: {1}", length, binary);
            string payload = binary;
            int read = 0;
            int readThisPacket;
            ulong val;
            List<ulong> values = new List<ulong>();
            while (read != length)
            {
                (val, readThisPacket) = ParsePacket(payload);
                values.Add(val);
                read += readThisPacket;
                payload = payload[readThisPacket..];
            }
            ulong ret = processOperator(values, typeId);
            return (ret, length);
        }

        private static (ulong, int) ParseSubpacketsByNum(string binary, int num, int typeId)
        {
            Console.WriteLine("Parsing operator subpacket by num with num {0} and payload: {1}", num, binary);
            string payload = binary;
            int count = 0;
            int readThisPacket;
            ulong val;
            int read = 0;
            List<ulong> values = new List<ulong>();
            while (count != num)
            {
                (val,readThisPacket) = ParsePacket(payload);
                values.Add(val);
                read += readThisPacket;
                count++;
                payload = payload[readThisPacket..];
            }
            ulong ret = processOperator(values, typeId);
            return (ret, read);
        }

        private static (ulong,int) ParseOperator(string binary, int typeId)
        {
            Console.WriteLine("Parsing operator packet with payload: {0}", binary);
            string payload;
            char bLengthTypeId = binary[0];
            if (bLengthTypeId == '0')
            {
                string bSubpacketsLength = binary[1..16];
                int subpacketsLength = Util.FromBinary(bSubpacketsLength);
                payload = binary[16..];
                (ulong ret, int read) = ParseSubpacketByLength(payload, subpacketsLength, typeId);
                return (ret, 16 + read);
            } 
            else
            {
                string bNumOfSubpackets = binary[1..12];
                int numOfSubpackets = Util.FromBinary(bNumOfSubpackets);
                payload = binary[12..];
                (ulong ret, int read) = ParseSubpacketsByNum(payload, numOfSubpackets, typeId);
                return (ret, 12+read); 
            }
        }

        private static (ulong ,int) ParseLiteral(string binary)
        {
            Console.WriteLine("Parsing literal packet w/ payload: {0}.", binary);
            char startBit = binary[0];
            string acc = "";
            // Console.WriteLine("Literal payload: {0}", binary);

            while (startBit != '0')
            {
                acc += binary[1..5];
                binary = binary[5..];
                startBit = binary[0];
            }
            acc += binary[1..5];
            ulong literal = Util.FromBinaryUL(acc);
            int read = (acc.Length + (acc.Length / 4));
            Console.WriteLine("bLiteral: {0} Literal: {1} Read: {2}", acc, literal, read);
            return (literal,read);
        }

        private static (int,int) ParseHeader(string binary)
        {
            (string bVersion, string bTypeId) = (binary[0..3], binary[3..6]);
            (int version, int typeId) = (Util.FromBinary(bVersion), Util.FromBinary(bTypeId));
            versionSum += version;
            Console.WriteLine("bVersion: {0} Version: {1} bTypeId: {2} TypeId: {3}", bVersion, version, bTypeId, typeId);
            return (version, typeId);
        }
    }
}
