using NUnit.Framework;
using System.Collections.Generic;

namespace TestDay01
{
    public class Tests
    {

        [Test]
        public void Test1_Part1_Imperative()
        {
            List<int> inputList = new() { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            int calculatedAnswerImp = Day01.Program.SolutionDay01Part1Imperative(inputList);
            Assert.AreEqual(calculatedAnswerImp, 7);
        }

        [Test]
        public void Test2_Part1_Functional()
        {
            List<int> inputList = new() { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            int calculatedAnswerFunc = Day01.Program.SolutionDay01Part1Functional(inputList);
            Assert.AreEqual(calculatedAnswerFunc, 7);
        }

        [Test]
        public void Test3_Part2_Imperative()
        {
            List<int> inputList = new() { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            List<int> windowedData = Day01.Program.CreateWindowedInputData(inputList, 3);
            int calculatedAnswerImp = Day01.Program.SolutionDay01Part1Imperative(windowedData);
            Assert.AreEqual(calculatedAnswerImp, 5);
        }
    }
}