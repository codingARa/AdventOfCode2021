namespace Day13Test;

public class Tests
{
    [Test]
    public void TestSolution1()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");

        // act
        var answer1 = Program.SolutionPart1(input);

        // assert
        answer1.Should().Be(17);
    }

    [Test]
    public void TestSolution2()
    {
        // arrange
        var input = Program.ParseInput("testinput.txt");
        var expectedOutput = "#####\n#   #\n#   #\n#   #\n#####\n";

        // act
        var foldings = Program.FoldingsSolutionPart2(input);
        var output = Program.CoordinateOutputString(foldings);

        // assert
        output.Should().Be(expectedOutput);
    }
}
