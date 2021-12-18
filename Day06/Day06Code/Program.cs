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

            Dictionary<int, ulong> Population2 = ParseInputPart2(inputStrings[0]);
            ulong answer2 = CountPopulationAfterNGenerationsPart2(Population2, 256);
            Console.WriteLine($"answer to part 2: {answer2}");
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

        public static Dictionary<int,ulong> ParseInputPart2(string inputString) {
            Dictionary<int, ulong> Population = new();
            foreach(int num in Enumerable.Range(0,9)) {
                char[] cArray = num.ToString().ToCharArray();
                Population.Add(num, (ulong)inputString.Count(x => x == cArray[0]));
            }
            return Population;
        }
        public static ulong CountPopulationAfterNGenerationsPart2(Dictionary<int,ulong> Population, int nGenerations) {
            for (int n = 0; n < nGenerations; n++) {
                Population = GenerationStep2(Population);
            }
            ulong result = 0;
            foreach (var num in Enumerable.Range(0, 9)) {
                result += Population[num];
            }
            return result;
        }
        static Dictionary<int, ulong> GenerationStep2(Dictionary<int, ulong> Population) {
            ulong nextGen = 0;
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
