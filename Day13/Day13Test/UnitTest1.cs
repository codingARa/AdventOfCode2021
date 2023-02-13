namespace Day13Test;

public class Tests
{
    [Test]
    public void TestSolution1()
    {
        // arrange
        var input = Program.ParseInput("testinput");

        // act
        var answer1 = Program.SolutionPart1(input);

        // assert
        answer1.Should().Be(0);
    }

    [Test]
    public void TestSolution2()
    {
        // arrange
        var input = Program.ParseInput("testinput");

        // act
        var answer2 = Program.SolutionPart2(input);

        // assert
        answer2.Should().Be(0);
    }
}