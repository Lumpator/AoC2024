using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

// 595 => too high 
namespace AoC2024.Days;

public class Day16 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    public Day16()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay16.txt");
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

        List<((int, int), long, int, int)> calculatedLocations = new(){
            (start, 0, 0, 0)
        };
        List<((int, int), long, Direction, int, int)> pathsToVisit = new()
        {
            (start, 0, Direction.Right, 0, 0),
        };


        while (pathsToVisit.Count > 0)
        {
            long score;
            (int, int) currentLocation;
            Direction currentDirection;
            int stepsCount;
            int turnCount;
            (currentLocation, score, currentDirection, stepsCount, turnCount) = pathsToVisit[0];

            pathsToVisit.RemoveAt(0);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                long scoreToAdd = 0;
                int turnToAdd = 0;
                if (currentDirection != direction)
                {
                    scoreToAdd = 1001;
                    turnToAdd = 1;
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
                            calculatedLocations[index] = (calculatedLocations[index].Item1, score + scoreToAdd, stepsCount + 1, turnCount + turnToAdd);
                            pathsToVisit.Add((nextLocation, score + scoreToAdd, direction, stepsCount + 1, turnCount + turnToAdd));
                        }
                    }
                }
                else
                {
                    calculatedLocations.Add((nextLocation, score + scoreToAdd, stepsCount + 1, turnCount + turnToAdd));
                    pathsToVisit.Add((nextLocation, score + scoreToAdd, direction, stepsCount + 1, turnCount + turnToAdd));
                }
            }
        }

        // print score of all end location
        var endLocation = calculatedLocations.Find(x => x.Item1 == end);
        part1Result = endLocation.Item2;

        Console.WriteLine($"Part 1 solution is: {part1Result}");

        //check all neighbours of end location and retrive one with the lowest score
        HashSet<(int, int)> pathCount = new();
        List<((int, int), long, int, int)> locationsToVisit = new()
        {
            endLocation
        };
        int totalTurns = endLocation.Item4;
        while (locationsToVisit.Count > 0)
        {
            var currentLocation = locationsToVisit[0];
            locationsToVisit.RemoveAt(0);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var nextLocation = FindNextLocation(currentLocation.Item1, direction);
                if (_grid[nextLocation.Item1][nextLocation.Item2] == '#')
                {
                    continue;
                }
                var tempLocation = calculatedLocations.Find(x => x.Item1 == nextLocation);
                if (tempLocation.Item3 == currentLocation.Item3 - 1)
                {
                    /*                  Console.Write(currentLocation.Item2);
                                     Console.Write(" | ");
                                     Console.Write(currentLocation.Item2 - tempLocation.Item2);
                                     Console.Write(" | ");
                                     Console.WriteLine(currentLocation.Item4 - tempLocation.Item4); */
                    if (currentLocation.Item4 - tempLocation.Item4 == 1 && currentLocation.Item2 - tempLocation.Item2 == 1001)
                    {
                        locationsToVisit.Add(tempLocation);
                        pathCount.Add(tempLocation.Item1);
                    }
                    if (tempLocation.Item4 - currentLocation.Item4 == 1 && tempLocation.Item2 - currentLocation.Item2 == 999 && tempLocation.Item4 < totalTurns)
                    {
                        locationsToVisit.Add(tempLocation);
                        pathCount.Add(tempLocation.Item1);
                    }
                    if (currentLocation.Item4 - tempLocation.Item4 == 0 && currentLocation.Item2 - tempLocation.Item2 == 1)
                    {
                        locationsToVisit.Add(tempLocation);
                        pathCount.Add(tempLocation.Item1);
                    }
                }
            }
        }


        Console.WriteLine($"Path: {pathCount.Count + 1}");

    }
    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}