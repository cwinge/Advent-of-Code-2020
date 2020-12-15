using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day_15
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = new int[]{12,1,16,3,11,0};
            Play(input, 2020);
            Play(input, 30000000);
        }

        static void Play(int[] input, int stop){
            Dictionary<int, (int, int)> history = new Dictionary<int, (int, int)>();
            int turn = 1, num = 0;
            for(var i = 0; i < input.Length; i++, turn++){
                num = input[i];
                history[num] = (turn, turn);
            }
            for(; turn <= stop; turn++){
                if(history.ContainsKey(num)){
                    num =  history[num].Item1 - history[num].Item2;
                }else{ num = 0;}

                if(history.ContainsKey(num)){
                    history[num] = (turn, history[num].Item1);
                }else{history[num] = (turn, turn);}                      
            }
            Console.WriteLine("{0}th number spoken: {1}", stop, num);
        }
    }
}
