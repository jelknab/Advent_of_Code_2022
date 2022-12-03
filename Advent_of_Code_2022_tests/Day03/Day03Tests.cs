using Advent_of_Code_2022.Solutions.Day_03;

namespace Advent_of_Code_2022_tests.Day03;

public class Day03Tests
{
    [Fact]
    public void CanSplitCompartments()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day03/test.input.txt")).ToList();

        var compartments = Solution.GetBackpackCompartments(parsedInput.First());

        Assert.Equal("vJrwpWtwJgWr", compartments.compartmentA);
        Assert.Equal("hcsFMMfFFhFp", compartments.ComportmentB);
    }

    [Fact]
    public void FindsSharedItem()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day03/test.input.txt")).ToList();

        var compartments = Solution.GetBackpackCompartments(parsedInput.First());
        var sharedItems = Solution.FindItemsInBothCompartments(compartments.compartmentA, compartments.ComportmentB);

        Assert.Equal(new[] { 'p' }, sharedItems.ToArray());
    }

    [Fact]
    public void ScoresCorrectly()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day03/test.input.txt")).ToList();

        var compartments = Solution.GetBackpackCompartments(parsedInput.First());
        var sharedItems = Solution.FindItemsInBothCompartments(compartments.compartmentA, compartments.ComportmentB);

        Assert.Equal(16, Solution.GetItemsPriorityScore(sharedItems));
        Assert.Equal(38, Solution.GetItemsPriorityScore(new []{'L'}));
        
        var totalPriorities = parsedInput
            .Select(Solution.GetBackpackCompartments)
            .Select(compartments => Solution.FindItemsInBothCompartments(compartments.compartmentA, compartments.ComportmentB))
            .Select(Solution.GetItemsPriorityScore)
            .Sum();
        
        
        Assert.Equal(157, totalPriorities);
    }

    [Fact]
    public void MakesCorrectGroups()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day03/test.input.txt")).ToList();

        var groups = Solution.GroupBackpacksBy3(parsedInput).ToArray();

        var expectedGroups = new[]
        {
            new[] { "vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg" },
            new[] { "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw" },
        };
        
        Assert.Equal(expectedGroups, groups);
    }

    [Fact]
    public void FindsMatchingItems()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day03/test.input.txt")).ToList();
        var badges = Solution.GroupBackpacksBy3(parsedInput)
            .Select(backpacks => Solution.FindItemsInAllBackpacks(backpacks).ToArray())
            .ToArray();
        
        var expectedBadges = new[]
        {
            new[] { 'r' },
            new[] { 'Z' },
        };
        
        Assert.Equal(expectedBadges, badges);
    }
}