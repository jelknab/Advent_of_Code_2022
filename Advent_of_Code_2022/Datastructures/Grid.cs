namespace Advent_of_Code_2022.Datastructures;

public class Grid<T>
{
    public T[][] Value { get; set; }
    
    public Grid(int width, int height)
    {
        Value = Enumerable.Range(0, height).Select(row => new T[width]).ToArray();
    }

    public Grid(int width, IEnumerable<T> values)
    {
        Value = values.Batch(width).Select(batch => batch.ToArray()).ToArray();
    }

    public T GetValue(int x, int y)
    {
        return Value[y][x];
    }

    public (int width, int height) GetDimensions()
    {
        return (Value[0].Length, Value.Length);
    }

    public IEnumerable<(T value, int x, int y)> GetEnumerable()
    {
        foreach (var colAndIndex in Value.Select((column, columnIndex) => (column, columnIndex)))
        {
            foreach (var rowAndIndex in colAndIndex.column.Select((value, rowIndex) => (value, rowIndex)))
            {
                yield return (rowAndIndex.value, x: rowAndIndex.rowIndex, y: colAndIndex.columnIndex);
            }
        }
    }
}