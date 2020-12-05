using System;
using System.IO;
using System.Collections.Generic;

namespace AoC_day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine(getMaxID(input)); // Part 1
            Console.WriteLine(getMySeat(getSeatIDs(input))); // Part 2            
        }
        static int getSeatID(string seat){
            int rowMin = 0;
            int rowMax = 127;
            for(int i = 0; i < 7; i++){
                if(seat[i] == 'F'){
                    rowMax -= (rowMax-rowMin) / 2 + 1;
                }else{
                    rowMin += (rowMax-rowMin) / 2 + 1;
                }
            }

            int colMin = 0;
            int colMax = 7;
            for(int i = 7; i < 10; i++){
                if(seat[i] == 'L'){
                    colMax -= (colMax-colMin) / 2 + 1;
                }else{
                    colMin += (colMax-colMin) / 2 + 1;
                }
            }
            return rowMin * 8 + colMin;
        }

        static int getMaxID(string[] input){
            int max = 0;
            foreach(var seat in input){
                int seatID = getSeatID(seat);
                if(seatID > max){
                    max = seatID;
                }
            }
            return max;
        }
        static List<int> getSeatIDs(string[] input){
            var list = new List<int>();
            foreach(var seat in input){
                list.Add(getSeatID(seat));
            }
            list.Sort();
            return list;
        }
        static int getMySeat(List<int> seats){
            for(int i = 1; i < seats.Count - 1; i++){
                if(!(seats[i] == seats[i-1] + 1 && seats[i] == seats[i+1] - 1)){
                    return seats[i] + 1;
                }
            }
            return -1;
        }
    }
}
