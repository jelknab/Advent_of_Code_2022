namespace Advent_of_Code_2022_tests.Day06;
using Advent_of_Code_2022.Solutions.Day_06;

public class Day06Tests
{
    private static readonly string[] TestData = new[]
    {
        "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
        "bvwbjplbgvbhsrlpgdmjqwftvncz",
        "nppdvjthqldpwncqszvftbrmjlhg",
        "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
        "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
    };

    [Fact]
    public void ProblemOneExpectedOutput()
    {
        var indices = TestData.Select(line => Solution.FindMarkerIndex(line, 4)).ToArray();
        
        Assert.Equal(7, indices[0]);
        Assert.Equal(5, indices[1]);
        Assert.Equal(6, indices[2]);
        Assert.Equal(10, indices[3]);
        Assert.Equal(11, indices[4]);
    }
    
    [Fact]
    public void ProblemTwoExpectedOutput()
    {
        var indices = TestData.Select(line => Solution.FindMarkerIndex(line, 14)).ToArray();
        
        Assert.Equal(19, indices[0]);
        Assert.Equal(23, indices[1]);
        Assert.Equal(23, indices[2]);
        Assert.Equal(29, indices[3]);
        Assert.Equal(26, indices[4]);
    }
}