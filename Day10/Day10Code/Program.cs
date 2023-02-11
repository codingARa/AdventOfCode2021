namespace Day10Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day10!");
        var input = ParseInput("input.txt");
        var answer1 = SolutionPart1(input);
        Console.WriteLine($"Answer1: {answer1}");
    }

    /// <summary>
    /// find first illegal/mismatched character in corrupted line and return
    /// error score for input file
    /// </summary>
    /// <param name="input"></param>
    /// <returns>return error score of input file</returns>
    public static int SolutionPart1(List<string> input)
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

        foreach (var line in input)
        {
            Stack<char> q = new();
            foreach (var currentChar in line)
            {
                if (openingBrackets.Contains(currentChar)){
                    q.Push(currentChar);
                    continue;
                }

                var matchingClosingBracket = matchingDictionary[q.Pop()];
                if (currentChar != matchingClosingBracket){
                    var currentErrorValue = scoringDictionary[currentChar];
                    error_score += currentErrorValue;
                    break;
                }

            }
        }

        return error_score;
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
