namespace Advent_of_Code_2022.Solutions.Day_06;

public class Solution: ISolution
{
    public static int FindMarkerIndex(string input, int markerLength)
    {
        return input.ToCharArray()
            .RollingWindow(markerLength)
            .Select((window, index) => (window, index))
            .First((windowAndIndex) => windowAndIndex.window.Distinct().Count() == markerLength)
            .index + markerLength;
    }
    
    public string GetFirstAnswer()
    {
        return FindMarkerIndex(File.ReadAllText("Solutions/Day 06/input.txt"), 4).ToString();
    }

    public string GetSecondAnswer()
    {
        return FindMarkerIndex(File.ReadAllText("Solutions/Day 06/input.txt"), 14).ToString();
    }
}