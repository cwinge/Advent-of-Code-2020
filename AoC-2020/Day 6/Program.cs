using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AoC_day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            PartOne(input);
            PartTwo(input);
        }

        static void PartOne(string[] input){
            Console.WriteLine("Sum of yes reponses: {0}", SumYesCount(GroupAnswers(input)));            
        }

        static void PartTwo(string[] input){
            Console.WriteLine("Unanimous yes count: {0}", SumUnanimousYesCount(GroupAnswers(input)));
        }

        static List<string> GroupAnswers(string[] input){
            var groups = new List<string>();
            string answer = "";
            for(int i = 0; i < input.Length; i++){
                if(String.IsNullOrWhiteSpace(input[i])){
                    groups.Add(answer);
                    answer = "";
                }else if(i == (input.Length-1)){
                    answer += input[i] + " ";
                    groups.Add(answer);
                }else{
                    answer += input[i] + " ";
                }
            }
            return groups;
        }

        static int SumYesCount(List<string> input){
            return input.Aggregate(0, (a, b) => a + b.Distinct().Count() - 1);
        }

        static int SumUnanimousYesCount(List<string> input){
            int sum = 0;
            foreach(var group in input){
                var answers = group.Replace(" ", "");
                var size = group.Length - answers.Length;
                sum += answers.GroupBy(c => c).Select(x => x).Where(s => s.Count() == size).Count();
            }
            return sum;
        }
    }
}
