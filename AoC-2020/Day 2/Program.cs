using System;
using System.IO;
using System.Linq;

namespace AoC_day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            FirstProblem(input);
            SecondProblem(input);
        }

        static void FirstProblem(string[] input)
        {
            int valid = 0;
            foreach(var line in input)
            {
                var col = line.Split(" ");

                var bounds = col[0].Split("-");
                var min = int.Parse(bounds[0]);
                var max = int.Parse(bounds[1]);

                var c = col[1].Split(":")[0].First();

                var password = col[2];

                var count = password.Count(_ => _ == c);
                if (!(count < min || count > max))
                {
                    valid++;
                }

            }
            Console.WriteLine("Number of valid #1 passwords: {0}", valid);
        }

        static void SecondProblem(string[] input)
        {
            int valid = 0;
            foreach(var line in input)
            {
                var col = line.Split(" ");

                var charPosition = col[0].Split("-");
                var pos1 = int.Parse(charPosition[0]) - 1;
                var pos2 = int.Parse(charPosition[1]) - 1;

                var c = col[1].Split(":")[0].First();

                var password = col[2];

                if(password[pos1] != password[pos2] && (password[pos1] == c || password[pos2] == c)){
                    Console.WriteLine("line: {0}, c: {1}, pos1: {2}, pos2: {3}", line, c, pos1, pos2);
                    valid++;
                }

            }
            Console.WriteLine("Number of valid #2 passwords: {0}", valid);
        }

    }
}
