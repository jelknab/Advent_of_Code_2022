using Advent_of_Code_2022.Solutions.Day_02;

namespace Advent_of_Code_2022_tests.Day02;

public class Day02Tests
{
    [Fact]
    public void ProblemOneExpectedResultTest()
    {
        var parsedInput = Solution.ParseInputPartOne(File.ReadAllText("Day02/test.input.txt")).ToList();

        Assert.Equal(Solution.Outcome.Win, Solution.OutcomeForMatch(parsedInput[0].myHand, parsedInput[0].theirHand));
        Assert.Equal(Solution.Outcome.Lose, Solution.OutcomeForMatch(parsedInput[1].myHand, parsedInput[1].theirHand));
        Assert.Equal(Solution.Outcome.Draw, Solution.OutcomeForMatch(parsedInput[2].myHand, parsedInput[2].theirHand));
    }
    
    [Fact]
    public void ProblemTwoExpectedResultTest()
    {
        var parsedInput = Solution.ParseInputPartTwo(File.ReadAllText("Day02/test.input.txt")).ToList();

        Assert.Equal(Solution.PlayOption.Rock, Solution.PlayOptionForMatch(parsedInput[0].theirHand, parsedInput[0].outcome));
        Assert.Equal(Solution.PlayOption.Rock, Solution.PlayOptionForMatch(parsedInput[1].theirHand, parsedInput[1].outcome));
        Assert.Equal(Solution.PlayOption.Rock, Solution.PlayOptionForMatch(parsedInput[2].theirHand, parsedInput[2].outcome));
    }
}