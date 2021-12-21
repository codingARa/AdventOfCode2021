using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day07Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day07!");
            var inputString = File.ReadAllLines("input.txt");
            int answer1 = CrabFuelConsumptionPart1(inputString[0]);
            Console.WriteLine($"Solution Part1: {answer1}");
        }
        public static int CrabFuelConsumptionPart1(string inputString) {
            int[] horizontalPositions = ParseInputString(inputString);
            int minPosition = horizontalPositions.Min();
            int amountToCount = horizontalPositions.Max() - minPosition;
            List<int> possibleFuelExpenditure = new();
            foreach (int position in Enumerable.Range(minPosition, amountToCount)) {
                int fuelSpent = 0;
                foreach(int crabPos in horizontalPositions) {
                    fuelSpent += Math.Abs(crabPos - position);
                }
                possibleFuelExpenditure.Add(fuelSpent);
            }
            return possibleFuelExpenditure.Min();
        }
        public static int[] ParseInputString(string inputString) {
            return inputString.Split(",").Select(s => Convert.ToInt32(s)).ToArray();
        }
    }
}
