using System;
using System.IO;
using System.Linq;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(x => int.Parse(x)).Append(0).OrderBy(x => x).ToArray();
            Console.WriteLine("{0}", PartOne(input));
            Console.WriteLine("{0}", PartTwo(input));
        }

        static int PartOne(int[] input){
            int one = 0, three = 0, jump = 0, last = 0;
            foreach(int jolt in input){
                jump = jolt - last;
                last = jolt;
                if(jump == 1){one++;}
                else if(jump == 3){three++;}
            }
            return one * (three + 1);
        }

        static long PartTwo(int[] input){
            var used = new long[input.Length];
            used[0] = 1;
            for(int i = 1; i < input.Length; i++){
                foreach(var j in Enumerable.Range(0, i)){
                    if(input[i] - input[j] <= 3){
                        used[i] += used[j];
                    }
                }
            }
            return used.Last();
        }
    }
}
