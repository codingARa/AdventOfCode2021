using Day14Code;

namespace Day14Test;

public class Tests
{
    [Test]
    public void Part1_NaiveApproach_TestData()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");
        var steps = 10;
        int expectedResult = 1588;
        int expectedLength = 3073;

        // act
        var answer = Program.SolutionPart1(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
        answer.Item2.Should().Be(expectedLength);
    }

    [Test]
    public void Part1_DifferentApproach_TestData()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");
        var steps = 10;
        ulong expectedResult = 1588;
        ulong expectedLength = 3073;

        // act
        var answer = Program.SolutionPart2(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
        answer.Item2.Should().Be(expectedLength);
    }

    [Test]
    public void Part1_NaiveApproach_RealData()
    {
        // arrange
        var input = Program.ParseInput("input.txt");
        var steps = 10;
        int expectedResult = 2975;

        // act
        var answer = Program.SolutionPart1(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
    }

    [Test]
    public void Part1_DifferentApproach_RealData()
    {
        // arrange
        var input = Program.ParseInput("input.txt");
        var steps = 10;
        ulong expectedResult = 2975;

        // act
        var answer = Program.SolutionPart2(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
    }

    [Test]
    public void Part2_DifferentApproach_TestData()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");
        var steps = 40;
        ulong expectedResult = 2188189693529;

        // act
        var answer = Program.SolutionPart2(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
    }

    [Test]
    public void Part2_DifferentApproach_RealData()
    {
        // arrange
        var input = Program.ParseInput("input.txt");
        var steps = 40;
        ulong expectedResult = 3015383850689;

        // act
        var answer = Program.SolutionPart2(input.Item1, input.Item2, steps);

        // assert
        answer.Item1.Should().Be(expectedResult);
    }
}
