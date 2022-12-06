using Advent_of_Code_2022;

namespace Advent_of_Code_2022_tests;

public class UtilsTests
{
    [Fact]
    public void RollingWindowWorks()
    {
        var testData = "ABCDEFGHIJK".ToCharArray();
        var windows = testData.RollingWindow(3).Select(chars => new string(chars.ToArray())).ToArray();
        
        Assert.Equal("ABC", windows[0]);
        Assert.Equal("BCD", windows[1]);
        Assert.Equal("CDE", windows[2]);
    }
}