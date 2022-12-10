namespace Advent_of_Code_2022_tests.Day09;
using Advent_of_Code_2022.Solutions.Day_09;

public class Day09Tests
{

    [Fact]
    public void InputParserWorks()
    {
        var commands = Solution.ParseInput(File.ReadAllText("Day09/test.input.txt")).ToArray();
        
        Assert.Equal(Solution.Direction.Right, commands[0].direction);
        Assert.Equal(4, commands[0].amount);
        
        Assert.Equal(Solution.Direction.Up, commands[1].direction);
        Assert.Equal(4, commands[1].amount);
    }

    [Fact]
    public void ExpectedResultPart1()
    {
        var commands = Solution.ParseInput(File.ReadAllText("Day09/test.input.txt")).ToArray();
        var positions = Solution.TrackTail(Solution.HeadPositions(commands)).Distinct();
        
        Assert.Equal(13, positions.Count());
    }

    [Fact]
    public void ExpectedResultPart2()
    {
        var commands = Solution.ParseInput(File.ReadAllText("Day09/test1.input.txt")).ToArray();
        var nthPosition = Solution.TrackTail(Solution.HeadPositions(commands));

        for (var i = 0; i < 8; i++)
        {
            nthPosition = Solution.TrackTail(nthPosition);
        }

        Assert.Equal(36, nthPosition.Distinct().Count());
    }
}