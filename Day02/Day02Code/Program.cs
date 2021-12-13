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
            int answer1 = SteerSubmarine(inputString);
            Console.WriteLine($"Answer to Part 1: {answer1}");
        }

        public static int SteerSubmarine(List<string> directions)
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
    }
}
