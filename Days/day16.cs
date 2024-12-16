using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

// 110516 => too high 
namespace AoC2024.Days;

public class Day16 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    public Day16()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
        _lines = File.ReadAllLines(filePath);
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
    }

    public void SolvePart1()
    {
        long part1Result = 0;
        var start = FindLocation(_grid, 'S');
        var end = FindLocation(_grid, 'E');

        List<((int, int), long)> calculatedLocations = new(){
            (start, 0)
        };
        List<((int, int), long, Direction)> pathsToVisit = new()
        {
            (start, 0, Direction.Right),
        };


        while (pathsToVisit.Count > 0)
        {
            long score;
            (int, int) currentLocation;
            Direction currentDirection;
            (currentLocation, score, currentDirection) = pathsToVisit[0];

            pathsToVisit.RemoveAt(0);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                long scoreToAdd = 0;
                if (currentDirection != direction)
                {
                    scoreToAdd = 1001;
                }
                else
                {
                    scoreToAdd = 1;
                }
                var nextLocation = FindNextLocation(currentLocation, direction);

                if (_grid[nextLocation.Item1][nextLocation.Item2] == '#')
                {
                    continue;
                }

                if (calculatedLocations.Any(x => x.Item1 == nextLocation))
                {
                    var index = calculatedLocations.FindIndex(x => x.Item1 == nextLocation);
                    if (index != -1)
                    {
                        if (calculatedLocations[index].Item2 > score + scoreToAdd)
                        {
                            calculatedLocations[index] = (calculatedLocations[index].Item1, score + scoreToAdd);
                            pathsToVisit.Add((nextLocation, score + scoreToAdd, direction));
                        }
                    }
                }
                else
                {
                    calculatedLocations.Add((nextLocation, score + scoreToAdd));
                    pathsToVisit.Add((nextLocation, score + scoreToAdd, direction));
                }
            }
        }

        // print score of all end location
        var endLocation = calculatedLocations.Find(x => x.Item1 == end);
        part1Result = endLocation.Item2;

        Console.WriteLine($"Part 1 solution is: {part1Result}");

        //check all neighbours of end location and retrive one with the lowest score
        List<(int, int)> Path = new()
        {
            end
        };
        while (Path.Last() != start)
        {
            var currentLocation = Path.Last();
            ((int, int), long) toGo = ((-1, -1), 200000);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var nextLocation = FindNextLocation(currentLocation, direction);
                if (_grid[nextLocation.Item1][nextLocation.Item2] == '#')
                {
                    continue;
                }
                var tempLocation = calculatedLocations.Find(x => x.Item1 == nextLocation);
                if (toGo.Item2 > tempLocation.Item2)
                {
                    toGo = tempLocation;
                }
            }
            Path.Add(toGo.Item1);
        }

        Console.WriteLine($"Path: {Path.Count}");

    }
    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}