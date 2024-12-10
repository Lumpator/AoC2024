using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day10 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid = null!;
    private readonly List<(int row, int col)> _zeroPositions;


    public Day10()
    {
        _lines = File.ReadAllLines("Inputs/inputDay10.txt");
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
        _zeroPositions = new List<(int row, int col)>();
        for (int row = 0; row < _grid.Length; row++)
        {
            for (int col = 0; col < _grid[row].Length; col++)
            {
                if (_grid[row][col] == '0')
                {
                    _zeroPositions.Add((row, col));
                }
            }
        }

    }

    public void SolvePart1()
    {
        int part1Result = 0;

        HashSet<(int row, int col)> reachableTrailheads = new HashSet<(int row, int col)>();

        foreach (var (zeroRow, zeroCol) in _zeroPositions)
        {
            reachableTrailheads.Clear();

            var pathsToVisit = new List<(int row, int col, int value)>
            {
                (zeroRow, zeroCol, 0)
            };

            while (pathsToVisit.Count > 0)
            {
                var (currentRow, currentCol, currentValue) = pathsToVisit[0];
                pathsToVisit.RemoveAt(0);
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    var nextLocation = FindNextLocation((currentRow, currentCol), direction);
                    if (CheckIfLocationIsInGridBoundary(nextLocation) && currentValue == 8 && char.GetNumericValue(_grid[nextLocation.Item1][nextLocation.Item2]) == 9)
                    {
                        reachableTrailheads.Add((nextLocation.Item1, nextLocation.Item2));
                    }
                    if (CheckIfLocationIsInGridBoundary(nextLocation) && char.GetNumericValue(_grid[nextLocation.Item1][nextLocation.Item2]) == currentValue + 1)
                    {
                        pathsToVisit.Add((nextLocation.Item1, nextLocation.Item2, currentValue + 1));
                    }
                }
            }
            part1Result += reachableTrailheads.Count;



        }


        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        foreach (var (zeroRow, zeroCol) in _zeroPositions)
        {

            var pathsToVisit = new List<(int row, int col, int value)>
            {
                (zeroRow, zeroCol, 0)
            };

            while (pathsToVisit.Count > 0)
            {
                var (currentRow, currentCol, currentValue) = pathsToVisit[0];
                pathsToVisit.RemoveAt(0);
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    var nextLocation = FindNextLocation((currentRow, currentCol), direction);
                    if (CheckIfLocationIsInGridBoundary(nextLocation) && currentValue == 8 && char.GetNumericValue(_grid[nextLocation.Item1][nextLocation.Item2]) == 9)
                    {
                        part2Result++;
                    }
                    if (CheckIfLocationIsInGridBoundary(nextLocation) && char.GetNumericValue(_grid[nextLocation.Item1][nextLocation.Item2]) == currentValue + 1)
                    {
                        pathsToVisit.Add((nextLocation.Item1, nextLocation.Item2, currentValue + 1));
                    }
                }
            }

        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }



    private bool CheckIfLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }

    private (int, int) FindNextLocation((int, int) currentLocation, Direction direction) => direction switch
    {
        Direction.Up => (currentLocation.Item1 - 1, currentLocation.Item2),
        Direction.Down => (currentLocation.Item1 + 1, currentLocation.Item2),
        Direction.Left => (currentLocation.Item1, currentLocation.Item2 - 1),
        Direction.Right => (currentLocation.Item1, currentLocation.Item2 + 1),
        _ => throw new ArgumentOutOfRangeException()
    };

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


}