using System.Text.RegularExpressions;

namespace Advent_of_Code_2022.Solutions.Day_01;

public class Solution: ISolution
{
    private static readonly Regex InputRegex = new(@"((?<calories>\d+)\n?)+\n?", RegexOptions.Multiline);
    
    public static IEnumerable<IEnumerable<int>> ParseInput(string input)
    {
        return InputRegex.Matches(input)
            .Select(match => match.Groups["calories"].Captures.Select(capture => int.Parse(capture.Value)));
    }

    public static int FindHighestCalories(IEnumerable<IEnumerable<int>> elfSnacks)
    {
        return elfSnacks.Select(snackCalories => snackCalories.Sum()).Max();
    }

    public static int SumTop3HighestCalories(IEnumerable<IEnumerable<int>> elfSnacks)
    {
        return elfSnacks
            .Select(snackCalories => snackCalories.Sum())
            .OrderByDescending(totalCalories => totalCalories)
            .Take(3)
            .Sum();
    }
    
    public string GetFirstAnswer()
    {
        var parsedInput = ParseInput(File.ReadAllText("Solutions/Day 01/input.txt"));
        
        return FindHighestCalories(parsedInput).ToString();
    }

    public string GetSecondAnswer()
    {
        var parsedInput = ParseInput(File.ReadAllText("Solutions/Day 01/input.txt"));
        
        return SumTop3HighestCalories(parsedInput).ToString();
    }
}