using Advent_of_Code_2022.Solutions;


// Program accepts optional [day] argument
// No argument = current day

var day = args.Select(arg => Convert.ToInt32(arg)).FirstOrDefault(DateTime.Today.Day);
var daySolution = GetSolutionInstanceForDay(day);

Console.WriteLine($"Running day {day}!");
Console.WriteLine();
PrintAnswer(1, daySolution.GetFirstAnswer);
Console.WriteLine();
PrintAnswer(2, daySolution.GetSecondAnswer);

static void PrintAnswer(int problemNumber, Func<string> solver)
{
    try
    {
        var solution = solver();
        Console.WriteLine($"Problem {problemNumber}: ");
        Console.WriteLine(solution);
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"Problem {problemNumber} has yet to be solved.");
    }
}

static ISolution GetSolutionInstanceForDay(int day)
{
    var dayWithLeadingZeros = day.ToString("D2"); 
    var assemblyPathToDaySolution = $"Advent_of_Code_2022.Solutions.Day_{dayWithLeadingZeros}.Solution";
    var solutionType = Type.GetType(assemblyPathToDaySolution) ?? throw new Exception($"No class found at: {assemblyPathToDaySolution}");
    if (!solutionType.GetInterfaces().Contains(typeof(ISolution)))
    {
        throw new Exception("Solution must extend ISolution interface");
    }
    var dayInstance = (ISolution?) Activator.CreateInstance(solutionType) ?? throw new Exception("Could not make instance of today's challenge.");

    return dayInstance;
}