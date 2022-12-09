namespace Advent_of_Code_2022_tests.Day08;
using Advent_of_Code_2022.Solutions.Day_08;

public class Day08Test
{
    [Fact]
    public void ProblemOneExpectedOutput()
    {
        var grid = Solution.ParseInput(File.ReadAllText("Day08/test.input.txt"));
        var treeCount = Solution.CountTreesVisibleFromOutside(grid);
        
        Assert.Equal(21, treeCount);
    }

    [Fact]
    public void ProblemTwoExpectedOutput()
    {
        var grid = Solution.ParseInput(File.ReadAllText("Day08/test.input.txt"));
        var highestScenic = grid.GetEnumerable()
            .Select(treeAndLocation => Solution.TreeViewDistance(treeAndLocation.x, treeAndLocation.y, grid))
            .Select(viewDistance => viewDistance.left * viewDistance.right * viewDistance.down * viewDistance.up)
            .Max();
        
        Assert.Equal(8, highestScenic);
    }
}