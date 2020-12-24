using System;
using System.Collections.Generic;
using System.IO;

namespace Day_24
{
    class Program
    {
        static HashSet<(int, int)> blackTiles = new HashSet<(int, int)>();
        static Dictionary<string, (int x, int y)> moves = new Dictionary<string, (int x, int y)>{
            {"e",  (1, 0)},
            {"w",  (-1, 0)},
            {"ne",  (0, -1)},
            {"nw",  (-1, -1)},
            {"se",  (1, 1)},
            {"sw",  (0, 1)},
            };
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt.");
            PartOne(input);
            PartTwo();
        }

        static void PartOne(string[] input)
        {
            foreach(var flip in input)
            {
                var inst = flip;
                var (x, y) = (0, 0);
                while(inst.Length > 0)
                {
                    foreach(var move in moves)
                    {
                        if (inst.StartsWith(move.Key))
                        {
                            inst = inst.Substring(move.Key.Length);
                            (x, y) = (x + move.Value.x, y + move.Value.y);
                        }
                    }
                    
                }
                if(blackTiles.Contains((x, y)))
                {
                    blackTiles.Remove((x, y));
                }
                else
                {
                    blackTiles.Add((x, y));
                }
            }
            Console.WriteLine($"Part one: {blackTiles.Count}");
        }

        static void PartTwo()
        {
            for(int i = 0; i < 100; i++)
            {
                var oldTiles = new HashSet<(int, int)>(blackTiles);
                var whiteTiles = GetWhiteTiles(oldTiles);
                foreach(var (x, y) in blackTiles)
                {
                    var count = 0;
                    foreach(var move in moves)
                    {
                        if(oldTiles.Contains((x + move.Value.x, y + move.Value.y))){
                            count++;
                        }
                    }
                    
                    if(count == 0 || count > 2)
                    {
                        blackTiles.Remove((x, y));
                    }
                }

                foreach(var (x, y) in whiteTiles)
                {
                    var count = 0;
                    foreach(var move in moves)
                    {
                        if (oldTiles.Contains((x + move.Value.x, y + move.Value.y)))
                        {
                            count++;
                        }
                    }
                    
                    if(count == 2)
                    {
                        blackTiles.Add((x, y));
                    }
                }
            }
            Console.WriteLine($"Part two: {blackTiles.Count}");
        }

        static HashSet<(int, int)> GetWhiteTiles(HashSet<(int, int)> tiles)
        {
            HashSet<(int, int)> whiteTiles = new HashSet<(int, int)>();
            foreach(var (x,y) in tiles)
            {
                foreach(var move in moves)
                {
                    if(!tiles.Contains((x + move.Value.x, y + move.Value.y))){
                        whiteTiles.Add((x + move.Value.x, y + move.Value.y));
                    }
                }
            }

            whiteTiles.ExceptWith(tiles);
            return whiteTiles;
        }
    }
}
