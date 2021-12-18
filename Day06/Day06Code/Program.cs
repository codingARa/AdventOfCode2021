using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day06Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day06!");

            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();
            Dictionary<int, int> Population = ParseInputPart1(inputStrings[0]);
            int answer1 = CountPopulationAfterNGenerations(Population, 80);
            Console.WriteLine($"answer to part 1: {answer1}");
        }
        public static Dictionary<int,int> ParseInputPart1(string inputString) {
            Dictionary<int, int> Population = new();
            foreach(int num in Enumerable.Range(0,9)) {
                char[] cArray = num.ToString().ToCharArray();
                Population.Add(num, inputString.Count(x => x == cArray[0]));
            }
            return Population;
        }
        public static int CountPopulationAfterNGenerations(Dictionary<int,int> Population, int nGenerations) {
            for (int n = 0; n < nGenerations; n++) {
                Population = GenerationStep(Population);
            }
            return Population.Values.ToList().Sum();
        }
        static Dictionary<int, int> GenerationStep(Dictionary<int, int> Population) {
            int nextGen = 0;
            foreach (var num in Enumerable.Range(0,9)) {
                if (num == 0) {
                    nextGen = Population[0];
                }
                else {
                    Population[num - 1] = Population[num];
                }
            }
            Population[6] += nextGen;
            Population[8] = nextGen;
            return Population;
        }
    }
}
