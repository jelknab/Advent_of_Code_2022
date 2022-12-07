namespace Advent_of_Code_2022.Solutions.Day_07;

public class AocFile
{
    public long Size { get; init; }
}

public class AocDirectory
{
    public AocDirectory? Parent { get; init; }
    public Dictionary<string, AocFile> Files { get; } = new();
    public Dictionary<string, AocDirectory> Directories { get; } = new();

    public long Size() => Files.Values.Select(file => file.Size).Sum() + Directories.Values.Select(dir => dir.Size()).Sum();

    public IEnumerable<AocDirectory> DirectoriesUnderThreshold(long size)
    {
        if (Size() < size)
        {
            yield return this;
        }

        foreach (var aocDirectory in Directories.Values.SelectMany(subDir => subDir.DirectoriesUnderThreshold(size)))
        {
            yield return aocDirectory;
        }
    }

    public IEnumerable<AocDirectory> AllDirectories()
    {
        yield return this;

        foreach (var aocDirectory in Directories.Values.SelectMany(subDir => subDir.AllDirectories()))
        {
            yield return aocDirectory;
        }
    }
}

public class AocTerminal
{
    public readonly AocDirectory Root = new();
    private AocDirectory ActiveDirectory { get; set; }

    public AocTerminal()
    {
        ActiveDirectory = Root;
    }

    public bool RunCommand(string[] args, IEnumerator<string> lineEnumerator)
    {
        switch (args[1])
        {
            case "cd":
                if (args[2].Equals(".."))
                {
                    ActiveDirectory = ActiveDirectory.Parent!;
                    return lineEnumerator.MoveNext();
                }
                
                ActiveDirectory = ActiveDirectory.Directories[args[2]];
                
                return lineEnumerator.MoveNext();
            case "ls":
                var contents = ReadLinesTillNextCommand(lineEnumerator);
                foreach (var content in contents)
                {
                    var splitContent = content.Split(' ');
                            
                    if (splitContent[0].Equals("dir"))
                    {
                        ActiveDirectory.Directories.Add(splitContent[1], new AocDirectory {Parent = ActiveDirectory});
                        continue;
                    }
                    
                    ActiveDirectory.Files.Add(splitContent[1], new AocFile {Size = long.Parse(splitContent[0])});
                }

                return true;
            default:
                return false;
        }
    }

    private static IEnumerable<string> ReadLinesTillNextCommand(IEnumerator<string> lineEnumerator)
    {
        while (lineEnumerator.MoveNext() && !lineEnumerator.Current.StartsWith('$'))
        {
            yield return lineEnumerator.Current;
        }
    }
    
}

public class Solution: ISolution
{
    
    public static IEnumerable<string> ReadTerminalLines(string input)
    {
        return input
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Where(line => !string.IsNullOrEmpty(line));
    }

    public static AocDirectory MapFileSystem(IEnumerable<string> lines)
    {
        // Skip "CD /"
        using var lineEnumerator = lines.Skip(1).GetEnumerator();
        {
            var terminal = new AocTerminal();
            lineEnumerator.MoveNext();

            string[] lineParts;
            do
            {
                if (string.IsNullOrEmpty(lineEnumerator.Current))
                {
                    break;
                }
                lineParts = lineEnumerator.Current.Split(' ');
            } while (terminal.RunCommand(lineParts, lineEnumerator));

            return terminal.Root;
        }
    }

    public string GetFirstAnswer()
    {
        var terminalLines = ReadTerminalLines(File.ReadAllText("Solutions/Day 07/input.txt"));
        return MapFileSystem(terminalLines)
            .DirectoriesUnderThreshold(100000)
            .Select(dir => dir.Size())
            .Sum()
            .ToString();
    }

    public string GetSecondAnswer()
    {
        var terminalLines = ReadTerminalLines(File.ReadAllText("Solutions/Day 07/input.txt"));
        var rootDir = MapFileSystem(terminalLines);
        var totalSize = rootDir.Size();
        var remaining = 70000000L - totalSize;
        var needed = 30000000L - remaining;

        return rootDir
            .AllDirectories()
            .Select(dir => (dir, Size: dir.Size()))
            .Where(dir => dir.Size >= needed)
            .MinBy(dirAndSize => dirAndSize.Size)
            .Size
            .ToString();
    }
}