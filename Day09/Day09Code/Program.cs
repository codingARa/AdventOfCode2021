using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09Code
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day09!");
            int[,] nums = ParseInput("input.txt");
            int answer1 = SolutionPart1(nums, nums.GetLength(0), nums.GetLength(1));
            Console.WriteLine($"answer Part1: {answer1}");
            int answer2 = SolutionPart2(nums, nums.GetLength(0), nums.GetLength(1));
            Console.WriteLine($"answer Part2: {answer2}");
        }

        public static int[,] ParseInput(string fileName)
        {
            string[] lines = File.ReadLines(fileName).ToArray();

            int[,] nums = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
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
        /// <param name="rowCount">row count of matrix</param>
        /// <param name="colCount">column count of matrix</param>
        /// <returns>riskLevel according to given rule as a sum over all local minima</returns>
        public static int SolutionPart1(int[,] nums, int rowCount, int colCount)
        {
            int riskLevel = 0;
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < colCount; column++)
                {
                    bool lessThanTop =
                        (row - 1) > -1 ? (nums[row, column] < nums[row - 1, column]) : true;
                    bool lessThanBottom =
                        row < (rowCount - 1) ? (nums[row, column] < nums[row + 1, column]) : true;
                    bool lessThanLeft =
                        (column - 1) > -1 ? (nums[row, column] < nums[row, column - 1]) : true;
                    bool lessThanRight =
                        column < (colCount - 1) ? (nums[row, column] < nums[row, column + 1]) : true;
                    if (lessThanTop && lessThanBottom && lessThanLeft && lessThanRight)
                    {
                        riskLevel += nums[row, column] + 1;
                    }
                }
            }
            return riskLevel;
        }

        /// <summary>
        /// finding all basins, getting all their sizes, and returning the product of the sizes of the largest three basins
        /// </summary>
        /// <param name="nums">matrix with height profile</param>
        /// <param name="rowCount">row count of matrix</param>
        /// <param name="colCount">column count of matrix</param>
        /// <returns>product of the sizes of the largest three basins</returns>
        public static int SolutionPart2(int[,] nums, int rowCount, int colCount)
        {
            var basin_sizes = new List<int>();
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (nums[row, col] != 9)
                    {
                        var basinReturnValues = FillCurrentBasin(col, row, nums, rowCount, colCount);
                        basin_sizes.Add(basinReturnValues.Item1);
                        nums = basinReturnValues.Item2;
                    }
                }
            }
            var answer = 1;
            foreach (var basin in basin_sizes.OrderByDescending(i => i).Take(3))
            {
                answer *= basin;
            }
            return answer;
        }

        /// <summary>
        /// filling a basin (with value 9) in nums by starting with current positions
        /// </summary>
        /// <param name="currentCol">current column position of found basin</param>
        /// <param name="currentRow">current row position of found basin</param>
        /// <param name="nums">matrix with height profile</param>
        /// <param name="rowCount">row count of matrix</param>
        /// <param name="colCount">column count of matrix</param>
        /// <returns>tuple with size of filled basin and new nums matrix, where found basin has been filled</returns>
        public static (int, int[,]) FillCurrentBasin(
            int currentCol,
            int currentRow,
            int[,] nums,
            int rowCount,
            int colCount
        )
        {
            int basin_size = 0;
            Queue<(int, int)> q = new();
            q.Enqueue((currentRow, currentCol));
            while (q.Count > 0)
            {
                var coords = q.Dequeue();
                var current_value = nums[coords.Item1, coords.Item2];
                if (current_value != 9)
                {
                    nums[coords.Item1, coords.Item2] = 9;
                    basin_size++;
                    // west
                    if (coords.Item1 + 1 >= 0 && coords.Item1 < rowCount - 1)
                        q.Enqueue((coords.Item1 + 1, coords.Item2));
                    // east
                    if (coords.Item1 - 1 >= 0 && coords.Item1 < rowCount - 1)
                        q.Enqueue((coords.Item1 - 1, coords.Item2));
                    // north
                    if (coords.Item2 + 1 >= 0 && coords.Item2 < colCount - 1)
                        q.Enqueue((coords.Item1, coords.Item2 + 1));
                    // south
                    if (coords.Item2 - 1 >= 0 && coords.Item2 < colCount - 1)
                        q.Enqueue((coords.Item1, coords.Item2 - 1));
                }
            }
            return (basin_size, nums);
        }
    }
}
