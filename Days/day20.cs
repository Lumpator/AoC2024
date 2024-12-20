using AoC2024.Interfaces;
using System.Linq;
using static AoC2024.Utils.Utilities;

namespace AoC2024.Days;

public class Day20 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    private readonly List<((int, int), long)> _calculatedLocations;
    private readonly (int, int) _start;
    private readonly (int, int) _end;
    public Day20()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay20.txt");
        _lines = File.ReadAllLines(filePath);
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
        _start = FindLocation(_grid, 'S');
        _end = FindLocation(_grid, 'E');

        _calculatedLocations = CalculateAllLocations(_grid, _start);
    }

    public void SolvePart1()
    {
        int part1Result = 0;

        var cheats = FindAllCheats(_grid, _calculatedLocations, _calculatedLocations.Find(x => x.Item1 == _end).Item2, 2);
        part1Result = cheats.Where(x => x.Key >= 100).Select(x => x.Value).Sum();


        Console.WriteLine($"Part 1 solution is: {part1Result}");

    }

    public void SolvePart2()
    {
        int part2Result = 0;

        var cheats2 = FindAllCheats(_grid, _calculatedLocations, _calculatedLocations.Find(x => x.Item1 == _end).Item2, 20);
        cheats2 = cheats2
            .Where(x => x.Key >= 50 && x.Key <= _calculatedLocations.Find(x => x.Item1 == _end).Item2)
            .ToDictionary(x => x.Key, x => x.Value);

        part2Result = cheats2.Where(x => x.Key >= 100).Select(x => x.Value).Sum();
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }
    private List<((int, int), long)> CalculateAllLocations(char[][] grid, (int, int) start)
    {
        List<((int, int), long)> calculatedLocations = new()
        {
            (start, 0)
        };

        List<((int, int), long)> pathsToVisit = new()
        {
            (start, 0),
        };

        while (pathsToVisit.Count > 0)
        {
            long picoseconds;
            (int, int) currentLocation;
            (currentLocation, picoseconds) = pathsToVisit[0];

            pathsToVisit.RemoveAt(0);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
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
                        if (calculatedLocations[index].Item2 > picoseconds + 1)
                        {
                            calculatedLocations[index] = (calculatedLocations[index].Item1, picoseconds + 1);
                            pathsToVisit.Add((nextLocation, picoseconds + 1));
                        }
                    }
                }
                else
                {
                    calculatedLocations.Add((nextLocation, picoseconds + 1));
                    pathsToVisit.Add((nextLocation, picoseconds + 1));
                }
            }
        }
        return calculatedLocations;
    }

    private static void AddToCheatsDict(ref Dictionary<long, int> cheats, long cheatTime)
    {
        if (cheats.ContainsKey(cheatTime))
        {
            cheats[cheatTime]++;
        }
        else
        {
            cheats[cheatTime] = 1;
        }
    }

    private Dictionary<long, int> FindAllCheats(char[][] grid, List<((int, int), long)> calculatedLocations, long endTimeWithoutCheat, int cheatDuration)
    {
        Dictionary<long, int> cheats = new();
        var racePath = calculatedLocations.Where(x => x.Item2 <= endTimeWithoutCheat).ToList();

        foreach (var location in racePath)
        {
            var currentLocation = location.Item1;
            var currentTime = location.Item2;

            var possibleCheats = racePath
                .Where(x => x.Item2 > currentTime && x.Item2 <= endTimeWithoutCheat)
                .Where(possibleCheat =>
                {
                    var cheatLocation = possibleCheat.Item1;
                    int distance = Math.Abs(currentLocation.Item1 - cheatLocation.Item1) + Math.Abs(currentLocation.Item2 - cheatLocation.Item2);
                    return distance <= cheatDuration;
                })
                .ToList();

            foreach (var possibleCheat in possibleCheats)
            {
                var cheatLocation = possibleCheat.Item1;
                var original = calculatedLocations.Find(x => x.Item1 == cheatLocation);
                var originalLocation = original.Item1;
                long distance = Math.Abs(currentLocation.Item1 - cheatLocation.Item1) + Math.Abs(currentLocation.Item2 - cheatLocation.Item2);
                long cheatTime = original.Item2 - currentTime - distance;

                AddToCheatsDict(ref cheats, cheatTime);
            }
        }
        return cheats;

    }


}


