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
/*            string teststring = "badc bd dbeaf cfdbge dfb cfbdea efbag edcfgab dcafe degfca | eacfd acdfbe cbdegf fcbaedg";
            var input = teststring.Split("|")[0]
                .Trim()
                .Split(" ")
                .GroupBy(x => x.Length)
                .ToDictionary(group => group.Key, group => group.ToArray());

            Dictionary<string,string> segments = FindSegments(input);
            Dictionary<int,string> figures = FindFigures(input);
*/
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

        /// <summary>
        /// find all segments from input
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Dictionary of standard segment-position to current position</returns>
        private static Dictionary<string,string> FindSegments(Dictionary<int, string[]> input) {
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
        private static Dictionary<int,string> FindFigures(Dictionary<int, string[]> input) {
            Dictionary<int, string> results = new();
            results.Add(1, input[2][0]);
            results.Add(7, input[3][0]);
            results.Add(4, input[4][0]);
            results.Add(8, input[7][0]);

            // find figure six
            string[] figureNineAndZero = new String[2];
            int i = 0;
            foreach (var item in input[6]) {
                var x = StringDifference(item, results[7]);
                if (x.Length == 4) results.Add(6, item);
                else {
                    figureNineAndZero[i] = item;
                    i++;
                }
            }

            // find figure five
            string[] figureTwoAndThree = new String[2];
            int j = 0;
            foreach (var item in input[5]) {
                var x = StringDifference(results[6], item);
                if (x.Length == 1) results.Add(5, item);
                else {
                    figureTwoAndThree[j] = item;
                    j++;
                }
            }

            //find figure nine and zero
            foreach (var item in figureNineAndZero) {
                var x = StringDifference(item, results[5]);
                if (x.Length == 1) results.Add(9, item);
                if (x.Length == 2) results.Add(0, item);
            }

            //find figure two and three
            foreach (var item in figureTwoAndThree) {
                var x = StringDifference(item, results[9]);
                if (x.Length == 1) results.Add(3, item);
                if (x.Length == 2) results.Add(2, item); 
            }

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
