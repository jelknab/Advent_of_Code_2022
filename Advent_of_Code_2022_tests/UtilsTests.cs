using Advent_of_Code_2022;

namespace Advent_of_Code_2022_tests;

public class UtilsTests
{
    [Fact]
    public void RollingWindowWorks()
    {
        var testData = "ABCDE".ToCharArray();
        var windows = testData.RollingWindow(3).Select(chars => new string(chars.ToArray())).ToArray();
        
        Assert.Equal("ABC", windows[0]);
        Assert.Equal("BCD", windows[1]);
        Assert.Equal("CDE", windows[2]);
        Assert.Equal(3, windows.Length);
        
        
        var smallTestData = "AB".ToCharArray();
        var smallTestDataWindows = smallTestData.RollingWindow(3).Select(chars => new string(chars.ToArray())).ToArray();
        Assert.Equal("AB", smallTestDataWindows[0]);
        Assert.Single(smallTestDataWindows);
    }
}