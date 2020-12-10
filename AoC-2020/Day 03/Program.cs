using System;
using System.IO;
using System.Linq;

namespace AoC_day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var trees = new int[5];
            Console.WriteLine(trees[1]);

            for(int i = 1; i < input.Length; i++){
                var line = input[i];
                var length = line.Length;
                if(line[i % length] == '#'){ trees[0]++; }
                if(line[(i*3) % length] == '#'){ trees[1]++; }
                if(line[(i*5) % length] == '#'){ trees[2]++; }
                if(line[(i*7) % length] == '#'){ trees[3]++; }
                if(i % 2 == 0){
                    if(line[(i/2) % length] == '#'){ trees[4]++; };
                    Console.WriteLine("{0}, {1}", i, i/2);
                }
            }
            Console.WriteLine(trees[0]);
            Console.WriteLine(trees[1]);
            Console.WriteLine(trees[2]);
            Console.WriteLine(trees[3]);
            Console.WriteLine(trees[4]);
            Console.WriteLine("Tree count: {0}", trees.Aggregate(1L, (a, b) => a * b));
        }
    }
}
