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
        }
    }
}