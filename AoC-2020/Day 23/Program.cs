using System;
using System.Linq;
using System.Collections.Generic;

namespace Day_23
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "0739862541".Select(x => Int16.Parse(x.ToString())).ToList(); // padded with 0
            short[] cups = new short[10]; // index = cup label, value = next cup in circle
            int[] cupsPartTwo = new int[1000001];
            for(short i = 1; i < input.Count;i++)
            {                
                if(i == 9)
                {
                    cups[input[i]] = input[1];
                    cupsPartTwo[input[i]] = 10;
                }
                else
                {
                    cups[input[i]] = input[i+1];
                    cupsPartTwo[input[i]] = input[i+1];
                }                
            }

            for(int i = 10; i < 1000000; i++)
            {
                cupsPartTwo[i] = i + 1 ;
            }
            cupsPartTwo[1000000] = 7;

            PartOne(cups);
            PartTwo(cupsPartTwo);
        }

        static void PartOne(short[] cups)
        {            
            short current = 7, next, move1, move2, move3;

            for(int i = 0; i < 100; i++)
            {
                next = current;
                move1 = cups[current];
                move2 = cups[move1];
                move3 = cups[move2];

                do
                {
                    next--;
                    if(next == 0) { next = 9; }
                } while (move1 == next || move2 == next || move3 == next);
                cups[current] = cups[move3];
                cups[move3] = cups[next];
                cups[next] = move1;

                current = cups[current];
            }

            Console.Write("Part one: ");
            current = cups[1];
            for(int i = 0; i < 8; i++)
            {
                Console.Write(current);
                current = cups[current];
            }
            Console.WriteLine();            
        }

        static void PartTwo(int[] cups)
        {
            int current = 7, next, move1, move2, move3;

            for (int i = 0; i < 10000000; i++)
            {
                next = current;
                move1 = cups[current];
                move2 = cups[move1];
                move3 = cups[move2];

                do
                {
                    next--;
                    if (next == 0) { next = 1000000; }
                } while (move1 == next || move2 == next || move3 == next);
                cups[current] = cups[move3];
                cups[move3] = cups[next];
                cups[next] = move1;

                current = cups[current];
            }

            Console.Write("Part two: {0}", (long)cups[1]*cups[cups[1]]);
        }
    }
}
