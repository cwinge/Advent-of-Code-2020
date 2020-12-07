using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var rules = new Dictionary<string, Dictionary<string, int>>();
            
            foreach(var bags in input){
                var bag = bags.Split(" bags contain ");
                var canHold = bag[1].TrimEnd('.').Split(", ");
                var canContain = new Dictionary<string, int>();
                foreach(var pack in canHold){
                    var rule = pack.Split(" "); // #num = [0], adjective = [1], color = [2], bag(s) = [3]
                    if(rule.Length != 4){
                        continue;
                    }
                    var color = rule[1] + " " + rule[2];
                    canContain.Add(color, int.Parse(rule[0]));
                }
                rules.Add(bag[0], canContain);
            }

            Console.WriteLine(PartOne(new List<string>() {"shiny gold"}, rules).Distinct().Count() - 1);

            Console.WriteLine(PartTwo(rules["shiny gold"], rules));

        }

        static List<string> PartOne(List<string> cin, Dictionary<string, Dictionary<string, int>> rules){
            var colors = new List<string>();
            foreach(var color in cin){
                foreach(var rule in rules){
                    if(rule.Value.ContainsKey(color)){
                        colors.Add(rule.Key);
                    }                    
                }
            }
            if(colors.Count == 0){
                return cin;
            }
            return cin.Concat((PartOne(colors, rules))).ToList();
        }

        static int PartTwo(Dictionary<string, int> nestled, Dictionary<string, Dictionary<string, int>> rules){
            int sum = 0;
            if(nestled.Count == 0){
                sum = 1;
            }
            foreach(var kvp in nestled){
                sum += (kvp.Value * PartTwo(rules[kvp.Key], rules));
            }
            return sum;

        }

        
    }
}
