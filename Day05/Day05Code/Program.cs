﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day05Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day05!");

            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();
            int answer1 = CountVentsPart1(inputStrings);
            Console.WriteLine($"answer to part 1: {answer1}");
        }
        public static int CountVentsPart1(List<string> inputStrings) {
            List<List<Vector2>> parsedCoords1 = ParseHorizontalAndVertical(inputStrings);
            return CountMultipleVentsHorizontalAndVertical(parsedCoords1);
        }
        public static List<List<Vector2>> ParseHorizontalAndVertical(List<string> inputStrings) {
            List<List<Vector2>> answer = new();
            foreach (var line in inputStrings) {
                var pairs = line.Replace(" -> ", ",").Split(",").Select(Int32.Parse).ToList();
                // weed out diagonal entries
                if (pairs[0] == pairs[2] || pairs[1] == pairs[3]){
                    answer.Add(
                        new List<Vector2>() {
                            // make sure, that the pair in answer goes from
                            // left to right, or top to bottom
                            new(Math.Min(pairs[0],pairs[2]), Math.Min(pairs[1], pairs[3])),
                            new(Math.Max(pairs[0],pairs[2]), Math.Max(pairs[1], pairs[3])),
                        });
                }
            }
            return answer;
        }
        public static int CountMultipleVentsHorizontalAndVertical(List<List<Vector2>> parsedCoords) {
            List<Vector2> vents = new();
            Vector2 vertical = new(0, 1);
            Vector2 horizontal = new(1, 0);

            foreach (var pair in parsedCoords) {
                // march vertically
                if (pair[0].X == pair[1].X) {
                    while (pair[0].Y <= pair[1].Y) {
                        vents.Add(pair[0]);
                        pair[0] += vertical;
                    }
                }
                // march horizontally 
                else {
                    while (pair[0].X <= pair[1].X) {
                        vents.Add(pair[0]);
                        pair[0] += horizontal;
                    } 
                }

            }

            // group the list by same coords, only leave behind those which are
            // counted more than once and count the hole length of the new list
            int answer = vents
                .GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Count();
            return answer;
        }
    }
}
