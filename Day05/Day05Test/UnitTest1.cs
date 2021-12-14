using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Day05Code;

namespace Day05Test {
    public class Tests {

        [Test]
        public void Test0_Sanity_Check() {
            var inputStrings = File.ReadAllLines("testInput.txt")
                .ToList();
            // Count lines, to see that reading file went okay
            Assert.AreEqual(10, inputStrings.Count);
        }
    }
}