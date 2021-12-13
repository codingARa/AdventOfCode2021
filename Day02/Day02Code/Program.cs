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
            int answer = SteerSubmarine(inputString);
        }

        public static int SteerSubmarine(List<string> directions)
        {
            return 1;
        }
    }
}
