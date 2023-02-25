using System.Text;

namespace Day14Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day14!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input.Item1, input.Item2, 10);
        Console.WriteLine($"Answer1: {answer1.Item1}\n");

        var answer2 = SolutionPart2(input.Item1, input.Item2, 40);
        Console.WriteLine($"Answer2: {answer2.Item1}\n");
    }

    /// <summary>
    /// parsing inputfile
    /// </summary>
    /// <param name="filename">filename of inputs</param>
    /// <returns>tuple of initial polymer Template (string) and Dictionary with
    /// insertion rules</returns>
    public static (string, Dictionary<string, string>) ParseInput(string filename)
    {
        string polymerTemplate = "";
        var insertionRules = new Dictionary<string, string>();

        bool switchInputType = false;

        foreach (string line in File.ReadLines(filename))
        {
            if (string.IsNullOrEmpty(line))
            {
                switchInputType = true;
                continue;
            }
            if (!switchInputType)
            {
                polymerTemplate = line;
            }
            else
            {
                var outp = line.Split(" -> ");
                insertionRules.Add(outp[0], outp[1]);
            }
        }

        return (polymerTemplate, insertionRules);
    }

    /// <summary>
    /// running steps for solution part 1
    /// </summary>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <param name="insertionRules">dictionary with pairs of elements as key
    /// and inserted element as value</param>
    /// <param name="steps">number of steps the process has to run</param>
    /// <returns>answer according to given equation and length of resulting polymer</returns>
    public static (long, long) SolutionPart1(
        string polymerTemplate,
        Dictionary<string, string> insertionRules,
        int steps
    )
    {
        var alphabet = insertionRules.SelectMany(x => x.Value).Distinct().ToList();
        var resultingPolymer = PolymerizeNaiveApproach(polymerTemplate, insertionRules, steps);
        var answer = CalculateScoreNaiveApproach(resultingPolymer, alphabet);
        return (answer, resultingPolymer.Count());
    }

    /// <summary>
    /// build resulting polymer as a string
    /// (1) starting with the polymerTemplate
    /// (2) iterate the given steps
    /// (3) iterate synchronous insertion of elements according to insertionRules in dictionary
    /// </summary>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <param name="insertionRules">dictionary with pairs of elements as key
    /// and inserted element as value</param>
    /// <param name="steps">number of steps the process has to run</param>
    /// <returns>resulting polymer as a string representation</returns>
    public static string PolymerizeNaiveApproach(
        string polymerTemplate,
        Dictionary<string, string> insertionRules,
        int steps
    )
    {
        string leftAtom = "";
        string rightAtom = "";
        string polymer = polymerTemplate;

        // iterate for given steps
        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            // initialize variable to build new polymer chain for current step
            var polymerWithInsertedAtoms = new StringBuilder();

            // iterate over pairs of atoms (leftAtom,rightAtom) in polymer
            for (int atomIndex = 0; atomIndex < polymer.Length - 1; atomIndex++)
            {
                leftAtom = char.ToString(polymer[atomIndex]);
                rightAtom = char.ToString(polymer[atomIndex + 1]);
                string insertionPair = leftAtom + rightAtom;
                var insertAtom = insertionRules[insertionPair];
                polymerWithInsertedAtoms.Append(leftAtom);
                polymerWithInsertedAtoms.Append(insertAtom);
            }
            // append last atom, since no atom needs to be inserted
            polymerWithInsertedAtoms.Append(rightAtom);

            // polymer of next step is polymerWithInsertedAtoms
            polymer = polymerWithInsertedAtoms.ToString();
        }

        return polymer;
    }

    /// <summary>
    /// calculate answer according to given equation
    /// </summary>
    /// <param name="finalPolymer">final polymer as string of elements</param>
    /// <param name="alphabet">all possible elements in final polymer</param>
    /// <returns>(count of most frequent element in final polymer) - (count of
    /// least frequent element in final polymer)</returns>
    public static long CalculateScoreNaiveApproach(string finalPolymer, List<char> alphabet)
    {
        long minCount = long.MaxValue;
        var minKey = "";
        long maxCount = -1;
        var maxKey = "";

        // tally is used for debugging
        var tally = new Dictionary<string, long>();

        foreach (var letter in alphabet)
        {
            long letterCount = finalPolymer.Count(c => c == letter);
            var letterString = char.ToString(letter);
            tally.Add(letterString, letterCount);

            if (letterCount < minCount)
            {
                minCount = letterCount;
                minKey = letterString;
            }
            if (letterCount > maxCount)
            {
                maxCount = letterCount;
                maxKey = letterString;
            }
        }

        return maxCount - minCount;
    }

    /// <summary>
    /// Part 2 needs a different approach than Part 1, because building the
    /// resulting polymer string is too large to store in memory!
    /// Estimate for result:
    /// ===================:
    /// length of resulting string:
    ///     (length of polymerTemplate - 1) * 2^(steps) = 19*2^40
    ///     \approx 2*10^13
    /// estimate for memory usage of a string in C#:
    ///     10^9 Char in String \approx 2 GB of Memory
    /// estimate for memory usage of resulting string:
    ///     2GB * (2*10^13)/(10^9) = 40 000 GB = 40 TB of data
    /// </summary>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <param name="insertionRules">dictionary with pairs of elements as key
    /// and inserted element as value</param>
    /// <param name="steps">number of steps the process has to run</param>
    /// <returns>answer according to given equation</returns>
    public static (ulong, ulong) SolutionPart2(
        string polymerTemplate,
        Dictionary<string, string> insertionRules,
        int steps
    )
    {
        // dictionary with all possible element pairs of the polymerization as
        // keys and their occurances as value (the count of pairs in
        // polymerTemplate already done)
        var givenPolymerPairs = GetInitalPolymerPairs(polymerTemplate, insertionRules);

        // iterate for given steps
        for (int s = 0; s < steps; s++)
        {
            // initialize empty dictionary (zeros as values) with all possible
            // element pairs in the polymerization as keys
            var nextPairs = GetEmptyPairDictionaryFromInsertionRules(insertionRules);

            // iterate over all possible pairs in givenPolymerPairs
            foreach (var possiblePair in givenPolymerPairs)
            {
                // only operate, if on occurance has been noted already
                if (possiblePair.Value > 0)
                {
                    // get new insertAtom by current possiblePair
                    var insertAtom = insertionRules[possiblePair.Key];

                    // insert the new left pair of the insertion into new
                    // dictionary nextPairs
                    var leftpair = possiblePair.Key[0] + insertAtom;
                    nextPairs[leftpair] += possiblePair.Value;

                    // insert the new right pair of the insertion into new
                    // dictionary nextPairs
                    var rightpair = insertAtom + possiblePair.Key[1];
                    nextPairs[rightpair] += possiblePair.Value;
                }
            }

            // switch givenPolymerPairs with nextPairs for next step
            givenPolymerPairs = nextPairs;
        }

        return CalculateScoreSolution2(insertionRules, givenPolymerPairs, polymerTemplate);
    }

    /// <summary>
    /// calculate the score for solution two
    /// </summary>
    /// <param name="insertionRules">dictionary with pairs of elements as key
    /// and inserted element as value</param>
    /// <param name="finalPolymerPairs">dictionary of all possible atom pairs
    /// in polymer and their occurances as value after polymerization
    /// process</param>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <returns>return the score and the length of the final polymer</returns>
    private static (ulong, ulong) CalculateScoreSolution2(
        Dictionary<string, string> insertionRules,
        Dictionary<string, ulong> finalPolymerPairs,
        string polymerTemplate
    )
    {
        var tally = GetInitialTallyFromInsertionRules(insertionRules);
        ulong pairCount = 0;

        // count only the right atom of each pair to not count each atom twice
        foreach (var finalpair in finalPolymerPairs)
        {
            tally[finalpair.Key[1]] += finalpair.Value;
            pairCount += finalpair.Value;
        }
        // add 1 for the first (left) atom in the polymer chain which stays
        // fixed for the whole polymerization process
        tally[polymerTemplate[0]] += 1;
        pairCount++;

        // get minimal and maximal value in tally
        ulong minValue = ulong.MaxValue;
        ulong maxValue = 0;
        foreach (var res in tally)
        {
            if (res.Value > maxValue)
            {
                maxValue = res.Value;
            }

            if (res.Value < minValue && res.Value > 0)
            {
                minValue = res.Value;
            }
        }

        return (maxValue - minValue, pairCount);
    }

    /// <summary>
    /// building dictionary with all possible pairs as keys and the count of
    /// inital polymer pairs from polymerTemplate placed as values
    /// </summary>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <param name="insertionRules">dictionary with insertion rules key:(possiblePair
    /// of elements), value:(element to insertAtom)</param>
    /// <returns></returns>
    public static Dictionary<string, ulong> GetInitalPolymerPairs(
        string polymerTemplate,
        Dictionary<string, string> insertionRules
    )
    {
        var initialDict = GetInitalDictionaryFromTemplatePolymer(polymerTemplate);

        var polymerPairs = new Dictionary<string, ulong>();
        foreach (var pair in insertionRules.Keys)
        {
            if (initialDict.ContainsKey(pair))
            {
                polymerPairs.Add(pair, initialDict[pair]);
            }
            else
            {
                polymerPairs.Add(pair, 0);
            }
        }
        return polymerPairs;
    }

    /// <summary>
    /// return an empty tally dictionary (all values zero) from insertion rules
    /// </summary>
    /// <param name="insertionRules">dictionary with insertion rules key:(possiblePair
    /// of elements), value:(element to insertAtom)</param>
    /// <returns>tally dictionary with all possible elements in polymer pairs
    /// and all zeros as values</returns>
    private static Dictionary<char, ulong> GetInitialTallyFromInsertionRules(
        Dictionary<string, string> insertionRules
    )
    {
        var tally = new Dictionary<char, ulong>();
        var alphabet = insertionRules.SelectMany(x => x.Value).Distinct().ToList();

        foreach (var letter in alphabet)
        {
            tally.Add(letter, 0);
        }

        return tally;
    }

    /// <summary>
    /// building dictionary with pairs from polymerTemplate as keys and their
    /// count as values
    /// </summary>
    /// <param name="polymerTemplate">starting polymer as a string of elements</param>
    /// <returns>see summary</returns>
    public static Dictionary<string, ulong> GetInitalDictionaryFromTemplatePolymer(
        string polymerTemplate
    )
    {
        var polymerPairs = new Dictionary<string, ulong>();

        for (int atomIndex = 0; atomIndex < polymerTemplate.Length - 1; atomIndex++)
        {
            var leftAtom = char.ToString(polymerTemplate[atomIndex]);
            var rightAtom = char.ToString(polymerTemplate[atomIndex + 1]);
            string insertionPair = leftAtom + rightAtom;
            if (polymerPairs.ContainsKey(insertionPair))
            {
                polymerPairs[insertionPair] += 1;
            }
            else
            {
                polymerPairs.Add(insertionPair, 1);
            }
        }

        return polymerPairs;
    }

    /// <summary>
    /// build a dictionary with all possible pairs as keys from insertion rule
    /// dictionary and only zeros as value
    /// </summary>
    /// <param name="insertionRules">dictionary with insertion rules key:(possiblePair
    /// of elements), value:(element to insertAtom)</param>
    /// <returns>see summary</returns>
    public static Dictionary<string, ulong> GetEmptyPairDictionaryFromInsertionRules(
        Dictionary<string, string> insertionRules
    )
    {
        var emptyPairs = new Dictionary<string, ulong>();
        foreach (var elem in insertionRules)
        {
            emptyPairs.Add(elem.Key, 0);
        }
        return emptyPairs;
    }
}
