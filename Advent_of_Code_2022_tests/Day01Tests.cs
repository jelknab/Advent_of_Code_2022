using Advent_of_Code_2022.Solutions.Day_01;

namespace Advent_of_Code_2022_tests;

public class Day01Tests
{
    [Fact]
    public void ProblemOneExpectedResultTest()
    {
        var solution = new Solution();
        Assert.Equal("Hello world!", solution.GetFirstAnswer());
    }
}