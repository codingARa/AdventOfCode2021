using NUnit.Framework;
using Day09Code;

namespace Day09Test {
    public class Tests {

        [Test]
        public void Test1_Part1() {
            var nums = Program.ParseInput("testinput.txt");
            int answer = Program.SolutionPart1(nums, nums.GetLength(0), nums.GetLength(1));
            Assert.AreEqual(15, answer);
        }

        [Test]
        public void Test2_Part2() {
            var nums = Program.ParseInput("testinput.txt");
            int answer = Program.SolutionPart2(nums, nums.GetLength(0), nums.GetLength(1));
            Assert.AreEqual(1134, answer);
        }
    }
}