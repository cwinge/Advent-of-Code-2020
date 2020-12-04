using System;
using System.IO;
using System.Linq;

namespace AoC_day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var entries = input.Select(int.Parse).ToArray();

            foreach(var entry1 in entries)
            {
                foreach(var entry2 in entries)
                {
                    foreach (var entry3 in entries)
                        {
                            if (entry1 + entry2 + entry3 == 2020)
                            {
                                Console.WriteLine("Entry 1: {0}, Entry 2: {1}, Entry 2: {2}, Multiplied: {3}", entry1, entry2, entry3, entry1 * entry2 * entry3);
                                return;
                            }
                        }
                }
            }
        }
    }
}
