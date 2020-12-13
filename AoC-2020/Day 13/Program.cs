using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var time = int.Parse(input[0]);
            int[] buses = input[1].Split(',').Where(x => x != "x").Select(x => int.Parse(x)).ToArray();
            PartOne(time, buses);
            PartTwo(input[1].Split(','));
        }

        static void PartOne(int time, int[] buses){
            int min = 0, id = 0;
            bool notFound = true;
            for(int i = time; notFound; i++){
                foreach(var bus in buses){             
                    if(i % bus == 0){notFound = false; id = bus; min = i - time; break;}
                }
            }            
            Console.WriteLine("Waiting time * busID = {0}", min*id);
        }

        static void PartTwo(string[] buses){
            long time = 0;
            long inc = long.Parse(buses[0]);
            for (var i = 1; i < buses.Length; i++)
            {
                if (!buses[i].Equals("x"))
                {
                    var bus = int.Parse(buses[i]);
                    while (true)
                    {
                        time += inc;
                        if ((time + i) % bus == 0)
                        {
                            inc *= bus;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("Earliest matching offset time: {0}", time);
        }
    }
}
