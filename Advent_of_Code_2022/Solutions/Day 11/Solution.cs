using System.Numerics;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2022.Solutions.Day_11;

public class Item
{
    public long WorryLevel { get; set; }
}

public enum Operation
{
    Multiply = '*',
    Add = '+'
}

public class Monkey
{
    public List<Item> Items { get; set; } = new();
    
    public Operation Operation { get; set; }
    
    public long OperationValue { get; set; }
    
    public int TestValue { get; set; }
    
    public int TrueTestMonkey { get; set; }
    
    public int FalseTestMonkey { get; set; }

    public int InspectionCount { get; set; } = 0;

    public void ThrowItems(Monkey[] monkeys, bool worry, long clearAfter)
    {
        Items.ForEach(item =>
        {
            InspectionCount++;

            var operationValue = this.OperationValue == -1 ? item.WorryLevel : this.OperationValue;
            
            switch (Operation)
            {
                case Operation.Multiply:
                    item.WorryLevel *= operationValue;
                    break;
                case Operation.Add:
                    item.WorryLevel += operationValue;
                    break;
            }

            if (worry)
            {
                item.WorryLevel /= 3; //Integer division = Floor division
            }
            else
            {
                item.WorryLevel %= clearAfter;
            }

            if (item.WorryLevel % TestValue == 0)
            {
                monkeys[TrueTestMonkey].Items.Add(item);
            }
            else
            {
                monkeys[FalseTestMonkey].Items.Add(item);
            }
        });
        
        Items.Clear();
    }
}

public class Solution: ISolution
{
    private static readonly Regex NumberRegex = new ("(\\d+)", RegexOptions.Compiled);
    private static readonly Regex OperationRegex = new ("(?<operation>[\\*\\+])\\s(?<OperationValue>\\d+|old)", RegexOptions.Compiled);
    
    public static Monkey[] ParseInput(string input)
    {
        return input.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None)
            .Select(monkeyString =>
            {
                var lines = monkeyString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToArray();
                var startingItemWorryLevels = NumberRegex
                    .Matches(lines[1])
                    .Select(group => group.Value)
                    .Select(int.Parse)
                    .Select(worryLevel => new Item { WorryLevel = worryLevel });

                var operationMatch = OperationRegex.Match(lines[2]);
                var operation = (Operation) operationMatch.Groups["operation"].Value[0];
                var unparsedOperationValue = operationMatch.Groups["OperationValue"].Value;

                return new Monkey
                {
                    Items = startingItemWorryLevels.ToList(),
                    Operation = operation,
                    OperationValue = unparsedOperationValue.Equals("old") ? -1 : int.Parse(unparsedOperationValue),
                    TestValue = int.Parse(NumberRegex.Match(lines[3]).Value),
                    TrueTestMonkey = int.Parse(NumberRegex.Match(lines[4]).Value),
                    FalseTestMonkey = int.Parse(NumberRegex.Match(lines[5]).Value)
                };
            })
            .ToArray();
    }

    public static void PlayRounds(Monkey[] monkeys, int rounds, bool worry, long clearAfter)
    {
        for (var i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.ThrowItems(monkeys, worry, clearAfter);
            }
        }
    }

    public string GetFirstAnswer()
    {
        var monkeys = ParseInput(File.ReadAllText("Solutions/Day 11/input.txt"));
        PlayRounds(monkeys, 20, true, 1);

        return monkeys.OrderByDescending(monkey => monkey.InspectionCount)
            .Take(2)
            .Select(monkey => monkey.InspectionCount)
            .Aggregate(1, (a, b) => a * b)
            .ToString();
    }

    public string GetSecondAnswer()
    {
        var monkeys = ParseInput(File.ReadAllText("Solutions/Day 11/input.txt"));
        
        var clearAfter = monkeys.Select(monkey => monkey.TestValue).Aggregate((a, b) => a * b);
        PlayRounds(monkeys, 10000, false, clearAfter);

        return monkeys.OrderByDescending(monkey => monkey.InspectionCount)
            .Take(2)
            .Select(monkey => monkey.InspectionCount)
            .Select(inspectionCount => new BigInteger(inspectionCount))
            .Aggregate(new BigInteger(1), (a, b) => a * b)
            .ToString();
    }
}