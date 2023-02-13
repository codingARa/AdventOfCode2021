namespace Day12Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day12!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input);
        Console.WriteLine($"Answer1: {answer1}");

        var answer2 = SolutionPart2(input);
        Console.WriteLine($"Answer2: {answer2}");
    }

    /// <summary>
    /// Searching all possible unique paths through the cave system from start
    /// to finish (breadth first search). Cave with lower case letters as names
    /// can only be traversed once in every valid path.
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

            // check all adjacent caves to the current cave
            foreach (var possibleNextCave in adjacentCaves)
            {
                // (1) skip cave, if it's the starting cave
                if (possibleNextCave == "start")
                {
                    continue;
                }

                // create nextPath element
                List<string> nextPath = new(currentPath) { possibleNextCave };

                // (2) append nextPath straight away, if cave is upper case
                if (char.IsUpper(possibleNextCave[0]))
                {
                    queueOfNotTerminatedPaths.Enqueue(nextPath);
                    continue;
                }

                // (3) append nextPath, if cave is lower case and not already
                // in path
                if (!currentPath.Contains(possibleNextCave))
                {
                    queueOfNotTerminatedPaths.Enqueue(nextPath);
                }
            }
        }
        return allPossiblePaths.Count;
    }

    /// <summary>
    /// Searching all possible unique paths through the cave system from start
    /// to finish (breadth first search). A single cave with lower case letters
    /// can be visited twice per path, every other lower case cave of a path
    /// can only be traversed once.
    /// </summary>
    /// <param name="connectionDict">dictionary with cave as key and list of
    /// adjacent caves as list</param>
    /// <returns>number of possible unique path</returns>
    public static int SolutionPart2(Dictionary<string, List<string>> connectionDict)
    {
        var allPossiblePaths = new Stack<List<string>>();

        var queueOfNotTerminatedPaths = new Queue<(List<string>, bool)>();
        queueOfNotTerminatedPaths.Enqueue((new List<string> { "start" }, false));

        while (queueOfNotTerminatedPaths.Count > 0)
        {
            var currentQ = queueOfNotTerminatedPaths.Dequeue();
            var currentPath = currentQ.Item1;
            var visitedTwice = currentQ.Item2;
            var currentPosition = currentPath.Last();

            // termination condition for current path
            if (currentPosition == "end")
            {
                allPossiblePaths.Push(currentPath);
                continue;
            }
            var adjacentCaves = connectionDict[currentPosition];

            // check all adjacent caves to the current cave
            foreach (var possibleNextCave in adjacentCaves)
            {
                // (1) skip cave, if it's the starting cave
                if (possibleNextCave == "start")
                {
                    continue;
                }

                // create nextPath element
                List<string> nextPath = new(currentPath) { possibleNextCave };

                // (2) append nextPath straight away, if cave is upper case
                if (char.IsUpper(possibleNextCave[0]))
                {
                    queueOfNotTerminatedPaths.Enqueue((nextPath, visitedTwice));
                    continue;
                }

                // (3a) append nextPath, if cave is lower case and not already
                // in path
                if (!currentPath.Contains(possibleNextCave))
                {
                    queueOfNotTerminatedPaths.Enqueue((nextPath, visitedTwice));
                }
                // (3b) append nextPath, if cave is lower and already in path,
                // but not lower case cave of current path has been visited
                // twice before
                else if (!visitedTwice)
                {
                    queueOfNotTerminatedPaths.Enqueue((nextPath, true));
                }
            }
        }
        return allPossiblePaths.Count;
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
