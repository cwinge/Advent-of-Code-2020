using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            string[][] rules = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Split(": ").ElementAt(1).Split(" or ")).ToArray();
            var ticket = input.SkipWhile(x => !x.Equals("your ticket:")).ElementAt(1).Split(',').Select(x => Int16.Parse(x)).ToArray();
            var nearbyTickets = input.SkipWhile(x => !x.Equals("nearby tickets:")).Skip(1).Select(x => x.Split(',').Select(y => Int16.Parse(y)).ToArray()).ToArray();
            List<(short, short)> ranges = new List<(short, short)>();
            foreach (var r in rules)
            {
                short[] v1 = r[0].Split('-').Select(x => Int16.Parse(x)).ToArray();
                short[] v2 = r[1].Split('-').Select(x => Int16.Parse(x)).ToArray();
                ranges.Add((v1[0], v1[1]));
                ranges.Add((v2[0], v2[1]));
            }

            PartOne(ranges.ToArray(), nearbyTickets);
            PartTwo(ranges.ToArray(), nearbyTickets, ticket);
        }

        static void PartOne((short, short)[] ranges, short[][] tickets)
        {
            int errorRate = 0;
            foreach (var ticket in tickets)
            {
                foreach (var num in ticket)
                {
                    bool error = true;
                    foreach (var range in ranges)
                    {
                        if (range.Item1 <= num && num <= range.Item2)
                        {
                            error = false;
                            break;
                        }
                    }
                    if (error) { errorRate += num; }
                }
            }
            Console.WriteLine("Error rate: {0}", errorRate);
        }

        static void PartTwo((short, short)[] ranges, short[][] tickets, short[] myTicket)
        {
            List<short[]> validTickets = GetValidTickets(ranges, tickets);
            List<int>[] positions = GetPositions(ranges, validTickets.ToArray());
            long prod = 1;
            for(int i = 0; i < positions.Length; i++){
                if(positions[i].First() < 6){
                    prod *= myTicket[i];
                }
            }
            Console.WriteLine("Prod of departures: {0}", prod);
        }

        static List<short[]> GetValidTickets((short, short)[] ranges, short[][] tickets)
        {
            List<short[]> validTickets = new List<short[]>();
            bool valid = true;
            for (int i = 0; i < tickets.Length; i++)
            {
                valid = true;
                foreach (var num in tickets[i])
                {
                    short validCount = 0;
                    for (int r = 0; r < ranges.Length; r+= 2)
                    {                    
                        if((ranges[r].Item1 <= num && num <= ranges[r].Item2) ||
                        (ranges[r+1].Item1 <= num && num <= ranges[r+1].Item2)){
                            validCount++;
                        }
                    }
                    if (validCount == 0)
                    {
                        valid = false;
                        break;
                    }
                    validCount = 0;
                }
                if (valid){validTickets.Add(tickets[i]);}
            }
            return validTickets;
        }

        static List<int>[] GetPositions((short, short)[] ranges, short[][] tickets)
        {
            List<int>[] positions = new List<int>[tickets[0].Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new List<int>();
                positions[i].AddRange(Enumerable.Range(0, tickets[0].Length).ToList());
            }
            foreach (var ticket in tickets)
            {
                for (int i = 0; i < tickets[0].Length; i++)
                {
                    if (positions[i].Count == 1) { continue; }
                    for (int r = 0; r < tickets[0].Length; r++)
                    {
                        if (!ValidRange(ticket[i], i, r, ranges))
                        {
                            positions[i].Remove(r);
                            if (positions[i].Count == 1)
                            {
                                RemoveForAllExcept(positions, i, positions[i].First());
                            }
                            break;
                        }
                    }
                }
            }
            return positions;
        }

        static void RemoveForAllExcept(List<int>[] positions, int except, int remove)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (i != except && positions[i].Count != 1)
                {
                    positions[i].Remove(remove);
                    if (positions[i].Count == 1)
                    {
                        RemoveForAllExcept(positions, i, positions[i].First());
                    }
                }
            }
        }

        static bool ValidRange(int num, int numIndex, int rIndex, (short, short)[] ranges)
        {
            int rangeIndex = rIndex * 2;
            if (ranges[rangeIndex].Item1 <= num && num <= ranges[rangeIndex].Item2){return true; }
            rangeIndex++;
            if (ranges[rangeIndex].Item1 <= num && num <= ranges[rangeIndex].Item2){return true; }
            return false;
        }
    }
}
