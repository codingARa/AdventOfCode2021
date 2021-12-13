using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Day04Test
{
    public class Tests
    {

        [Test]
        public void Test1_Part1()
        {
            var inputStrings = File.ReadAllLines("testInput.txt")
                .ToList();
            // Count lines, to see that reading file went okay
            Assert.AreEqual(19, inputStrings.Count);
        }
    }
}