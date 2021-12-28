using NUnit.Framework;
using Day08Code;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Numerics;

namespace Day08Test {
    public class Tests {
        [Test]
        public void Test1_SolutionPart1() {
            int answer = Program.SolutionPart1("testinput.txt");
            Assert.AreEqual(26, answer);
        }

        [Test]
        public void Test2_Segmentation() {
            string teststring = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
            var input = teststring.Split("|")[0]
                            .Trim()
                            .Split(" ")
                            .GroupBy(x => x.Length)
                            .ToDictionary(group => group.Key, group => group.ToArray());
            var answer_dict = Program.FindSegments(input);
            foreach (KeyValuePair<string,string> dict in answer_dict) {
                System.Console.WriteLine($"{dict.Key} : {dict.Value}");
            }
            Dictionary<string, string> expected = new() {
                { "a", "d" } ,
                { "b", "e" } ,
                { "c", "a" } ,
                { "d", "f" } ,
                { "e", "g" } ,
                { "f", "b" } ,
                { "g", "c" } ,
            };
            Assert.AreEqual(expected, answer_dict);
        }

        [Test]
        public void Test3_Figures() {
            string teststring = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
            var input = teststring.Split("|")[0]
                            .Trim()
                            .Split(" ")
                            .GroupBy(x => x.Length)
                            .ToDictionary(group => group.Key, group => group.ToArray());
            var answer_dict = Program.FindFigures(input);
            foreach (KeyValuePair<string,int> dict in answer_dict) {
                System.Console.WriteLine($"{dict.Key} : {dict.Value}");
            }
            Dictionary<string, int> expected_ = new() {
                { "cagedb", 0 } ,
                { "ab", 1 } ,
                { "gcdfa", 2 } ,
                { "fbcad", 3 } ,
                { "eafb", 4 } ,
                { "cdfbe", 5 } ,
                { "cdfgeb", 6 } ,
                { "dab", 7 } ,
                { "acedgfb", 8 } ,
                { "cefabd", 9 } ,
            };
            // sort keys alphabetically
            Dictionary<string, int> expected = expected_.ToDictionary(x => string.Concat(x.Key.OrderBy(c => c)), x => x.Value);
            Assert.AreEqual(expected, answer_dict); 
        }

        [Test]
        public void Test4() {
            int answer = Program.SolutionPart2("testinput.txt");
            Assert.AreEqual(61229, answer);
        }
    }
}