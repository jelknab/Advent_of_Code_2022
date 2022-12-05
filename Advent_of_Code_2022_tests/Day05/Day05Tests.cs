namespace Advent_of_Code_2022_tests.Day05;
using Advent_of_Code_2022.Solutions.Day_05;

public class Day05Tests
{
    [Fact]
    void ExpectedOutcome9000()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day05/test.input.txt"));

        var stacks = parsedInput.commands
            .Aggregate(parsedInput.crateStacks, (current, inputCommand) => Solution.ExecuteMoveCommand9000(
                    current,
                    inputCommand.amount,
                    inputCommand.fromIndex,
                    inputCommand.toIndex
                )
            );
        
        Assert.Equal("CMZ", new string(stacks.Select(stack => stack.Peek()).ToArray()));
    }
    
    [Fact]
    void ExpectedOutcome9001()
    {
        var parsedInput = Solution.ParseInput(File.ReadAllText("Day05/test.input.txt"));

        var stacks = parsedInput.commands
            .Aggregate(parsedInput.crateStacks, (current, inputCommand) => Solution.ExecuteMoveCommand9001(
                    current,
                    inputCommand.amount,
                    inputCommand.fromIndex,
                    inputCommand.toIndex
                )
            );
        
        Assert.Equal("MCD", new string(stacks.Select(stack => stack.Peek()).ToArray()));
    }
}