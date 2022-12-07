using System.Drawing;

namespace Advent_of_Code_2022_tests.Day07;
using Advent_of_Code_2022.Solutions.Day_07;

public class Day07Tests
{
    [Fact]
    void ExpectedPartOne()
    {
        var terminalLines = Solution.ReadTerminalLines(File.ReadAllText("Day07/test.input.txt"));
        var rootDir = Solution.MapFileSystem(terminalLines);
        var directoriesUnderThresholdSize = rootDir.DirectoriesUnderThreshold(100000).Select(dir => dir.Size()).Sum();
        
        Assert.Equal(95437, directoriesUnderThresholdSize);
    }
    
    [Fact]
    void ExpectedPartTwo()
    {
        var terminalLines = Solution.ReadTerminalLines(File.ReadAllText("Day07/test.input.txt"));
        var rootDir = Solution.MapFileSystem(terminalLines);
        var totalSize = rootDir.Size();
        var remaining = 70000000L - totalSize;
        var needed = 30000000L - remaining;


        var smallestDeletableDir = rootDir
            .AllDirectories()
            .Select(dir => (dir, Size: dir.Size()))
            .Where(dir => dir.Size >= needed)
            .MinBy(dirAndSize => dirAndSize.Size)
            .Size;

        Assert.Equal(24933642, smallestDeletableDir);
    }
}