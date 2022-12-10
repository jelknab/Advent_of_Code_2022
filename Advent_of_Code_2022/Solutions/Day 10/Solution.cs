namespace Advent_of_Code_2022.Solutions.Day_10;

public class Solution: ISolution
{
    public static IEnumerable<string> ParseInput(string input)
    {
        return input
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(line => !string.IsNullOrEmpty(line));
    }

    public static IEnumerable<int> RunCommand(IEnumerable<string> commands)
    {
        var x = 1;
        foreach (var args in commands.Select(line => line.Split(' ')))
        {
            switch (args[0])
            {
                case "noop":
                    yield return x;
                    break;
                case "addx":
                    yield return x;
                    yield return x;
                    x += int.Parse(args[1]);
                    break;
            }
        }
    }

    private static string PrintScreen(IEnumerable<bool> pixels, int screenWidth)
    {
        return pixels
            .Batch(screenWidth)
            .Aggregate("", (current, pixelRow) => current + (new string(pixelRow.Select(on => on ? '#' : ' ').ToArray()) + '\n'));
    }

    public string GetFirstAnswer()
    {
        var indices = new[] { 20, 60, 100, 140, 180, 220 };

        return RunCommand(ParseInput(File.ReadAllText("Solutions/Day 10/input.txt")))
            .Select((x, index) => (x, index))
            .Where(xAndI => indices.Contains(xAndI.index + 1))
            .Select(xAndI => (xAndI.index + 1) * xAndI.x)
            .Sum()
            .ToString();
    }

    public string GetSecondAnswer()
    {
        var pixels = new bool[6 * 40];

        var pixelPosition = RunCommand(ParseInput(File.ReadAllText("Solutions/Day 10/input.txt"))).ToArray();

        for (var pixelIndex = 0; pixelIndex < pixelPosition.Length; pixelIndex++)
        {
            var instruction = pixelPosition[pixelIndex];
            for (var spriteIndex = instruction - 1; spriteIndex <= instruction + 1; spriteIndex++)
            {
                if (spriteIndex == pixelIndex % 40)
                {
                    pixels[pixelIndex] = true;
                }
            }
        }

        return PrintScreen(pixels, 40);
    }
}