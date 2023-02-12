namespace Day11Code;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Let's solve Day10!");
        var nums = ParseInput("input.txt");

        var answer1 = SolutionPart1(nums, 100);
        Console.WriteLine($"Answer1: total number of flashes: {answer1.Item2}");

        var answer2 = SolutionPart2(nums);
        Console.WriteLine($"Answer2: All squids flash simultaneously at step: {answer2.Item2}");
    }

    public static (int[,], int) SolutionPart1(int[,] nums, int steps)
    {
        int flashes = 0;
        for (int s = 0; s < steps; s++)
        {
            nums = IncrementSquids(nums);

            int newFlashes;
            (nums, newFlashes) = CountFlashingSquids(nums);
            flashes += newFlashes;
        }

        return (nums, flashes);
    }
    public static (int[,], int) SolutionPart2(int[,] nums)
    {
        var didAllFlash = false;
        var step = 0;
        while (!didAllFlash)
        {
            step++;
            nums = IncrementSquids(nums);
            didAllFlash = DidAllSquidsFlashSimultaneously(nums, step);
        }
        return (nums, step);
    }

    /// <summary>
    /// increment energy level of all squids "flat" by one and then check, if
    /// the current own has reach level 10. If so, his energy level is
    /// temporarly set to -1 to indicate, that it is part of the current
    /// propagation wave. Members of the current propagation wave will be
    /// queued increment their neighbors, but they themselve are excluded from
    /// any further raising of their energy levels this simulation
    /// step/propagation wave.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns>current squid field, where each flashed squid gets the
    /// temporary value -1</returns>
    private static int[,] IncrementSquids(int[,] nums)
    {
        var totalRows = nums.GetLength(0);
        var totalCols = nums.GetLength(1);
        
        Queue<(int,int)> q = new();

        for (var row = 0; row < totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                // flat increment
                nums[row, col]++;

                // flashing
                if (nums[row, col] >= 10)
                {
                    nums[row,col] = -1;
                    q.Enqueue((row, col));
                }
            }
        }

        nums = EvalutePropagationQueue(nums, totalRows, totalCols, q);

        return nums;
    }
   
    /// <summary>
    /// Evaluate the current light flashing propagation wave of squids by using
    /// a queue. To do this, the neighborhood of each flashing squids is
    /// incremented and examined. New squids are queued, if they reach the
    /// energy level 10.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="totalRows">total count of rows for the given squid field</param>
    /// <param name="totalCols">total count of columns for the given squid field</param>
    /// <param name="q">inital Queue from IncrementSquid method</param>
    /// <returns></returns>
    private static int[,] EvalutePropagationQueue(int[,] nums, int totalRows, int totalCols, Queue<(int, int)> q)
    {
        while (q.Count > 0)
        {
            var coords = q.Dequeue();
            int[,] newNums;
            (int, int) newQ;
            var neighborhood = DetermineNeighborhood(totalRows, totalCols, coords);

            foreach (var neighbor in neighborhood)
            {
                (newNums, newQ) = IncrementEnergyOfNeighborAndQueueAgain(neighbor, nums);
                nums = newNums;
                if (newQ.Item1 != -1)
                {
                    q.Enqueue(newQ);
                }
            }
        }

        return nums;
    }
    
    /// <summary>
    /// Determine the local neigborhood of one flashing squid with its
    /// coordinate tuple (x,y)
    /// </summary>
    /// <param name="totalRows">total count of rows for the given squid field</param>
    /// <param name="totalCols">total count of columns for the given squid field</param>
    /// <param name="coords">tuple of coordinates (x,y) in the field of squids</param>
    /// <returns>List with allowed coordinates of the local neighborhood</returns>
    private static List<(int, int)> DetermineNeighborhood(int totalRows, int totalCols, (int, int) coords)
    {
        var row = coords.Item1;
        var col = coords.Item2;

        // cardinal directions
        var northAllowed = (row - 1 >= 0);
        var southAllowed = (row + 1 <= totalRows - 1);
        var westAllowed = (col - 1 >= 0);
        var eastAllowed = (col + 1 <= totalCols - 1);

        List<(int, int)> neighborhood = new() { };
        if (northAllowed)
            neighborhood.Add((row - 1, col));
        if (southAllowed)
            neighborhood.Add((row + 1, col));
        if (eastAllowed)
            neighborhood.Add((row, col + 1));
        if (westAllowed)
            neighborhood.Add((row, col - 1));
        // north-west
        if (northAllowed && westAllowed)
            neighborhood.Add((row - 1, col - 1));
        // north-east
        if (northAllowed && eastAllowed)
            neighborhood.Add((row - 1, col + 1));
        // south-west
        if(southAllowed && westAllowed)
            neighborhood.Add((row + 1, col - 1));
        // south-east
        if (southAllowed && eastAllowed)
            neighborhood.Add((row + 1, col + 1));
        return neighborhood;
    }

    /// <summary>
    /// Increment the energy level of a neighor of already flashing squids. The
    /// neighbor is checked, if they are themselve already flashing. If the
    /// neighbor gains enough energy to flash themselves, a corresponding queue
    /// element will be returned.
    /// </summary>
    /// <param name="coord">tuple (x,y) of a particular neigbor in the
    /// neighborhood of a flashing squid</param>
    /// <param name="nums">the current field of squids</param>
    /// <returns>tuple with the updated field of squids, and a potential new
    /// queue element for squids which reached the energy level to flash
    /// themselves</returns>
    private static (int[,], (int,int)) IncrementEnergyOfNeighborAndQueueAgain((int, int) coord, int[,] nums)
    {
        (int, int) newQ = (-1,-1);
        // increment, if it is not in the current propagation wave (-1) and
        // it has not enough energy to flash (10)
        if (nums[coord.Item1, coord.Item2] > 0 && nums[coord.Item1, coord.Item2] < 10)
        {
            nums[coord.Item1, coord.Item2]++;
        }
        // ad squid to current propagation wave with the newQ element and by
        // marking it as part of the current propagation wave, if above
        // increment tipped it's energy treshold to flash
        if (nums[coord.Item1, coord.Item2] >= 10)
        {
            nums[coord.Item1, coord.Item2] = -1;
            newQ = (coord.Item1, coord.Item2);
        }
        return (nums, newQ);
    }

    /// <summary>
    /// Count the all flashed squids in the current step, by finding all squids
    /// with value -1 and settings it back to the correct value 0.
    /// </summary>
    /// <param name="nums">squid field after running method EvalutePropagationQueue()</param>
    /// <returns>tuple with current field of squids (for debugging/testing) and the</returns>
    private static (int[,] nums, int newFlashes) CountFlashingSquids(int[,] nums)
    {
        var totalRows = nums.GetLength(0);
        var totalCols = nums.GetLength(1);
        int flashes = 0;
        for (var row = 0; row < totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                if (nums[row, col] == -1)
                {
                    nums[row, col] = 0;
                    flashes++;
                }
            }
        }
        return (nums, flashes);
    } 

    /// <summary>
    /// Determine, if all squids flash at once by counting all squids with
    /// value -1 and setting it back to the correct value of 0.
    /// </summary>
    /// <param name="nums">current field of squids</param>
    /// <returns>boolean, if all squids did flash at once</returns>
    private static bool DidAllSquidsFlashSimultaneously(int[,] nums, int steps)
    {
        if (steps == 224)
        {
            Console.WriteLine("Moin");
        }
        var totalRows = nums.GetLength(0);
        var totalCols = nums.GetLength(1);
        int flashes = 0;
        for (var row = 0; row < totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                if (nums[row, col] == -1)
                {
                    nums[row, col] = 0;
                    flashes++;
                }
            }
        }

        if (steps == 224)
        {
            Console.WriteLine("Moin");
        }

        return (flashes == 100);
    }

    /// <summary>
    /// parsing the given textfile
    /// </summary>
    /// <param name="fileName">name of the inputfile</param>
    /// <returns>2d array to represent the data</returns>
    public static int[,] ParseInput(string fileName)
    {
        string[] lines = File.ReadLines(fileName).ToArray();

        int[,] nums = new int[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                nums[i, j] = int.Parse(lines[i][j].ToString());
            }
        }
        return nums;
    }
}