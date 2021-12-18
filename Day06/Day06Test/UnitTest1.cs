using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Day06Code;
using System.Numerics;

namespace Day06Test {
    public class Tests {

        [Test]
        public void Test1_Part1() {
            string initial_state = "3,4,3,1,2";
            Dictionary<int, int> Population = Program.ParseInputPart1(initial_state);
            int answer = Program.CountPopulationAfterNGenerations(Population, 80);
            Assert.AreEqual(5934, answer);
        }
        [Test]
        public void Test1_Part2() {
            string initial_state = "3,4,3,1,2";
            Dictionary<int, ulong> Population = Program.ParseInputPart2(initial_state);
            ulong answer = Program.CountPopulationAfterNGenerationsPart2(Population, 256);
            Assert.AreEqual(26984457539, answer);
        }
    }
}