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
                "forward 2forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };
            int answer = Day02Code.Program.SteerSubmarine(inputString);
            Assert.AreEqual(answer, 150);
        }
    }
}