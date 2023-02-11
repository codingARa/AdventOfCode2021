namespace Day10Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day10!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input);
        Console.WriteLine($"Answer1: {answer1.Item1}");

        var answer2 = SolutionPart2(answer1.Item2);
        Console.WriteLine($"Answer2: {answer2}");
    }

    /// <summary>
    /// find first illegal/mismatched character in corrupted line and return
    /// error score for input file
    /// </summary>
    /// <param name="input"></param>
    /// <returns>return error score of input file</returns>
    public static (int, List<Stack<char>>) SolutionPart1(List<string> input)
    {
        var scoringDictionary = new Dictionary<char, int> {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };
        var matchingDictionary = new Dictionary<char, char> {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        var openingBrackets = new List<char> { '(', '[', '{', '<' };

        int error_score = 0;
        List<Stack<char>> incompleteLines = new();

        foreach (var line in input)
        {
            Stack<char> s = new();
            var currentErrorValue = 0;
            foreach (var currentChar in line)
            {
                if (openingBrackets.Contains(currentChar)){
                    s.Push(currentChar);
                    continue;
                }

                var matchingClosingBracket = matchingDictionary[s.Pop()];
                if (currentChar != matchingClosingBracket){
                    currentErrorValue = scoringDictionary[currentChar];
                    error_score += currentErrorValue;
                    break;
                }

            }
            if (currentErrorValue == 0)
            {
                incompleteLines.Add(s);
            }
        }

        return (error_score, incompleteLines);
    }

    /// <summary>
    /// taking the list of stacks of unclosed brackets for each line and returning the "middle score"
    /// </summary>
    /// <param name="incompleteLines">stack of unclosed brackets from Part1</param>
    /// <returns>"middle score" compared to every line score</returns>
    public static ulong SolutionPart2(List<Stack<char>> incompleteLines)
    {
        var scores = new List<ulong>();
        foreach (var stack in incompleteLines)
        {
            var remainingOpenBrackets = stack.ToList();
            var lineScore = EvaluateRemainingBrackets(remainingOpenBrackets);
            scores.Add(lineScore);
        }
        var sortedScores =  scores.OrderBy(i => i).ToList();
        var middleScore = sortedScores[Convert.ToInt32(scores.Count / 2)];
        return middleScore;
    }

    /// <summary>
    /// evaluate the remaining brackets by the provided algorithm
    /// </summary>
    /// <param name="remainingOpenBrackets">list of remaining brackets</param>
    /// <returns>score for a given list of remaining brackets</returns>
    private static ulong EvaluateRemainingBrackets(List<char> remainingOpenBrackets)
    {
        ulong score = 0;
        var scoringDictionary = new Dictionary<char, ulong> {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 }
        };
        foreach (var item in remainingOpenBrackets)
        {
            score *= 5;
            score += scoringDictionary[item];
        }
        return score;
    }

    /// <summary>
    /// parsing given input file and returning List of string
    /// </summary>
    /// <param name="fileName">file name of input</param>
    /// <returns>List of strings of input file</returns>
    public static List<string> ParseInput(string fileName)
    {
        return File.ReadLines(fileName).ToList();
    }
}
