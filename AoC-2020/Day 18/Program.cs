using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_18
{
    class Program
    {
        static Regex rx = new Regex(@"\([\d\s?\+\*]+\)", RegexOptions.Compiled);
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var evaluate = RemoveParantheses(input);
            var result = evaluate.Select(x => CalculateSimplyfied(x)).Sum();
            Console.WriteLine("Part one: {0}", result);
            evaluate = RemoveParantheses(input, true);
            result = evaluate.Select(x => CalculateSimplyfiedPartTwo(x)).Sum();
            Console.WriteLine("Part two: {0}", result);
        }

        static List<string> RemoveParantheses(string[] input, bool partTwo = false){
            var result = new List<string>();
            foreach(var exp in input){
                result.Add(CalculateParan(exp, partTwo));
            }
            return result;
        }

        static string CalculateParan(string input, bool partTwo = false){
            string result = input;
            Match match = rx.Match(result);
            while(match.Success){
                if(partTwo){
                    result = result.Substring(0, match.Index) + CalculateSimplyfiedPartTwo(match.Value.Substring(1, match.Value.Length - 2)) + result.Substring(match.Index+match.Length);
                }else{
                    result = result.Substring(0, match.Index) + CalculateSimplyfied(match.Value.Substring(1, match.Value.Length - 2)) + result.Substring(match.Index+match.Length);
                }                
                match = rx.Match(result);
            }
            return result;
        }

        static long CalculateSimplyfied(string input){
            var exp = input.Split(' ');
            long result = Int64.Parse(exp[0]);
            for(int i = 1; i < exp.Length; i++){
                if(exp[i].Equals("+")){
                    result += Int64.Parse(exp[++i]); 
                }else if(exp[i].Equals("*")){
                    result *= Int64.Parse(exp[++i]);
                }
            }
            return result;
        }

        static long CalculateSimplyfiedPartTwo(string input){
            var exp = input.Split(' ');
            List<long> multiply = new List<long>();
            long temp = 0;
            for(int i = 0; i < exp.Length; i++){
                if(exp[i].Equals("+")){
                    temp += Int64.Parse(exp[++i]);
                }else if(exp[i].Equals("*")){
                    multiply.Add(temp);
                }else{
                    temp = Int64.Parse(exp[i]);
                }
            }
            multiply.Add(temp);
            return multiply.Aggregate((a, x) => a * x);
        }
    }
}
