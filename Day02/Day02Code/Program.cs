using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02Code
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day 02");
            var inputString = File.ReadAllLines("input.txt")
                .ToList();
            int answer1 = SteerSubmarinePart1(inputString);
            Console.WriteLine($"Answer to Part 1: {answer1}");
            int answer2 = SteerSubmarinePart2(inputString);
            Console.WriteLine($"Answer to Part 2: {answer2}");
        }

        /// <summary>
        /// steer submarine by input directions according to rules of part 1
        /// </summary>
        /// <param name="directions"></param>
        /// <returns></returns>
        public static int SteerSubmarinePart1(List<string> directions)
        {
            int fwd = 0;
            int depth = 0;
            foreach (string direction in directions)
            {
                var dir = direction.Split(" ");
                if (dir[0] == "forward")
                {
                    fwd += Int32.Parse(dir[1]);
                } else if (dir[0] == "up")
                {
                    depth -= Int32.Parse(dir[1]);
                } else if (dir[0] == "down")
                {
                    depth += Int32.Parse(dir[1]);
                } else { throw new Exception("unexpected input argument"); }
            }
            return fwd*depth;
        }


        /// <summary>
        /// steer submarine by input directions according to rules of part 2
        /// </summary>
        /// <param name="directions"></param>
        /// <returns></returns>
        public static int SteerSubmarinePart2(List<string> directions)
        {
            int aim = 0;
            int fwd = 0;
            int depth = 0;
            foreach (string direction in directions)
            {
                var dir = direction.Split(" ");
                if (dir[0] == "forward")
                {
                    int newfwd = Int32.Parse(dir[1]);
                    fwd += newfwd;
                    depth += aim * newfwd;
                } else if (dir[0] == "up")
                {
                    aim -= Int32.Parse(dir[1]);
                } else if (dir[0] == "down")
                {
                    aim += Int32.Parse(dir[1]);
                } else { throw new Exception("unexpected input argument"); }
            }
            return fwd*depth;
        }
    }
}
