using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03Code
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day 03");
            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();
            int answer1 = PowerConsumptionPart1(inputStrings);
            Console.WriteLine($"Answer for Part 1: {answer1}");
        }


        /// <summary>
        /// calculate the submarine power consumptions by the rules of part 1
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static int PowerConsumptionPart1(List<string> inputString)
        {
            // how many digits are in inputString?
            int positions = inputString[0].Length;

            // init counts by positions
            List<int> counts = new();
            for (int i = 0; i < positions; i++) { counts.Add(0); }

            // sum the bits by position over all inputs
            foreach (string code in inputString)
            {
                char[] bits = code.ToCharArray();
                for (int i = 0; i < positions; i++)
                {
                    counts[i] += (int)char.GetNumericValue(bits[i]);
                }
            }

            // to calculate the gamma_rate, check if the counted bits are above
            // or below half of all inputs
            int lineCount = inputString.Count();
            string gamma_rate_string = "";
            foreach (int count in counts)
            {
                if (count > lineCount / 2)
                {
                    gamma_rate_string = string.Concat(gamma_rate_string, 1);
                }
                else if (count < lineCount / 2)
                {
                    gamma_rate_string = string.Concat(gamma_rate_string, 0);
                }
                else { throw new Exception("input data cannot be evaluate according to given rules"); }
            }
            int gamma_rate = Convert.ToInt32(gamma_rate_string, 2);
            // by definition the epsilon_rate is the bitwise inverse of the
            // gamma_rate, which means for 5 digits it is always the
            // following...
            int epsilon_rate = (int)Math.Pow(2, positions) - gamma_rate - 1;

            return gamma_rate*epsilon_rate;
        }

        public static int LifeSupportRatingPart2(List<string> inputString)
        {
            return 1;
        }
    }
}
