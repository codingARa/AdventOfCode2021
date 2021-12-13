using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's solve Day 04!");
            var inputStrings = File.ReadAllLines("input.txt")
                .ToList();
            int answer1 = inputStrings.Count;
            Console.WriteLine($"answer to part 1: {answer1}");
        }
    }
}
