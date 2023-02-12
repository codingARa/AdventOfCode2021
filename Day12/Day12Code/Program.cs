namespace Day12Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day12!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input);
        Console.WriteLine($"Answer1: {answer1}");
    }

    /// <summary>
    /// Searching all possible unique paths through the cave system from start
    /// to finish. Cave with lower case letters as names can only be traversed
    /// once in every valid path.
    /// </summary>
    /// <param name="connectionDict">dictionary with cave as key and list of
    /// adjacent caves as list</param>
    /// <returns>number of possible unique path</returns>
    public static int SolutionPart1(Dictionary<string, List<string>> connectionDict)
    {
        var allPossiblePaths = new Stack<List<string>>();

        var queueOfNotTerminatedPaths = new Queue<List<string>>();
        queueOfNotTerminatedPaths.Enqueue(new List<string> { "start" });

        while (queueOfNotTerminatedPaths.Count > 0)
        {
            var currentPath = queueOfNotTerminatedPaths.Dequeue();
            var currentPosition = currentPath.Last();

            // termination condition for current path
            if (currentPosition == "end")
            {
                allPossiblePaths.Push(currentPath);
                continue;
            }
            var adjacentCaves = connectionDict[currentPosition];

            // check all adjacent cave, if they are:
            // (1) the starting cave => skip
            // (2) lower case caves => skip, if they are part of the path
            //     already
            // (3) in every other case: form a new extended path and queue it
            //     to be progressed in a later iteration
            foreach (var possibleNextCave in adjacentCaves)
            {
                if (possibleNextCave != "start") // (1)
                {
                    if (char.IsLower(possibleNextCave[0])) // (2)
                    {
                        if (!currentPath.Contains(possibleNextCave))
                        {
                            // (3)
                            List<string> nextPath = new List<string>(currentPath)
                            {
                                possibleNextCave
                            };
                            queueOfNotTerminatedPaths.Enqueue(nextPath);
                        }
                    }
                    else
                    {
                        // (3)
                        List<string> nextPath = new List<string>(currentPath) { possibleNextCave };
                        queueOfNotTerminatedPaths.Enqueue(nextPath);
                    }
                }
            }
        }
        return allPossiblePaths.Count();
    }

    /// <summary>
    /// Parse given input and create a Dictionary with all connections of the
    /// cave system.
    /// </summary>
    /// <param name="filename">name of input file</param>
    /// <returns>dictionary with cave as key and list of adjacent caves as
    /// list</returns>
    public static Dictionary<string, List<string>> ParseInput(string filename)
    {
        var connectionDict = new Dictionary<string, List<string>>();

        var input = File.ReadLines(filename).ToList();
        foreach (var connectionstring in input)
        {
            var parts = connectionstring.Split("-");
            if (connectionDict.ContainsKey(parts[0]))
            {
                connectionDict[parts[0]].Add(parts[1]);
            }
            else
            {
                connectionDict.Add(parts[0], new List<string>() { parts[1] });
            }

            if (connectionDict.ContainsKey(parts[1]))
            {
                connectionDict[parts[1]].Add(parts[0]);
            }
            else
            {
                connectionDict.Add(parts[1], new List<string>() { parts[0] });
            }
        }

        return connectionDict;
    }
}
