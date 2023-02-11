namespace Day10Test;

public class Tests
{
    [Test]
    public void TestSolutionPart1()
    {
        var input = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(input);
        Assert.AreEqual(26397, answer1);
    }
}