using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_21
{
    class Program
    {
        static List<string> alergens = new List<string>();
        static List<string> ingredients = new List<string>();
        static Dictionary<string, List<string>> alergenRecipes = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            PartOne(input);
            PartTwo();
        }

        static void PartOne(string[] input)
        {
            foreach(var recipe in input)
            {
                var split = recipe.Split(" (contains ");
                var recipeIngredients = split[0].Split(" ").ToList();
                ingredients.AddRange(split[0].Split(" "));
                var recipeAlergens = split[1].Trim(')').Split(", ");

                foreach(var alergen in recipeAlergens)
                {
                    if (alergenRecipes.ContainsKey(alergen))
                    {
                        alergenRecipes[alergen] = new List<string>(alergenRecipes[alergen].Intersect(recipeIngredients));
                    }
                    else
                    {
                        alergenRecipes[alergen] = recipeIngredients;
                    }
                }
            }            

            foreach (var recipe in alergenRecipes.OrderBy(x => x.Value.Count))
            {
                if(recipe.Value.Count == 1) { 
                    alergens.AddRange(recipe.Value);
                    foreach(var r in alergenRecipes)
                    {
                        if(r.Key != recipe.Key && r.Value.Contains(recipe.Value.First()))
                        {
                            r.Value.Remove(recipe.Value.First());
                        }
                    }
                }
                else
                {
                    var reduced = recipe.Value.Except(alergens).ToList();
                    alergens.AddRange(reduced);
                    alergenRecipes[recipe.Key] = new List<string>(reduced);
                }
            }

            alergens.ForEach(x => ingredients.RemoveAll(y => y.Equals(x)));
            Console.WriteLine($"Part one: {ingredients.Count}");
        }

        static void PartTwo()
        {
            string CDIlist = "";
            var ordered = alergenRecipes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach(var alergenRecipe in ordered)
            {
                if (alergenRecipe.Value.Count == 0) {continue;}                
                CDIlist += "," + string.Join(",", alergenRecipe.Value);
            }
            
            Console.WriteLine($"Part two: {CDIlist.Trim(',')}");
        }
    }
}