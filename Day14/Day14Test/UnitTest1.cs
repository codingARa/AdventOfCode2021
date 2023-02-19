using Day14Code;
namespace Day14Test;

public class Tests
{
    [Test]
    public void Test_SolutionPart1()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");

        // act
        var answer1 = Program.SolutionPart1(input);

        // assert
        answer1.Should().Be(1588);
    }
}