using Advent_of_Code_2022.Solutions.Day_01;

namespace Advent_of_Code_2022_tests.Day01;

public class Day01Tests
{
    [Fact]
    public void ProblemOneExpectedResultTest()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day01/test.input.txt"));
        Assert.Equal(24000, Solution.FindHighestCalories(parsedInput));
    }
    
    [Fact]
    public void ProblemTwoExpectedResultTest()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day01/test.input.txt"));
        Assert.Equal(45000, Solution.SumTop3HighestCalories(parsedInput));
    }
}