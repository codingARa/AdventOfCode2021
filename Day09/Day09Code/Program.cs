using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day09Code {
    public class Program {
        static void Main(string[] args) {
            Console.WriteLine("Let's solve Day09!");
            int[,] nums = ParseInput("input.txt");
            int answer1 = SolutionPart1(nums, 100, 100);
            Console.WriteLine($"answer Part1: {answer1}");

        }

        public static int[,] ParseInput(string fileName) {
            string[] lines = File.ReadLines(fileName).ToArray();

            int[,] nums = new int[lines.Count(), lines[0].Count()];

            for (int i = 0; i < lines.Count(); i++) {
                for (int j = 0; j < lines[0].Count(); j++) {
                    nums[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            return nums;
        }

        /// <summary>
        /// calculate riskLevel by finding local minimum. This is done by
        /// comparing top, bottom, left and right neighbor, if they exist
        /// </summary>
        /// <param name="nums">matrix with height profile</param>
        /// <param name="dimx">x dimension of matrix</param>
        /// <param name="dimy">y dimension of matrix</param>
        /// <returns>riskLevel according to given rule as a sum over all local minima</returns>
        public static int SolutionPart1(int[,] nums, int dimx, int dimy) {
            int riskLevel = 0;
            for (int row = 0; row < dimy; row++) {
                for (int column = 0; column < dimx; column++) {
                    bool lessThanTop = (row - 1) > -1 ? (nums[row, column] < nums[row - 1, column]) : true; 
                    bool lessThanBottom = row < (dimy-1) ? (nums[row, column] < nums[row + 1, column]) : true;
                    bool lessThanLeft = (column-1) > -1 ? (nums[row, column] < nums[row, column-1]) : true;
                    bool lessThanRight = column < (dimx-1) ? (nums[row, column] < nums[row, column+1]) : true;
                    if (lessThanTop && lessThanBottom && lessThanLeft && lessThanRight) {
                        riskLevel += nums[row, column] + 1;
                    }
                }
            }
            return riskLevel;
        }
    }
}
