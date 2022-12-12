using System.Numerics;

namespace Advent_of_Code_2022_tests.Day11;
using Advent_of_Code_2022.Solutions.Day_11;

public class Day11Tests
{
    [Fact]
    public void CorrectPartOneAnswer()
    {

        var monkeys = Solution.ParseInput(File.ReadAllText("Day11/test.input.txt"));
        Solution.PlayRounds(monkeys, 20, true, 1);
        
        var monkeyBusiness = monkeys.OrderByDescending(monkey => monkey.InspectionCount)
            .Take(2)
            .Select(monkey => monkey.InspectionCount)
            .Aggregate(1, (a, b) => a * b);
        
        Assert.Equal(10605, monkeyBusiness);
    }

    [Fact]
    public void CorrectPartTwoAnswer()
    {
        var monkeys = Solution.ParseInput(File.ReadAllText("Day11/test.input.txt"));
        
        var clearAfter = monkeys.Select(monkey => monkey.TestValue).Aggregate((a, b) => a * b);
        Solution.PlayRounds(monkeys, 10000, false, clearAfter);

        var monkeyBusiness = monkeys.OrderByDescending(monkey => monkey.InspectionCount)
            .Take(2)
            .Select(monkey => monkey.InspectionCount)
            .Select(inspectionCount => new BigInteger(inspectionCount))
            .Aggregate(new BigInteger(1), (a, b) => a * b);
        
        Assert.Equal(new BigInteger(2713310158), monkeyBusiness);
    }
}