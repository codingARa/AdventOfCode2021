namespace Day13Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day13!");
        var input = ParseInput("input.txt");

        var answer1 = SolutionPart1(input);
        Console.WriteLine($"Answer1: {answer1}\n");

        var coords = FoldingsSolutionPart2(input);
        var answer2 = CoordinateOutputString(coords);
        Console.WriteLine("Answer2:\n");
        Console.WriteLine(answer2);
    }

    /// <summary>
    /// count the list of remaining points after folding the coordinates once
    /// according the folding instructions
    /// </summary>
    /// <param name="input">tuple with: coordinates as list of tuples
    /// representing coordinates (x,y)</param>
    /// <returns>number of points after one fold</returns>
    public static int SolutionPart1((List<(int, int)>, List<(string, int)>) input)
    {
        List<(int, int)> coords = input.Item1;
        List<(string, int)> folds = input.Item2;

        var fold = folds.First();
        List<(int, int)> newCoords = GetNewCoordsAfterFolding(coords, fold);
        coords = newCoords;

        return coords.Count;
    }

    /// <summary>
    /// creating the final coordinate system, which can be outputed on console
    /// by performing all folds on given coordinates which can be outputed
    /// </summary>
    /// <param name="input">tuple with: coordinates as list of tuples
    /// representing coordinates (x,y)</param>
    /// <returns>tuple with: coordinates as list of tuples representing
    /// coordinates (x,y) and folding instructions as list of tuples
    /// representing (axis, value)</returns>
    public static List<(int, int)> FoldingsSolutionPart2(
        (List<(int, int)>, List<(string, int)>) input
    )
    {
        List<(int, int)> coords = input.Item1;
        List<(string, int)> folds = input.Item2;

        // iterating over all given folds
        foreach (var fold in folds)
        {
            var newCoords = GetNewCoordsAfterFolding(coords, fold);
            coords = newCoords;
        }
        return coords;
    }

    /// <summary>
    /// calculate the new coordinate after folding according to the folding
    /// instructions
    /// </summary>
    /// <param name="coords">coordinates as list of tuples representing
    /// coordinates (x,y)</param>
    /// <param name="fold">folding instructions as list of tuples representing
    /// (axis, value)</param>
    /// <returns>tuple with: new coordinates after the folding as list of
    /// tuples representing coordinates (x,y)</returns>
    private static List<(int, int)> GetNewCoordsAfterFolding(
        List<(int, int)> coords,
        (string, int) fold
    )
    {
        // list of coordinates of points after folding
        var newCoords = new List<(int, int)>();

        // case1: fold parallel to x-axis at yValueOfFoldingLine on y-axis
        if (fold.Item1 == "y")
        {
            var yValueOfFoldingLine = fold.Item2;
            // iterate over all given points for the given fold
            foreach (var coord in coords)
            {
                // ignore point, if already accounted for
                if (newCoords.Contains(coord))
                {
                    continue;
                }

                // Points below the folding line get mapped onto points above
                // the folding line.
                // Therefore points lower than folding line will be stored in
                // newCoords straight away.
                if (coord.Item2 < yValueOfFoldingLine)
                {
                    newCoords.Add(coord);
                    continue;
                }

                // calculated foldedY which is mirrored on yValueOfFoldingLine
                var foldedY = 2 * yValueOfFoldingLine - coord.Item2;
                var foldedCoord = (coord.Item1, foldedY);

                // check, if at the new position a point already exists
                if (!newCoords.Contains(foldedCoord))
                {
                    newCoords.Add(foldedCoord);
                }
            }
        }
        // case2: fold parallel to y-axis at xValueOfFoldingLine on x-axis
        else
        {
            var xValueOfFoldingLine = fold.Item2;
            // iterate over all given points for the given fold
            foreach (var coord in coords)
            {
                // ignore point, if already accounted for
                if (newCoords.Contains(coord))
                {
                    continue;
                }

                // The right-hand side is mapped onto left-hand side of the
                // coordinate system.
                // Therefore points left of the folding line will be stored in
                // newCoords straight away.
                if (coord.Item1 < xValueOfFoldingLine)
                {
                    newCoords.Add(coord);
                    continue;
                }

                // calculated foldedX which is mirrored on xValueOfFoldingLine
                var foldedX = 2 * xValueOfFoldingLine - coord.Item1;
                var foldedCoord = (foldedX, coord.Item2);

                // check, if at the new position a point already exists
                if (!newCoords.Contains(foldedCoord))
                {
                    newCoords.Add(foldedCoord);
                }
            }
        }

        return newCoords;
    }

    /// <summary>
    /// get the output string of coordinates as a necessary part of solution 2
    /// </summary>
    /// <param name="coords">list of tuple represents coordinates (x,y)</param>
    public static string CoordinateOutputString(List<(int, int)> coords)
    {
        int xMax = coords.Select(tuple => tuple.Item1).Max();
        int yMax = coords.Select(tuple => tuple.Item2).Max();

        string output = "";
        for (int y = 0; y <= yMax; y++)
        {
            for (int x = 0; x <= xMax; x++)
            {
                if (coords.Contains((x, y)))
                {
                    output += "#";
                }
                else
                {
                    output += " ";
                }
            }
            output += "\n";
        }
        return output;
    }

    /// <summary>
    /// parse the input in filename
    /// </summary>
    /// <param name="filename">name of input file</param>
    /// <returns>tuple with: coordinates as list of tuples representing
    /// coordinates (x,y) and folding instructions as list of tuples
    /// representing (axis, value)</returns>
    public static (List<(int, int)>, List<(string, int)>) ParseInput(string filename)
    {
        List<(string, int)> folds = new();
        List<(int, int)> coords = new();

        bool switchFromDotsToFolds = false;

        foreach (string line in File.ReadLines(filename))
        {
            // break between data types in inputFile
            if (string.IsNullOrEmpty(line))
            {
                switchFromDotsToFolds = true;
                continue;
            }

            // First part of input: Coordinates
            if (!switchFromDotsToFolds)
            {
                var coordStrings = line.Split(",");
                var x = int.Parse(coordStrings[0]);
                var y = int.Parse(coordStrings[1]);

                coords.Add((x, y));
            }
            // Second part of input after break: Folding Instructions
            else
            {
                var foldInstruction = line.Split(" ");
                var foldCoords = foldInstruction[2].Split("=");
                folds.Add((foldCoords[0], int.Parse(foldCoords[1])));
            }
        }

        return (coords, folds);
    }
}
