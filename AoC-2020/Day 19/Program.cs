using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Day_19
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            Console.WriteLine("Part one: {0}", CountValidMessages(input));
            Console.WriteLine("Part two: {0}", CountValidMessages(input, true));            
        }

       static int CountValidMessages(string[] input, bool isPartTwo = false)
        {
            var (rules, messages) = ParseInput(input);

            if (isPartTwo){
                rules[8] = "c";
                rules[11] = "d";
            }

            Simplify(rules);
            var startingRule = rules[0];   

            if (isPartTwo){
                startingRule = startingRule.Replace("c", $"(?:{rules[42]})+")
                    .Replace("d", $"(?<DEPTH>{rules[42]})+(?<-DEPTH>{rules[31]})+(?(DEPTH)(?!))");
            }

            return messages.Count(new Regex($"^{startingRule}$", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace).IsMatch);
        }

        static (Dictionary<int, string>, List<string>) ParseInput(IEnumerable<string> input)
        {
            var rules = new Dictionary<int, string>();
            var messages = new List<string>();
            var isRule = true;

            foreach (var entry in input)
            {
                if (string.IsNullOrEmpty(entry))
                {
                    isRule = false;
                }else if (isRule)
                {
                    var rule = entry.Split(':');
                    if(rule[1][1] == '"'){ // a or b
                        rules[int.Parse(rule[0])] = rule[1][2..^1];
                    }else{ // num
                        rules[int.Parse(rule[0])] = rule[1][1..];
                    }           
                }
                else{
                    messages.Add(entry);
                }
            }

            return (rules, messages);
        }


        private static void Simplify(Dictionary<int, string> rules)
        {
            var processed = new HashSet<int>();
            foreach (var (rule, ruleChain) in rules){
                if(ruleChain.Length == 1){processed.Add(rule);}
            }                
            
            while (rules.Count !=processed.Count)
            {
                foreach (var (rule, ruleChain) in rules)
                {
                    if (processed.Contains(rule)){continue;}                        

                    var simple = true;
                    rules[rule] = Regex.Replace(ruleChain, @"\d+", match =>
                    {
                        var matchedRule = int.Parse(match.Value);
                        if (processed.Contains(matchedRule)){return $"(?:{rules[matchedRule]})";}                            

                        simple = false;
                        return match.Value;
                    });

                    if (simple)
                        processed.Add(rule);
                }
            }
        }        
    }
}
