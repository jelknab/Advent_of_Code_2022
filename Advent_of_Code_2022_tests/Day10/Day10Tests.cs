using Xunit.Abstractions;

namespace Advent_of_Code_2022_tests.Day10;
using Advent_of_Code_2022.Solutions.Day_10;

public class Day10Tests
{
    [Fact]
    public void PartOneWorks()
    {
        var indices = new[] { 20, 60, 100, 140, 180, 220 };

        var signalStrengthSum = Solution.RunCommand(Solution.ParseInput(File.ReadAllText("Day10/test.input.txt")))
            .Select((x, index) => (x, index))
            .Where(xAndI => indices.Contains(xAndI.index + 1))
            .Select(xAndI => (xAndI.index + 1) * xAndI.x)
            .Sum();
        
        Assert.Equal(13140, signalStrengthSum);
    }
}