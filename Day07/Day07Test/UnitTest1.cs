using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Day07Code;
using System.Numerics;

namespace Day07Test {
    public class Tests {

        [Test]
        public void Test1_Part1() {
            string input = "16,1,2,0,4,2,7,1,2,14";
            int fuel = Program.CrabFuelConsumptionPart1(input);
            Assert.AreEqual(37, fuel);
        }
    }
}