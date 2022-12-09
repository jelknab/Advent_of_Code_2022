using Advent_of_Code_2022.Datastructures;

namespace Advent_of_Code_2022.Solutions.Day_08;

public class Solution: ISolution
{
    public static Grid<int> ParseInput(string input)
    {
        var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToArray();
        var lineSize = lines[0].Length;

        return new Grid<int>(lineSize, lines.SelectMany(line => line.ToCharArray()).Select(c => c - '0'));
    }

    public static int CountTreesVisibleFromOutside(Grid<int> treeGrid)
    {
        return treeGrid.GetEnumerable().Count(treeAndLocation =>
        {
            for (var x = treeAndLocation.x - 1;; x--)
            {
                if (x == -1) return true;
                if (treeGrid.GetValue(x, treeAndLocation.y) >= treeAndLocation.value) break;
            }

            for (var x = treeAndLocation.x + 1;; x++)
            {
                if (x == treeGrid.GetDimensions().width) return true;
                if (treeGrid.GetValue(x, treeAndLocation.y) >= treeAndLocation.value) break;
            }

            for (var y = treeAndLocation.y - 1;; y--)
            {
                if (y == -1) return true;
                if (treeGrid.GetValue(treeAndLocation.x, y) >= treeAndLocation.value) break;
            }

            for (var y = treeAndLocation.y + 1;; y++)
            {
                if (y == treeGrid.GetDimensions().height) return true;
                if (treeGrid.GetValue(treeAndLocation.x, y) >= treeAndLocation.value) break;
            }

            return false;
        });
    }

    public static (int left, int right, int up, int down) TreeViewDistance(int treeX, int treeY, Grid<int> grid)
    {
        var tree = grid.GetValue(treeX, treeY);

        var left = 0;
        var right = 0;
        var up = 0;
        var down = 0;

        for (var x = treeX - 1; x >= 0; x--)
        {
            left++;
            if (grid.GetValue(x, treeY) >= tree) break;
        }

        for (var x = treeX + 1; x < grid.GetDimensions().width; x++)
        {
            right++;
            if (grid.GetValue(x, treeY) >= tree) break;
        }

        for (var y = treeY - 1; y >= 0; y--)
        {
            down++;
            if (grid.GetValue(treeX, y) >= tree) break;
        }

        for (var y = treeY + 1; y < grid.GetDimensions().height; y++)
        {
            up++;
            if (grid.GetValue(treeX, y) >= tree) break;
        }

        return (left, right, up, down);
    }

    public string GetFirstAnswer()
    {
        var grid = ParseInput(File.ReadAllText("Solutions/Day 08/input.txt"));
        return CountTreesVisibleFromOutside(grid).ToString();
    }

    public string GetSecondAnswer()
    {
        var grid = ParseInput(File.ReadAllText("Solutions/Day 08/input.txt"));
        return grid.GetEnumerable()
            .Select(treeAndLocation => TreeViewDistance(treeAndLocation.x, treeAndLocation.y, grid))
            .Select(viewDistance => viewDistance.left * viewDistance.right * viewDistance.down * viewDistance.up)
            .Max()
            .ToString();
    }
}