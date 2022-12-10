namespace Advent_of_Code_2022.Solutions.Day_09;

public class Solution: ISolution
{
    public static (int x, int y)[] BorderingCoordinates = {
        (1, 1), (0, 1), (-1, 1),
        (1, 0), (0, 0), (-1, 0),
        (1, -1), (0, -1), (-1, -1)
    };
    
    public enum Direction
    {
        Right = 'R', Left = 'L', Up = 'U', Down = 'D',
    }

    public static IEnumerable<(Direction direction, int amount)> ParseInput(string input)
    {
        return input
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(line => line.Split(' '))
            .Select(split => (direction: (Direction)split[0][0], amount: int.Parse(split[1])));
    }

    public static IEnumerable<(int x, int y)> HeadPositions(IEnumerable<(Direction direction, int amount)> commands)
    {
        var position = (x: 0, y: 0);

        foreach (var command in commands)
        {
            for (var step = 0; step < command.amount; step++)
            {
                switch (command.direction)
                {
                    case Direction.Up: position = (position.x, position.y + 1); break;
                    case Direction.Down: position = (position.x, position.y - 1); break;
                    case Direction.Left: position = (position.x - 1, position.y); break;
                    case Direction.Right: position = (position.x + 1, position.y); break;
                }
                
                yield return position;
            }
        }
    }

    public static bool IsTouching(int x, int x1, int y, int y1)
    {
        return BorderingCoordinates.Contains((x - x1, y - y1));
    }

    public static IEnumerable<(int x, int y)> TrackTail(IEnumerable<(int x, int y)> headPositions)
    {
        var headPositionArray = headPositions.ToArray();
        var tailPosition = (x: 0, y: 0);

        yield return tailPosition;

        foreach (var headPosition in headPositionArray)
        {
            if (IsTouching(tailPosition.x, headPosition.x, tailPosition.y, headPosition.y)) continue;
            
            var signedDiff = (
                x: Math.Sign(tailPosition.x - headPosition.x), 
                y: Math.Sign(tailPosition.y - headPosition.y));
                
            tailPosition = (tailPosition.x - signedDiff.x, tailPosition.y - signedDiff.y);
            yield return tailPosition;
        }
    }

    public string GetFirstAnswer()
    {
        var commands = ParseInput(File.ReadAllText("Solutions/Day 09/input.txt")).ToArray();
        return TrackTail(HeadPositions(commands)).Distinct().Count().ToString();
    }

    public string GetSecondAnswer()
    {
        var commands = ParseInput(File.ReadAllText("Solutions/Day 09/input.txt")).ToArray();
        var nthPosition = TrackTail(HeadPositions(commands));

        for (var i = 0; i < 8; i++)
        {
            nthPosition = TrackTail(nthPosition);
        }

        return nthPosition.Distinct().Count().ToString();
    }
}