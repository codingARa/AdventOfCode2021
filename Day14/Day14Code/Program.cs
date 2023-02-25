using System;
using System.Text;

namespace Day14Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day14!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input.Item1, input.Item2, 10);
        Console.WriteLine($"Answer1: {answer1}\n");

        var answer2 = DifferentApproach(input.Item1, input.Item2, 40);
        Console.WriteLine($"Answer2: {answer2}\n");

        // too low result: 2856688823348
        ulong prevToLow = 2856688823348;
        Console.WriteLine($"possible improvement: {answer2 > prevToLow}");

    }

    public static long SolutionPart1(string polymerTemplate, Dictionary<string, string> insertionRules, int steps)
    {
        var alphabet = insertionRules.SelectMany(x => x.Value).Distinct().ToList();
        var resultingPolymer = PolymerizeNaiveApproach(polymerTemplate, insertionRules, steps);
        var answer = CalculateScoreNaiveApproach(resultingPolymer, alphabet);
        //foreach(var elem in answer.Item2)
        //{
        //    Console.WriteLine($"key: {elem.Key}, value: {elem.Value}");
        //}
        return answer.Item1;
    }

    public static ulong DifferentApproach(string polymerTemplate, Dictionary<string, string> insertionRules, int steps)
    {
        var initialDict = GetInitalDictionaryFromTemplatePolymer(polymerTemplate);

        var polymerPairs = new Dictionary<string, ulong>();
        foreach(var pair in insertionRules.Keys)
        {
            if (initialDict.ContainsKey(pair)){
                polymerPairs.Add(pair, initialDict[pair]);
            }
            else
            {
                polymerPairs.Add(pair, 0);
            }
        }

        for (int s = 0; s < steps; s++)
        {
            var nextPairs = GetEmptyPairDictionaryFromInsertionRules(insertionRules);
            foreach(var elem in polymerPairs)
            {
                if(elem.Value > 0)
                {
                    var insert = insertionRules[elem.Key];

                    var leftpair = elem.Key[0] + insert;
                    if (nextPairs.ContainsKey(leftpair))
                    {
                        nextPairs[leftpair] += elem.Value;
                    }
                    else
                    {
                        nextPairs.Add(leftpair, elem.Value);
                    }

                    var rightpair = insert + elem.Key[1];
                    if (nextPairs.ContainsKey(rightpair))
                    {
                        nextPairs[rightpair] += elem.Value;
                    }
                    else
                    {
                        nextPairs.Add(rightpair, elem.Value);
                    } 
                }
            }
            polymerPairs = nextPairs;
        }

        var tally = GetInitialTallyFromInsertionRules(insertionRules);

        foreach (var finalpair in polymerPairs)
        {
            tally[finalpair.Key[1]] += finalpair.Value;
        }

        ulong minValue = ulong.MaxValue;
        char minKey = char.MinValue;
        ulong maxValue = 0;
        char maxKey = char.MinValue;
        bool firstNonZero = true;

        foreach (var res in tally)
        {
            if (res.Value > maxValue)
            {
                maxValue = res.Value;
                maxKey = res.Key;
            }

            if (firstNonZero && res.Value > 0)
            {
                firstNonZero = false;
                minValue = res.Value;
                minKey = res.Key;
            }
            else if(res.Value < minValue)
            {
                minValue = res.Value;
                minKey = res.Key;
            } 
        }
        Console.WriteLine($"Max {maxKey}: {maxValue}");
        Console.WriteLine($"Min {minKey}: {minValue}");

        return maxValue - minValue;
    }

    private static Dictionary<char, ulong> GetInitialTallyFromInsertionRules(Dictionary<string, string> insertionRules)
    {
        var tally = new Dictionary<char, ulong>();
        var alphabet = insertionRules.SelectMany(x => x.Value).Distinct().ToList();

        foreach (var letter in alphabet)
        {
            tally.Add(letter, 0);
        }

        return tally;
    }

    public static Dictionary<string, ulong> GetInitalDictionaryFromTemplatePolymer(string polymerTemplate)
    {
        var polymerPairs = new Dictionary<string, ulong>();

        for (int atomIndex = 0; atomIndex < polymerTemplate.Length-1; atomIndex++)
        {
            var leftAtom = char.ToString(polymerTemplate[atomIndex]);
            var rightAtom = char.ToString(polymerTemplate[atomIndex+1]);
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

    public static Dictionary<string, ulong> GetEmptyPairDictionaryFromInsertionRules(Dictionary<string,string> insertionRules)
    {
        var emptyPairs = new Dictionary<string, ulong>();
        foreach(var elem in insertionRules)
        {
            emptyPairs.Add(elem.Key, 0);
        }
        return emptyPairs;
    }

    public static string PolymerizeNaiveApproach(string polymerTemplate, Dictionary<string, string> insertionRules, int steps)
    {
        string leftAtom = "";
        string rightAtom = "";
        string resultingPolymer = polymerTemplate;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            var buildingPolymerChain = new StringBuilder();

            for (int atomIndex = 0; atomIndex < resultingPolymer.Length-1; atomIndex++)
            {
                leftAtom = char.ToString(resultingPolymer[atomIndex]);
                rightAtom = char.ToString(resultingPolymer[atomIndex+1]);
                string insertionPair = leftAtom + rightAtom;
                var insertAtom = insertionRules[insertionPair];
                buildingPolymerChain.Append(leftAtom);
                buildingPolymerChain.Append(insertAtom);
            }
            buildingPolymerChain.Append(rightAtom);
            resultingPolymer = buildingPolymerChain.ToString();
        }

        return resultingPolymer;
    }

    public static (long, Dictionary<string, long>) CalculateScoreNaiveApproach(string finalPolymer, List<char> alphabet)
    {
        long minCount = long.MaxValue;
        var minKey = "";
        long maxCount = -1;
        var maxKey = "";

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
            if(letterCount > maxCount)
            {
                maxCount = letterCount;
                maxKey = letterString;
            }
        }

        return (maxCount - minCount, tally);
    }

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
}
