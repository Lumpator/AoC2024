using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day12 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    private readonly List<List<(int, int)>> _regions = new List<List<(int, int)>>();
    public Day12()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
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
            //Console.WriteLine($"Area size: {region.Count}");
            //Console.WriteLine($"Perimeter: {CalculateRegionPerimeter(region)}");
            part1Result += region.Count * CalculateRegionPerimeter(region);
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

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        int CalculateNumberOfRegionSides(List<(int, int)> region)
        {
            var sideCount = 0;
            foreach (var location in region)
            {
                HashSet<(int, int)> sides = new HashSet<(int, int)>();
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    var nextLocation = FindNextLocation(location, direction);
                    if (!region.Contains(nextLocation))
                    {
                        sides.Add(nextLocation);
                    }
                }
                sideCount += sides.Count();
            }
            return sideCount;
        }





        foreach (var region in _regions)
        {
            //Console.WriteLine($"Area size: {region.Count}");
            //Console.WriteLine($"Sides: {CalculateNumberOfRegionSides(region)}");
            var regionSides = CalculateNumberOfRegionSides(region);
            Console.WriteLine($"Region sides: {regionSides}");
            part2Result += region.Count * regionSides;

        }
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private (int, int) FindNextLocation((int, int) currentLocation, Direction direction) => direction switch
    {
        Direction.Up => (currentLocation.Item1 - 1, currentLocation.Item2),
        Direction.Down => (currentLocation.Item1 + 1, currentLocation.Item2),
        Direction.Left => (currentLocation.Item1, currentLocation.Item2 - 1),
        Direction.Right => (currentLocation.Item1, currentLocation.Item2 + 1),
        _ => throw new ArgumentOutOfRangeException()
    };

    private bool CheckIfLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void CheckNeighbours(int row, int col, List<(int, int)> temp, char regionChar)

    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var nextLocation = FindNextLocation((row, col), direction);
            if (CheckIfLocationIsInGridBoundary(nextLocation) && _grid[nextLocation.Item1][nextLocation.Item2] == regionChar && !temp.Contains(nextLocation))
            {
                temp.Add(nextLocation);
                CheckNeighbours(nextLocation.Item1, nextLocation.Item2, temp, regionChar);
            }
        }
    }


}