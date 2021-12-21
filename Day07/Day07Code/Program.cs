using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day07Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day07!");
            var inputString = File.ReadAllLines("input.txt");
            Stopwatch watch = new();

            watch.Start();
            int answer1 = CrabFuelConsumptionPart1(inputString[0]);
            watch.Stop();
            Console.WriteLine($"Solution Part1: {answer1}");
            Console.WriteLine($"Found in: {watch.ElapsedMilliseconds} ms");
            watch.Reset();

            watch.Start();
            int answer2 = CrabFuelConsumptionPart2(inputString[0]);
            watch.Stop();
            Console.WriteLine($"Solution Part2: {answer2}");
            Console.WriteLine($"Found in: {watch.ElapsedMilliseconds} ms");
        }

        public static int[] ParseInputString(string inputString) {
            return inputString.Split(",").Select(s => Convert.ToInt32(s)).ToArray();
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

        public static int CrabFuelConsumptionPart2(string inputString) {
            int[] horizontalPositions = ParseInputString(inputString);
            int minPosition = horizontalPositions.Min();
            int amountToCount = horizontalPositions.Max() - minPosition;
            List<int> possibleFuelExpenditure = new();
            int minFuelYet = int.MaxValue;

            foreach (int position in Enumerable.Range(minPosition, amountToCount)) {
                int distanceToWalk = 0;
                int fuelSpent = 0;
                foreach(int crabPos in horizontalPositions) {
                    distanceToWalk = Math.Abs(crabPos - position);
                    fuelSpent += Enumerable.Range(1, distanceToWalk).Sum();
                    if (fuelSpent >= minFuelYet) { break; }
                }
                possibleFuelExpenditure.Add(fuelSpent);
                minFuelYet = possibleFuelExpenditure.Min();
            }
            return possibleFuelExpenditure.Min();
        }
    }
}
