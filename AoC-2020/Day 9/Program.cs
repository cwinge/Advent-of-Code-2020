using System;
using System.IO;
using System.Linq;

namespace Day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(x => long.Parse(x)).ToArray();
            var found = PartOne(input);
            PartTwo(input, found);
        }

        static long PartOne(long[] input){
            for(int i = 25; i < input.Length; i++){
                if(!validNumber(input, i)){
                    Console.WriteLine("Found {0}", input[i]);
                    return input[i];
                }
            }
            return -1;
        }

        static void PartTwo(long[] input, long target){
            for(int i = 0; i < input.Length -1; i++){
                long sum = 0;
                for(int j = i; j < input.Length; j++){
                    sum += input[j];
                    if(sum > target){break;}
                    if(sum == target){
                        long min = long.MaxValue, max = 0;
                        while(i <= j){
                            min = Math.Min(input[i], min);
                            max = Math.Max(input[i], max);
                            i++;
                        }
                        Console.WriteLine("Found XMAS weakness: {0}", min+max);
                        return;
                    }
                }
            }
        }

        static bool validNumber(long[] input, int index){
            for(int i = index - 25; i < index; i++){
                for(int j = index - 25; j < index; j++){
                    if(input[i] + input[j] == input[index]){
                        if(input[i] != input[j]){
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
