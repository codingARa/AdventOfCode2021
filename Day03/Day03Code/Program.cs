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
            var inputIntegers = File.ReadAllLines("input.txt")
                .ToList();
        }
    }
}
