using Day11Code;

namespace Day11Test;

public class Tests
{
    [Test]
    public void TestSolution1Step1()
    {
        var expectedFields = Program.ParseInput($"{Directory.GetCurrentDirectory()}\\officialResults\\testresultAfter1.txt");

        var nums = Program.ParseInput("OfficialInputfile.txt");
        var answer1 = Program.SolutionPart1(nums, 1);

        answer1.Item1.Should().BeEquivalentTo(expectedFields);
        answer1.Item2.Should().Be(0);
    }

    [Test]
    public void TestSolution1Step2()
    {
        var expectedFields = Program.ParseInput($"{Directory.GetCurrentDirectory()}\\officialResults\\testresultAfter2.txt");

        var nums = Program.ParseInput("OfficialInputfile.txt");
        var answer1 = Program.SolutionPart1(nums, 2);

        answer1.Item1.Should().BeEquivalentTo(expectedFields);
        answer1.Item2.Should().Be(35);
    }

    [Test]
    public void TestSolution1Step100()
    {
        var expectedFields = Program.ParseInput($"{Directory.GetCurrentDirectory()}\\officialResults\\testresultAfter100.txt");

        var nums = Program.ParseInput("OfficialInputfile.txt");
        var answer1 = Program.SolutionPart1(nums, 100);

        answer1.Item1.Should().BeEquivalentTo(expectedFields);
        answer1.Item2.Should().Be(1656);
    }

    private static IEnumerable<TestCaseData> _officalResults
    {
        get
        {
            yield return new TestCaseData(1, "testresultAfter1.txt");
            yield return new TestCaseData(2, "testresultAfter2.txt");
            yield return new TestCaseData(3, "testresultAfter3.txt");
            yield return new TestCaseData(4, "testresultAfter4.txt");
            yield return new TestCaseData(5, "testresultAfter5.txt");
            yield return new TestCaseData(6, "testresultAfter6.txt");
            yield return new TestCaseData(7, "testresultAfter7.txt");
            yield return new TestCaseData(8, "testresultAfter8.txt");
            yield return new TestCaseData(9, "testresultAfter9.txt");
            yield return new TestCaseData(10, "testresultAfter10.txt");
            yield return new TestCaseData(20, "testresultAfter20.txt");
            yield return new TestCaseData(30, "testresultAfter30.txt");
            yield return new TestCaseData(40, "testresultAfter40.txt");
            yield return new TestCaseData(50, "testresultAfter50.txt");
            yield return new TestCaseData(60, "testresultAfter60.txt");
            yield return new TestCaseData(70, "testresultAfter70.txt");
            yield return new TestCaseData(80, "testresultAfter80.txt");
            yield return new TestCaseData(80, "testresultAfter80.txt");
            yield return new TestCaseData(100, "testresultAfter100.txt");
            yield return new TestCaseData(193, "testresultAfter193.txt");
            yield return new TestCaseData(194, "testresultAfter194.txt");
            yield return new TestCaseData(195, "testresultAfter195.txt");
        }
    }

    [Test, TestCaseSource(nameof(_officalResults))]
    public void TestSolution1CompareAllAvailableFieldsWithTestCaseSource(int steps, string filename)
    {
        var expectedFields = Program.ParseInput($"{Directory.GetCurrentDirectory()}\\officialResults\\" + filename);

        var nums = Program.ParseInput("OfficialInputfile.txt");
        var answer1 = Program.SolutionPart1(nums, steps);

        answer1.Item1.Should().BeEquivalentTo(expectedFields);
    }

    [Test]
    public void TestSolution2NoOfFlashesAfter195Steps()
    {
        var nums = Program.ParseInput("OfficialInputfile.txt");
        var answer2 = Program.SolutionPart2(nums);
        answer2.Item2.Should().Be(195);
    }

    [Test]
    public void TestSolution2ValidateTargetSolutionByForeignUser()
    {
        // solution by foreign user
        // https://github.com/Qualia91/AdventOfCode2021/blob/master/Day11/day11.py
        var foreignSolution = 324;
        var expectedField = new int[,]
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };

        var nums = Program.ParseInput("MyInputfile.txt");
        var answer2 = Program.SolutionPart1(nums, foreignSolution);

        answer2.Item1.Should().BeEquivalentTo(expectedField);
    }

    private static IEnumerable<TestCaseData> _foreignResults 
    {
        get
        {
            for (int i = 1; i <= 324; i++)
            {
                yield return new TestCaseData(i, $"{Directory.GetCurrentDirectory()}\\foreignResultsForMyInputfile\\resultAtStep{i.ToString().PadLeft(3, '0')}.txt");
            }
        }
    }

    [Test, TestCaseSource(nameof(_foreignResults))]
    public void TestSolution2ValidateForeignUserResults(int steps, string filename)
    {
        var expectedFields = Program.ParseInput(filename);

        var nums = Program.ParseInput("MyInputfile.txt");
        var answer1 = Program.SolutionPart1(nums, steps);

        answer1.Item1.Should().BeEquivalentTo(expectedFields);
    }
}