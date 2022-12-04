using System.Text.RegularExpressions;

namespace Advent_of_Code_2022.Solutions.Day_04;

public class Solution: ISolution
{
    private static readonly Regex InputRegex = new(@"(\d+)", RegexOptions.Compiled);
    
    public static IEnumerable<(int[] elfOne, int[] elfTwo)> ParseInput(string input)
    {
        return input
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(pairString => !string.IsNullOrEmpty(pairString))
            .Select(pairString =>
            {
                return InputRegex.Matches(pairString).Select(match => match.Value).Select(int.Parse).ToArray();
            })
            .Select(sectionIds =>
            {
                return (elfOne: new[] { sectionIds[0], sectionIds[1] },
                        elfTwo: new[] { sectionIds[2], sectionIds[3] });
            });
    }

    public static bool PairContainsOther(int[] elfOne, int[] elfTwo)
    {
        if (elfOne[0] <= elfTwo[0] && elfOne[1] >= elfTwo[1])
        {
            return true;
        }
        
        return elfTwo[0] <= elfOne[0] && elfTwo[1] >= elfOne[1];
    }

    public static bool PairHasOverlap(int[] elfOne, int[] elfTwo)
    {
        if (elfOne[0] >= elfTwo[0] && elfOne[0] <= elfTwo[1])
        {
            return true;
        }
        
        if (elfOne[1] >= elfTwo[0] && elfOne[1] <= elfTwo[1])
        {
            return true;
        }
        
        if (elfTwo[0] >= elfOne[0] && elfTwo[0] <= elfOne[1])
        {
            return true;
        }
        
        return elfTwo[1] >= elfOne[0] && elfTwo[1] <= elfOne[1];
    }
    
    public string GetFirstAnswer()
    {
        return ParseInput(File.ReadAllText("Solutions/Day 04/input.txt"))
            .Select(pair => PairContainsOther(pair.elfOne, pair.elfTwo))
            .Count(contains => contains)
            .ToString();
    }

    public string GetSecondAnswer()
    {
        return ParseInput(File.ReadAllText("Solutions/Day 04/input.txt"))
            .Select(pair => PairHasOverlap(pair.elfOne, pair.elfTwo))
            .Count(contains => contains)
            .ToString();
    }
}