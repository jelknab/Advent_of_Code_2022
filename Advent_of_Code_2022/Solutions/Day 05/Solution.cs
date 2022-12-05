using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2022.Solutions.Day_05;

public class Solution: ISolution
{
    private static readonly Regex CrateStackIndicesRegex = new(@"^(\s+\d+\s+)+$", RegexOptions.Compiled);
    private static readonly Regex CommandRegex = new(@"move\s(?<amount>\d+)\sfrom\s(?<from>\d)\sto\s(?<to>\d)", RegexOptions.Compiled | RegexOptions.Multiline);
    
    public static (ImmutableArray<ImmutableStack<char>> crateStacks, (int amount, int fromIndex, int toIndex)[] commands) ParseInput(string input)
    {
        var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToArray();

        var indicesLine = lines.Select((line, index) => (line, index)).First(line => CrateStackIndicesRegex.Match(line.line).Success);
        
        var indicesCount = indicesLine.line.Replace(" ", "").Length;
        var commands = CommandRegex.Matches(input).Select(match => (
            int.Parse(match.Groups["amount"].Value), 
            int.Parse(match.Groups["from"].Value) - 1, 
            int.Parse(match.Groups["to"].Value) - 1
        )).ToArray();

        var crateStacks = Enumerable.Range(0, indicesCount).Select(_ => new Stack<char>()).ToArray();
        foreach (var crateInput in lines.Take(indicesLine.index))
        {
            for (var index = 0; index < indicesCount; index++)
            {
                var crate = crateInput[ 1 + index * 4];
                if (crate != ' ')
                {
                    crateStacks[index].Push(crate);
                }
            }
        }
                

        var immutableStacks = crateStacks.Select(stack => ImmutableStack.Create(stack.ToArray())).ToArray();
        return (ImmutableArray.Create(immutableStacks), commands);
    }
    
    public static ImmutableArray<ImmutableStack<char>> ExecuteMoveCommand9000(ImmutableArray<ImmutableStack<char>> stacks, int amount, int fromIndex, int toIndex)
    {
        var fromStack = stacks[fromIndex];
        var toStack = stacks[toIndex];

        for (var i = 0; i < amount; i++)
        {
            fromStack = fromStack.Pop(out var crate);
            toStack = toStack.Push(crate);
        }

        return stacks.SetItem(fromIndex, fromStack).SetItem(toIndex, toStack);
    }
    
    public static ImmutableArray<ImmutableStack<char>> ExecuteMoveCommand9001(ImmutableArray<ImmutableStack<char>> stacks, int amount, int fromIndex, int toIndex)
    {
        var fromStack = stacks[fromIndex];
        var toStack = stacks[toIndex];

        var taken = new Stack<char>();

        for (var i = 0; i < amount; i++)
        {
            fromStack = fromStack.Pop(out var crate);
            taken.Push(crate);
        }

        toStack = taken.Aggregate(toStack, (current, crate) => current.Push(crate));

        return stacks.SetItem(fromIndex, fromStack).SetItem(toIndex, toStack);
    }
    
    public string GetFirstAnswer()
    {
        var parsedInput = ParseInput(File.ReadAllText("Solutions/Day 05/input.txt"));

        var topOfStacks = parsedInput.commands
            .Aggregate(parsedInput.crateStacks, (current, inputCommand) => ExecuteMoveCommand9000(current, inputCommand.amount, inputCommand.fromIndex, inputCommand.toIndex))
            .Select(stack => stack.Peek())
            .ToArray();

        return new string(topOfStacks);
    }

    public string GetSecondAnswer()
    {
        var parsedInput = ParseInput(File.ReadAllText("Solutions/Day 05/input.txt"));

        var topOfStacks = parsedInput.commands
            .Aggregate(parsedInput.crateStacks, (current, inputCommand) => ExecuteMoveCommand9001(current, inputCommand.amount, inputCommand.fromIndex, inputCommand.toIndex))
            .Select(stack => stack.Peek())
            .ToArray();

        return new string(topOfStacks);
    }
}