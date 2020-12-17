using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day_17
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            PartOne(input);
            PartTwo(input);
        }

        static void PartOne(string[] input){
            var world = GenerateStartWorld(input); 
            for(int i = 0; i < 6; i++){
                world = StateChange(world);
            }
            Console.WriteLine("Part 1 - Active states: {0}", world.Count.ToString());
        }

        static void PartTwo(string[] input){
            var world = GenerateStartWorld(input); 
            for(int i = 0; i < 6; i++){
                world = StateChange(world, true);
            }
            Console.WriteLine("Part 2 - Active states: {0}", world.Count.ToString());
        }

         static ImmutableHashSet<(int, int, int, int)> GenerateStartWorld(string[] input)
        {
            var activeCoords = new HashSet<(int, int, int, int)>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if(input[y][x].Equals('#'))
                    {
                        activeCoords.Add((x, y, 0, 0));
                    }
                }
            }
            return activeCoords.ToImmutableHashSet();
        }

        static ImmutableHashSet<(int, int, int, int)> StateChange(ImmutableHashSet<(int, int, int, int)> inState, bool partTwo = false){
            var activeCoords = new HashSet<(int, int, int, int)>();
            var moore = GenerateMooreNeighborhood(partTwo);

            foreach(var pos in inState){
                int activeNeighbors = GetNeighborPositions(moore, pos).Count(nPos => inState.Contains(nPos));                
                if(activeNeighbors == 2 || activeNeighbors == 3){activeCoords.Add(pos);}
            }
            foreach(var pos in inState){
                var inactiveNeighbors = GetNeighborPositions(moore, pos).Where(nPos => !inState.Contains(nPos)).Distinct(); 

                foreach(var inactive in inactiveNeighbors){
                    var activeNeighbors = GetNeighborPositions(moore, inactive).Count(nPos => inState.Contains(nPos));
                    
                    if(activeNeighbors == 3){activeCoords.Add(inactive);}
                }                
            }

            return activeCoords.ToImmutableHashSet();
        }

        static IEnumerable<(int, int, int, int)> GetNeighborPositions(ImmutableList<(int, int, int, int)> moore, (int x, int y, int z, int w) pos){
            return moore.Select(n => (pos.x+n.Item1, pos.y+n.Item2, pos.z+n.Item3, pos.w+n.Item4));
        }

        // https://en.wikipedia.org/wiki/Moore_neighborhood
        static ImmutableList<(int, int, int, int)> GenerateMooreNeighborhood(bool partTwo = false)
        {
            var positions = new List<(int, int, int, int)>();

            for(int x = -1; x <= 1; x++){
                for(int y = -1; y <= 1; y++){
                    for(int z = -1; z <= 1; z++){
                        if(partTwo){
                            for(int w = -1; w <= 1; w++){
                                positions.Add((x,y,z,w));
                            }
                        }else{
                            positions.Add((x,y,z,0));
                        }
                    }
                }
            }            
            positions.Remove((0,0,0,0));
            
            return positions.ToImmutableList();
        }        
    }
}
