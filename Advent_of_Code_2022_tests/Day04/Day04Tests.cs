namespace Advent_of_Code_2022_tests.Day04;
using Advent_of_Code_2022.Solutions.Day_04;

public class Day04Tests
{
    [Fact]
    public void CanParseInput()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day04/test.input.txt")).First();
        
        Assert.Equal(new [] {2, 4}, parsedInput.elfOne);
        Assert.Equal(new [] {6, 8}, parsedInput.elfTwo);
    }

    [Fact]
    public void ContainCheckWorks()
    {
        var containingCount = Solution
            .ParseInput(File.ReadAllText("Day04/test.input.txt"))
            .Select(pair => Solution.PairContainsOther(pair.elfOne, pair.elfTwo))
            .Count(contains => contains);
        
        Assert.Equal(2, containingCount);
    }

    [Fact]
    public void OverlapCheckWorks()
    {
        var containingCount = Solution
            .ParseInput(File.ReadAllText("Day04/test.input.txt"))
            .Select(pair => Solution.PairHasOverlap(pair.elfOne, pair.elfTwo))
            .Count(contains => contains);
        
        Assert.Equal(4, containingCount);
    }
}