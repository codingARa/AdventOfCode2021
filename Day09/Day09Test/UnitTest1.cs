using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Day09Code;
using System.Numerics;

namespace Day09Test {
    public class Tests {

        [Test]
        public void Test1_Part1() {
            var nums = Program.ParseInput("testinput.txt");
            int answer = Program.SolutionPart1(nums, 10, 5);
            Assert.AreEqual(15, answer);
        }
    }
}