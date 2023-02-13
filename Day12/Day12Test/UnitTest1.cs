using Day12Code;

namespace Day12Test;

public class Tests
{
    [Test]
    public void TestSolution1()
    {
        // arrange
        var expectedValue = 10;

        // act
        var input = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart1(input);

        // assert
        answer1.Should().Be(expectedValue);
    }

    [Test]
    public void TestSolution2()
    {
        // arrange
        var expectedValue = 36;

        // act
        var input = Program.ParseInput("testinput.txt");
        var answer1 = Program.SolutionPart2(input);

        // assert
        answer1.Should().Be(expectedValue);
    }
}
