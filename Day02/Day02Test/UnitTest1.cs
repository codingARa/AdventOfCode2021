using NUnit.Framework;
using System.Collections.Generic;

namespace Day02Test
{
    public class Tests
    {
        [Test]
        public void Test1_Part1()
        {
            List<string> inputString = new()
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2" 
            };
            int answer = Day02Code.Program.SteerSubmarinePart1(inputString);
            Assert.AreEqual(150, answer);
        }

        [Test]
        public void Test2_Part2()
        {
            List<string> inputString = new()
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2" 
            };
            int answer = Day02Code.Program.SteerSubmarinePart2(inputString);
            Assert.AreEqual(900, answer);
        }
    }
}