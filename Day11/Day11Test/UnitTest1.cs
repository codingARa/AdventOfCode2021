using Day11Code;

namespace Day11Test;

public class Tests
{
    [Test]
    public void TestSolution1Step1()
    {
        var expectedFields = Program.ParseInput("testresultAfter1.txt");

        var nums = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(nums, 1);

        Assert.AreEqual(expectedFields, answer1.Item1);
        Assert.AreEqual(0, answer1.Item2);
    }

    [Test]
    public void TestSolution1Step2()
    {
        var nums = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(nums, 2);

        var expectedFields = Program.ParseInput("testresultAfter2.txt");
        Assert.AreEqual(expectedFields, answer1.Item1);
        Assert.AreEqual(35, answer1.Item2);
    }

    [Test]
    public void TestSolution1Step100()
    {
        var nums = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(nums, 100);

        var expectedFields = Program.ParseInput("testresultAfter100.txt");
        Assert.AreEqual(expectedFields, answer1.Item1);
        Assert.AreEqual(1656, answer1.Item2);
    }
}