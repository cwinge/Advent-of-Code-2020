using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            PartOne(input);
            PartTwo(input);
        }

        static void PartOne(string[] input)
        {
            var mem = new Dictionary<string, long>();
            string mask = "";
            foreach (var line in input)
            {
                var l = line.Split(" = ");
                if (l[0].Equals("mask")) { mask = l[1]; }
                else
                {
                    mem[l[0]] = ApplyMask(l[1], mask);
                }
            }
            Console.WriteLine("Part one: {0}", mem.Values.Sum());
        }

        static void PartTwo(string[] input)
        {
            var mem = new Dictionary<string, long>();
            string mask = "";
            foreach (var line in input)
            {
                var l = line.Split(" = ");
                if (l[0].Equals("mask")) { mask = l[1]; }
                else
                {
                    ApplyMaskMem(l[0], l[1], mask, mem);
                }
            }
            Console.WriteLine("Part two: {0}", mem.Values.Sum());
        }

        static void GetAddressRange(string input, out int first, out int second)
        {
            first = input.IndexOf('[') + 1;
            second = input.IndexOf(']');
        }

        static void ApplyMaskMem(string adr, string value, string mask, Dictionary<string, long> mem)
        {
            int x, y;
            GetAddressRange(adr, out x, out y);
            string address = adr.Substring(x, y - x);
            var binary = Convert.ToString(Convert.ToInt32(address), 2).PadLeft(mask.Length, '0').ToCharArray();
            var indexes = new List<int>();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0') { }
                else if (mask[i] == '1') { binary[i] = '1'; }
                else { indexes.Add(i); }
            }
            long v = Int64.Parse(value);
            GeneratePermutations(binary, indexes, mem, v, indexes.Count() - 1);
        }

        static void GeneratePermutations(char[] binary, List<int> indexes, Dictionary<string, long> mem, long value, int remaining)
        {
            if (remaining < 0)
            {
                string key = new string(binary);
                mem[key] = value;
                return;
            }

            char[] binaryZero = (char[])binary.Clone();
            binaryZero[indexes[remaining]] = '0';
            char[] binaryOne = (char[])binary.Clone();
            binaryOne[indexes[remaining]] = '1';
            remaining--;
            GeneratePermutations(binaryZero, indexes, mem, value, remaining);
            GeneratePermutations(binaryOne, indexes, mem, value, remaining);
        }

        static long ApplyMask(string num, string mask)
        {
            var binary = Convert.ToString(Convert.ToInt32(num), 2).PadLeft(mask.Length, '0').ToCharArray();
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '0') { binary[i] = '0'; }
                else if (mask[i] == '1') { binary[i] = '1'; }
            }
            string b = new string(binary);
            return Convert.ToInt64(b, 2);
        }
    }
}
