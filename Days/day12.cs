using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

namespace AoC2024.Days;

public class Day12 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    private readonly List<List<(int, int)>> _regions = new List<List<(int, int)>>();
    public Day12()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay12.txt");
        _lines = File.ReadAllLines(filePath);
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
        List<(int, int)> alreadyVisitedLocations = new List<(int, int)>();

        for (int row = 0; row < _grid.Length; row++)
        {
            for (int col = 0; col < _grid[row].Length; col++)
            {
                if (!alreadyVisitedLocations.Contains((row, col)))
                {
                    List<(int, int)> temp = new List<(int, int)>
                    {
                        { (row, col) }
                    };
                    var regionChar = _grid[row][col];
                    CheckNeighbours(row, col, temp, regionChar);
                    _regions.Add(temp);
                    alreadyVisitedLocations.AddRange(temp);
                }
            }
        }
    }
    public void SolvePart1()
    {
        int part1Result = 0;

        foreach (var region in _regions)
        {
            part1Result += region.Count * CalculateRegionPerimeter(region);
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        int CalculateNumberOfRegionSides(List<(int, int)> region)
        {
            var sideCount = 0;
            // side count is same as count of edges in my shape
            foreach (var location in region)
            {
                sideCount += FindOuterEdges(region, location);
                sideCount += FindInnerEdges(region, location);
            }
            return sideCount;
        }

        foreach (var region in _regions)
        {
            part2Result += region.Count * CalculateNumberOfRegionSides(region);
        }
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }
    int CalculateRegionPerimeter(List<(int, int)> region)
    {
        int perimeter = 0;
        foreach (var location in region)
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var nextLocation = FindNextLocation(location, direction);
                if (!region.Contains(nextLocation))
                {
                    perimeter++;
                }
            }
        }
        return perimeter;
    }
    private int FindOuterEdges(List<(int, int)> region, (int, int) location)
    {
        int horizontalNeighbours = 0;
        int verticalNeighbours = 0;
        int innerEdgesCount = 0;
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var nextLocation = FindNextLocation(location, direction);

            if (region.Contains(nextLocation))
            {
                if (direction == Direction.Left || direction == Direction.Right)
                {
                    horizontalNeighbours++;
                }
                else
                {
                    verticalNeighbours++;
                }
            }
        }

        innerEdgesCount += (horizontalNeighbours, verticalNeighbours) switch
        {
            (0, 0) => 4,
            (1, 1) => 1,
            (1, 0) => 2,
            (0, 1) => 2,
            _ => 0
        };
        return innerEdgesCount;
    }

    private int FindInnerEdges(List<(int, int)> region, (int, int) location)
    {
        int innerEdgesCount = 0;
        var diagonalChecks = new (Direction, Direction, (int, int))[]
                        {
                    (Direction.Up, Direction.Right, (location.Item1 - 1, location.Item2 + 1)),
                    (Direction.Right, Direction.Down, (location.Item1 + 1, location.Item2 + 1)),
                    (Direction.Down, Direction.Left, (location.Item1 + 1, location.Item2 - 1)),
                    (Direction.Left, Direction.Up, (location.Item1 - 1, location.Item2 - 1))
                        };

        foreach (var (direction1, direction2, diagonal) in diagonalChecks)
        {
            var nextLocation1 = FindNextLocation(location, direction1);
            var nextLocation2 = FindNextLocation(location, direction2);
            if (region.Contains(nextLocation1) && region.Contains(nextLocation2) && !region.Contains(diagonal))
            {
                innerEdgesCount++;
            }
        }
        return innerEdgesCount;
    }

    private void CheckNeighbours(int row, int col, List<(int, int)> temp, char regionChar)

    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var nextLocation = FindNextLocation((row, col), direction);
            if (CheckIfLocationIsInGridBoundary(nextLocation, _grid) && _grid[nextLocation.Item1][nextLocation.Item2] == regionChar && !temp.Contains(nextLocation))
            {
                temp.Add(nextLocation);
                CheckNeighbours(nextLocation.Item1, nextLocation.Item2, temp, regionChar);
            }
        }
    }

}