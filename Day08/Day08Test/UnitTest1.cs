using NUnit.Framework;
using Day08Code;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Numerics;

namespace Day08Test {
    public class Tests {
        [Test]
        public void Test1() {
            int answer = Program.SolutionPart1("testinput.txt");
            Assert.AreEqual(26, answer);
        }
    }
}