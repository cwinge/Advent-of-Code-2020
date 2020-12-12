using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<(char, int)> input = File.ReadAllLines("input.txt").Select(x => (x[0], int.Parse(x.Substring(1))));
            PartOne(input);
            PartTwo(input);
        }

        static void PartOne(IEnumerable<(char, int)> input){
            int direction = 90; // east
            var moves = new int[]{0,0,0,0}; // NESW

            foreach(var inst in input){
                switch(inst.Item1){
                    case 'N':
                        moves[0] += inst.Item2;
                        break;
                    case 'S':
                        moves[2] += inst.Item2;
                        break;
                    case 'E':
                        moves[1] += inst.Item2;
                        break;
                    case 'W':
                        moves[3] += inst.Item2;
                        break;
                    case 'L':
                        direction = Direction(direction, -inst.Item2);
                        break;
                    case 'R':
                        direction = Direction(direction, inst.Item2);
                        break;
                    case 'F':
                        moves[(direction / 90)] += inst.Item2;
                        break;
                    default:
                        Console.WriteLine("Defaulted");
                        break;
                }
            }
            Console.WriteLine("Manhattandistance: NS: {0} + EW:{1} = {2}", Math.Abs(moves[0]-moves[2]), Math.Abs(moves[1]-moves[3]), Math.Abs(moves[0]-moves[2])+Math.Abs(moves[1]-moves[3]));
        }

        static void PartTwo(IEnumerable<(char, int)> input){
            var pos = new int[]{0,0}; // E/W, N/S
            var waypoint = new int[]{10, 1}; // E/W, N/S            

            foreach(var inst in input){
                switch(inst.Item1){
                    case 'N':
                        waypoint[1] += inst.Item2;
                        break;
                    case 'S':
                        waypoint[1] -= inst.Item2;
                        break;
                    case 'E':
                        waypoint[0] += inst.Item2;
                        break;
                    case 'W':
                        waypoint[0] -= inst.Item2;
                        break;
                    case 'L':
                        Rotate(inst.Item2, waypoint, true);
                        break;
                    case 'R':
                        Rotate(inst.Item2, waypoint, false);
                        break;
                    case 'F':
                        pos[0] += waypoint[0] * inst.Item2;
                        pos[1] += waypoint[1] * inst.Item2;
                        break;
                    default:
                        Console.WriteLine("Defaulted");
                        break;
                }
            }
            Console.WriteLine("Manhattandistance part 2: NS: {0} + EW:{1} = {2}", pos[0], pos[1], Math.Abs(pos[0]) + Math.Abs(pos[1]));
        }

        static int Direction(int current, int change){
            current = (current + change) % 360;
            return current < 360 ? (current + 360) % 360: current;
        }

        static int[] cos = new int[]{1, 0, -1, 0};
        static int[] sin = new int []{0, 1, 0, -1};
        static void Rotate(int direction, int[] waypoint, bool left){
            int x = waypoint[0];
            int y = waypoint[1];
            int d = left ? direction / 90 : 4 - (direction / 90);
            waypoint[0] = x*cos[d] - y*sin[d];
            waypoint[1] = x*sin[d] + y*cos[d];
        }
    }
}
