using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_11
{
    class Program
    {
        static readonly (int x, int y)[] adjacent = new(int, int)[]{(-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1) };
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            PartOne(input.Select(s => s.ToArray()).ToArray());
            PartTwo(input.Select(s => s.ToArray()).ToArray());
        }

        static void PartOne(char[][] seating){
            var changes = new List<(int, int, char)>();

            do{
                changes.Clear();
                for(int row = 0; row < seating.Length; row++){
                    for(int col = 0; col < seating[0].Length; col++){
                        if(seating[row][col] == '.'){continue;}
                        var occupied = 0;                    
                        foreach((int x, int y) in adjacent){
                            try{
                                if(seating[row+x][col+y] == '#'){
                                    occupied++;
                                }
                            }catch(System.IndexOutOfRangeException exception){}                        
                        }
                        if(occupied == 0 && seating[row][col] == 'L'){
                            changes.Add((row, col, '#'));
                        }else if(occupied >= 4 && seating[row][col] == '#'){
                            changes.Add((row, col, 'L'));
                        }
                    }
                }
                foreach((int row, int col, char c) in changes){
                    seating[row][col] = c;
                }
            }while(changes.Any());
            
            Console.WriteLine("Occupied seats: {0}", seating.SelectMany(c => c ).Count(c => c == '#'));
        }

        static void PartTwo(char[][] seating){
            var changes = new List<(int, int, char)>();

            do{
                changes.Clear();
                for(int row = 0; row < seating.Length; row++){
                    for(int col = 0; col < seating[0].Length; col++){
                        if(seating[row][col] == '.'){continue;}
                        var occupied = 0;                    
                        foreach((int x, int y) in adjacent){
                            try{
                                for(int i = 1;; i++){
                                    if(seating[row+(x*i)][col+(y*i)] == '#'){
                                        occupied++;
                                        break;
                                    }else if(seating[row+(x*i)][col+(y*i)] == 'L'){break;}
                                }                                
                            }catch(System.IndexOutOfRangeException exception){}                        
                        }
                        if(occupied == 0 && seating[row][col] == 'L'){
                            changes.Add((row, col, '#'));
                        }else if(occupied >= 5 && seating[row][col] == '#'){
                            changes.Add((row, col, 'L'));
                        }
                    }
                }
                foreach((int row, int col, char c) in changes){
                    seating[row][col] = c;
                }
            }while(changes.Any());

            Console.WriteLine("Occupied seats part two: {0}", seating.SelectMany(c => c ).Count(c => c == '#'));
        }
    }
}
