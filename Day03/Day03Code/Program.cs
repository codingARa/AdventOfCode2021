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
            int answer2 = LifeSupportRatingPart2(inputStrings);
            Console.WriteLine($"Answer for Part 2: {answer2}");
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

        /// <summary>
        /// calculate the life support rating by evaluation
        /// OxygenGeneratorRating and CO2ScrubberRating
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static int LifeSupportRatingPart2(List<string> inputString)
        {
            int oxyGenRating = OxygenGeneratorRating(inputString);
            int scrubberRating = CO2ScrubberRating(inputString);

            return oxyGenRating*scrubberRating;
        }
        /// <summary>
        /// calculate the oxygen generator rating by iteratively calling Rating
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        static int OxygenGeneratorRating(List<string> inputString)
        {
            // how many digits are in inputString?
            int positions = inputString[0].Length;
            int current_position_oxy = 0;

            // call Rating as long as the returned list is not of length on or
            // the positions of the given diagnostic output haven been
            // exhausted
            List<string> oxyGenRatingList = inputString;
            while (oxyGenRatingList.Count > 1 && current_position_oxy <= positions)
            {
                oxyGenRatingList = Rating(oxyGenRatingList, current_position_oxy, ('1', '0'));
                current_position_oxy++;
            }
            int oxyGenRating = 0;
            if (oxyGenRatingList.Count == 1)
            {
                oxyGenRating = Convert.ToInt32(oxyGenRatingList[0], 2);

            }
            else { throw new Exception("input data cannot be evaluate according to given rules"); }
            return oxyGenRating;
        }
        /// <summary>
        /// calculate the CO2 scrubber rating by iteratively calling Rating
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        static int CO2ScrubberRating(List<string> inputString)
        {
            // how many digits are in inputString?
            int positions = inputString[0].Length;
            int current_position_co2 = 0;

            // call Rating as long as the returned list is not of length on or
            // the positions of the given diagnostic output haven been
            // exhausted
            List<string> scrubberRatingList = inputString;
            while (scrubberRatingList.Count > 1 && current_position_co2 <= positions)
            {
                scrubberRatingList = Rating(scrubberRatingList, current_position_co2, ('0', '1'));
                current_position_co2++;
            }
            int scrubberRating = 0;
            if (scrubberRatingList.Count == 1) {
                scrubberRating = Convert.ToInt32(scrubberRatingList[0], 2);
            }
            else { throw new Exception("input data cannot be evaluate according to given rules"); }
            return scrubberRating;
        }
        /// <summary>
        /// Rating abstracts the necessary comparision, how many zeros or ones
        /// are counted in a given position of the given diagnostic report
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="position"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        static List<string> Rating(List<string>inputString, int position, (char, char) rule)
        {
            int counts = GetCountsFromStringList(inputString, position);
            List<string> answer = new();
            // more than half of rule.Item1
            if (counts >= inputString.Count / 2.0f)
            {
                answer = inputString.Where(x => x[position] == rule.Item1).ToList();
            // more than half of rule.Item2
            } else if (counts < inputString.Count / 2.0f)
            {
                answer = inputString.Where(x => x[position] == rule.Item2).ToList();
            }
            return answer;

        }

        static int GetCountsFromStringList(List<string> inputString, int position)
        {
            int counts = 0;
            // sum the bits by position over all inputs
            foreach (string code in inputString)
            {
                char[] bits = code.ToCharArray();
                counts += (int)char.GetNumericValue(bits[position]);
            }
            return counts;
        }
    }
}
