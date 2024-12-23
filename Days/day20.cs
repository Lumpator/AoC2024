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

        var cheats = FindAllCheats(_calculatedLocations, 2);
        part1Result = cheats.Where(x => x.Key >= 100).Select(x => x.Value).Sum();


        Console.WriteLine($"Part 1 solution is: {part1Result}");

    }

    public void SolvePart2()
    {
        int part2Result = 0;
        var cheats2 = FindAllCheats(_calculatedLocations, 20);

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
            long picoSeconds;
            (int, int) currentLocation;
            (currentLocation, picoSeconds) = pathsToVisit[0];
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
                        if (calculatedLocations[index].Item2 > picoSeconds + 1)
                        {
                            calculatedLocations[index] = (calculatedLocations[index].Item1, picoSeconds + 1);
                            pathsToVisit.Add((nextLocation, picoSeconds + 1));
                        }
                    }
                }
                else
                {
                    calculatedLocations.Add((nextLocation, picoSeconds + 1));
                    pathsToVisit.Add((nextLocation, picoSeconds + 1));
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

    private Dictionary<long, int> FindAllCheats(List<((int, int), long)> calculatedLocations, int cheatDuration)
    {
        Dictionary<long, int> cheats = new();
        var endTimeWithoutCheat = calculatedLocations.Find(x => x.Item1 == _end).Item2;
        foreach (var location in calculatedLocations)
        {
            var currentLocation = location.Item1;
            var currentTime = location.Item2;

            var possibleCheats = calculatedLocations
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
                AddToCheatsDict(ref cheats, possibleCheat.Item2 - currentTime - (Math.Abs(currentLocation.Item1 - possibleCheat.Item1.Item1) + Math.Abs(currentLocation.Item2 - possibleCheat.Item1.Item2)));
            }
        }
        return cheats;

    }


}


