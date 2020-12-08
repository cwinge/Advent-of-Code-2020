using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_8
{
    class Program
    {
        static int acc = 0;
        static bool found = false;
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            for(int i = 0; i < input.Length && !found; i++){
                runInstructions(swapInstructions(input, i));
                swapInstructions(input, i);
            }
        }

        static void runInstructions(string[] input){
            var ran = new HashSet<int>();
            acc = 0;
            
            for(int i = 0; i < input.Length;){                
                if(ran.Contains(i)){return;}
                ran.Add(i);
                switch(input[i][0]){
                    case 'a':
                        acc += int.Parse(input[i].Substring(4));
                        i++;
                        break;
                    case 'n':
                        i++;
                        break;
                    case 'j':
                        i += int.Parse(input[i].Substring(4));
                        break;
                    default: 
                        Console.WriteLine("Default...2");
                        break;
                }                
            }
            Console.WriteLine("Found solution: {0}", acc); found = true;
        }

        static string[] swapInstructions(string[] input, int i){
            switch(input[i][0]){
                case 'a':
                    break;
                case 'n':
                    input[i] = input[i].Replace("nop", "jmp");
                    break;
                case 'j':
                    input[i] = input[i].Replace("jmp", "nop");
                    break;
                default: 
                    Console.WriteLine("Default...");
                    break;
            }
            return input;
        }  
    } 
}
