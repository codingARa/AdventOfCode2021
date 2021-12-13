using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day 01");
            var inputIntegers = File.ReadAllLines("input.txt")
                .Select(Int32.Parse)
                .ToList();
            int answer1 = SolutionDay01Part1Imperative(inputIntegers);
            Console.WriteLine($"Solution part 1: {answer1}");

            List<int> windowedData = CreateWindowedInputData(inputIntegers, 3);
            int answer2 = SolutionDay01Part1Functional(windowedData);
            Console.WriteLine($"Solution part 2: {answer2}");
        }

        /// <summary>
        /// imperative solution for comparing consecutive integers in list and
        /// count how often the next number is larger than the previous ones
        /// </summary>
        /// <param name="inputIntegers"></param>
        /// <returns></returns>
        public static int SolutionDay01Part1Imperative(List<int> inputIntegers)
        {
            /* imperative solution */
            int answer = 0;
            int compare = inputIntegers[0];
            foreach (int item in inputIntegers)
            {
                if (item > compare) { answer++; }
                compare = item;
            }
            return answer;
        }

        /// <summary>
        /// functional solution for comparing consecutive integers in list and
        /// count how often the next number is larger than the previous ones
        /// </summary>
        /// <param name="inputIntegers"></param>
        /// <returns></returns>
        public static int SolutionDay01Part1Functional(List<int> inputIntegers)
        {
            /* functional solution */
            var compareWith = inputIntegers.Skip(1).Zip(inputIntegers.SkipLast(1));
            int answer = compareWith.Aggregate(seed: 0,
                func: (acc, x) => (x.Item2 < x.Item1) ? acc += 1 : acc );

            return answer;
        }

        public static List<int> CreateWindowedInputData(List<int> inputIntegers, int windowSize)
        {
            List<int> windowedData = new();
            for (int i = 0; i < inputIntegers.Count-windowSize+1; i++)
            {
                windowedData.Add(inputIntegers.Skip(i).Take(windowSize).Sum());
            }
            return windowedData;

        }
    }
}
