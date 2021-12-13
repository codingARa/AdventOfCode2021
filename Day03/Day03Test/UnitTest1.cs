using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Day03Test
{
    public class Tests
    {

        [Test]
        public void Test1_Part1()
        {
            List<string> inputList = new() { 
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010"
            };
            int answer = Day03Code.Program.PowerConsumptionPart1(inputList);
            Assert.AreEqual(198, answer);
        }

        [Test]
        public void Test2_Part2()
        {
            List<string> inputList = new() { 
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010"
            };
            int answer = Day03Code.Program.LifeSupportRatingPart2(inputList);
            Assert.AreEqual(230, answer);
        }
    }
}