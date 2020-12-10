using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            FirstProblem(input);
            SecondProblem(input);
        }

        static void FirstProblem(string[] input){
            var reqFields = new[]{"ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"};
            string data = "";
            int valid = 0;
            
            foreach(var entry in input){
                data += entry;
                if(String.IsNullOrWhiteSpace(entry)){                    
                    if(reqFields.All(c => data.Contains(c))){
                        valid++;
                    }
                    data = "";
                }
            }
            // Ugly but efficient
            if(reqFields.All(c => data.Contains(c))){
                        valid++;
            }

            Console.WriteLine("Valid passports: {0}", valid);
        }

        static void SecondProblem(string[] input){
            var reqFields = new[]{"ecl:", "pid:", "eyr:", "hcl:", "byr:", "iyr:", "hgt:"};
            string data = "";
            int valid = 0;
            
            foreach(var entry in input){
                data += entry;
                if(String.IsNullOrWhiteSpace(entry)){                    
                    if(reqFields.All(c => data.Contains(c))){
                        if(validateAll(data)){
                            valid++;
                        }
                    }
                    data = "";
                }
            }

            Console.WriteLine("Valid(ated) passports: {0}", valid);
        }

        static bool validateAll(string data){
            if(!validateEcl(data)){ }
            else if(!validatePid(data)){}
            else if(!validateEyr(data)){}
            else if(!validateHcl(data)){}
            else if(!validateByr(data)){}
            else if(!validateIyr(data)){}
            else if(!validateHgt(data)){}
            else {return true;}
            return false;
        }

        static bool validateEcl(string data){
            var index = data.IndexOf("ecl:") + 4;
            var info = data.Substring(index, 3);   
            var valid = new[]{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            if(valid.Any(c => info.Contains(c))){
                return true;
            }else{
                return false;
            }
        }

        static bool validatePid(string data){
            var index = data.IndexOf("pid:") + 4;
            string info = "";
            try{
                info = data.Substring(index, 9);
            }catch{
                return false;
            }
            int v;
            if(int.TryParse(info, out v)){
                return true;
            }else{
                return false;
            }            
        }

        static bool validateEyr(string data){
            var index = data.IndexOf("eyr:") + 4;
            var info = data.Substring(index, 4);
            int year;
            if(int.TryParse(info, out year)){
                if(year >= 2020 && year <= 2030){
                    return true;
                }               
            }
            return false;
        }

        static bool validateHcl(string data){
            var index = data.IndexOf("hcl:") + 4;
            string info = "";
            try{
                info = data.Substring(index, 7);
            }
            catch{
                return false;
            }
            if(Regex.Match(info, "^#[a-f0-9]{6}$").Success){
                return true;
            }
            else{
                return false;
            }
        }

        static bool validateByr(string data){
            var index = data.IndexOf("byr:") + 4;
            var info = data.Substring(index, 4);
            int year;
            if(int.TryParse(info, out year)){
                if(year >= 1920 && year <= 2002){
                    return true;
                }              
            }
            return false;

        }

        static bool validateIyr(string data){
            var index = data.IndexOf("iyr:") + 4;
            var info = data.Substring(index, 4);
            int year;
            if(int.TryParse(info, out year)){
                if(year >= 2010 && year <= 2020){
                    return true;
                }
            }
             return false;

        }

        static bool validateHgt(string data){
            var index = data.IndexOf("hgt:") + 4;
            int len = 0;
            if(int.TryParse(data.Substring(index, 3), out len)){
                if(data.Substring(index+3, 2) == "cm"){
                    if(len >= 150 && len <= 193){
                        return true;
                    }
                }
            }else if(data.Substring(index+2, 2) == "in"){
                if(int.TryParse(data.Substring(index, 2), out len)){
                    if(len >= 59 && len <= 76){
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
