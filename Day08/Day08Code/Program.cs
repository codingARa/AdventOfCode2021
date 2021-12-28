﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day08Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day07!");

            int answer1 = SolutionPart1("input.txt");
            Console.WriteLine($"answer to part 1: {answer1}");

            int answer2 = SolutionPart2("input.txt");
            Console.WriteLine($"answer to part 2: {answer2}");
        }

        public static int SolutionPart1(string inputFile) {
            var inputStrings = File.ReadAllLines(inputFile).ToList();
            int x = 0;
            foreach (var input in inputStrings) {
                x += input.Split("|")[1]
                    .Trim()
                    .Split(" ")
                    .Where(s => s.Length == 2 || s.Length == 3 || s.Length == 4 || s.Length == 7)
                    .Count();
            }
            return x; 
        }

        public static int SolutionPart2(string inputFile) {
            var inputStrings = File.ReadAllLines(inputFile).ToList();
            int sum = 0;
            foreach (var line in inputStrings) {

                var input = line.Split("|")[0]
                    .Trim()
                    .Split(" ")
                    .GroupBy(x => x.Length)
                    .ToDictionary(group => group.Key, group => group.ToArray());

                Dictionary<string, int> figures = FindFigures(input);

                var x = line.Split("|")[1]
                    .Trim()
                    .Split(" ");

                string tallyString = string.Empty;
                foreach (var item in x) {
                    string f = String.Concat(item.OrderBy(c => c));
                    tallyString = string.Concat(tallyString, figures[f] );
                }
                sum += int.Parse(tallyString);
            }
            return sum; 
        }

        /// <summary>
        /// find all segments from input
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Dictionary of standard segment-position to current position</returns>
        public static Dictionary<string,string> FindSegments(Dictionary<int, string[]> input) {
            Dictionary<string, string> results = new();
            //1 
            results.Add("a", StringDifference(input[3][0], input[2][0]));
            //2
            string aeg = StringDifference(input[7][0], input[4][0]);
            string eg = StringDifference(aeg, results["a"]);

            //3
            string dg = string.Empty;
            string d = string.Empty;
            foreach (var item in input[5]) {
                var x = StringDifference(item, input[3][0]);
                if (x.Length == 2) {
                    dg = x;
                    d = StringDifference(x, eg);
                }
            }
            results.Add("d", d);
            results.Add("g", StringDifference(dg, d));
            results.Add("e", StringDifference(eg, results["g"]));

            // 4
            // find figure 6
            string figureSix = string.Empty;
            foreach (var item in input[6]) {
                var x = StringDifference(item, input[3][0]);
                if (x.Length == 4) figureSix = item;
            }
            results.Add("c", StringDifference(input[7][0], figureSix));
            results.Add("f", StringDifference(input[2][0], results["c"]));

            // 5
            string bd = StringDifference(input[4][0], input[2][0]);
            results.Add("b", StringDifference(bd, results["d"]));
            return results;
        }

        /// <summary>
        /// find all specific strings for each figure in seven-segment display
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Dictionary of figure to specific string sequence</returns>
        public static Dictionary<string,int> FindFigures(Dictionary<int, string[]> input) {
            Dictionary<int, string> help = new();
            help.Add(1, input[2][0]);
            help.Add(7, input[3][0]);
            help.Add(4, input[4][0]);
            help.Add(8, input[7][0]);

            // find figure six
            string[] figureNineAndZero = new String[2];
            int i = 0;
            foreach (var item in input[6]) {
                var x = StringDifference(item, help[7]);
                if (x.Length == 4) {
                    help.Add(6, item);
                    }
                else {
                    figureNineAndZero[i] = item;
                    i++;
                }
            }

            // find figure five
            string[] figureTwoAndThree = new String[2];
            int j = 0;
            foreach (var item in input[5]) {
                var x = StringDifference(help[6], item);
                if (x.Length == 1) {
                    help.Add(5, item);
                }
                else {
                    figureTwoAndThree[j] = item;
                    j++;
                }
            }

            //find figure nine and zero
            foreach (var item in figureNineAndZero) {
                var x = StringDifference(item, help[5]);
                if (x.Length == 1) {
                    help.Add(9, item);
                }
                if (x.Length == 2) {
                    help.Add(0, item);
                }
            }

            //find figure two and three
            foreach (var item in figureTwoAndThree) {
                var x = StringDifference(help[9], item);
                if (x.Length == 1) {
                    help.Add(3, item);
                }
                if (x.Length == 2) {
                    help.Add(2, item);
                }
            }

            Dictionary<string, int> results = new();
            // result-dictionary is ordered figure-string to number
            results = help.ToDictionary(x => string.Concat(x.Value.OrderBy(c => c)), x => x.Key);
            //results = help.ToDictionary(x => x.Value, x => x.Key);

            return results;
        }

        static string StringDifference(string minuend, string subtrahend) {
            foreach (char c in subtrahend) {
                minuend = minuend.Replace(c.ToString(), string.Empty);
            }
            return minuend;
        }
    }
}
