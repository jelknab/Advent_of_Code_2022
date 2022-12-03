﻿namespace Advent_of_Code_2022.Solutions.Day_03;

public class Solution: ISolution
{
    public static IEnumerable<string> ParseInput(string input)
    {
        return input
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(backpack => !string.IsNullOrEmpty(backpack));
    }

    public static (string compartmentA, string ComportmentB) GetBackpackCompartments(string backpack)
    {
        return (compartmentA: backpack[..(backpack.Length / 2)], ComportmentB: backpack[(backpack.Length / 2)..]);
    }

    public static IEnumerable<char> FindItemsInBothCompartments(string compartmentA, string comportmentB)
    {
        return compartmentA.Intersect(comportmentB);
    }

    public static IEnumerable<char> FindItemsInAllBackpacks(IEnumerable<string> backpacks)
    {
        return backpacks.Select(backpack => backpack.ToCharArray()).Aggregate((a, b) => a.Intersect(b).ToArray());
    }

    public static int GetItemsPriorityScore(IEnumerable<char> items)
    {
        return items
            .Select(item => item >= 'a' ? item - 'a' + 1 : item - 'A' + 27)
            .Sum();
    }

    public string GetFirstAnswer()
    {
        return ParseInput(File.ReadAllText("Solutions/Day 03/input.txt"))
            .Select(GetBackpackCompartments)
            .Select(compartments => FindItemsInBothCompartments(compartments.compartmentA, compartments.ComportmentB))
            .Select(GetItemsPriorityScore)
            .Sum()
            .ToString();
    }

    public string GetSecondAnswer()
    {
        return ParseInput(File.ReadAllText("Solutions/Day 03/input.txt"))
            .Batch(3)
            .Select(FindItemsInAllBackpacks)
            .Select(GetItemsPriorityScore)
            .Sum()
            .ToString();
    }
}