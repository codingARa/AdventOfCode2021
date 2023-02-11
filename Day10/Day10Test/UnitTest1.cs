namespace Day10Test;

public class Tests
{
    [Test]
    public void TestSolutionPart1()
    {
        var input = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(input);
        Assert.AreEqual(26397, answer1.Item1);
    }

    [Test]
    public void TestSolutionPart2()
    {
        var input = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(input);
        var answer2 = Program.SolutionPart2(answer1.Item2);
        Assert.AreEqual(288957, answer2);
    }
}