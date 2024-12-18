using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

namespace AoC2024.Days;

public class Day18 : IDay
{
    private readonly string[] _lines;

    public Day18()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay18.txt");
        _lines = File.ReadAllLines(filePath);

    }

    public void SolvePart1()
    {
        int part1Result = 0;
        int rows = 71;
        int cols = 71;
        char[][] grid = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            grid[i] = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                grid[i][j] = '.';
            }
        }
        for (int i = 0; i < 1024; i++)
        {
            string line = _lines[i];
            string[] parts = line.Split(',');
            int col = int.Parse(parts[0]);
            int row = int.Parse(parts[1]);
            grid[row][col] = '#';
        }


        var start = (0, 0);
        var end = (70, 70);


        List<((int, int), int)> visitedLocations = new();
        List<((int, int), int)> pathsToVisit = new()
           {
               (start, 0)
           };

        while (pathsToVisit.Count > 0)
        {
            int currentSteps;
            (int, int) currentLocation;
            (currentLocation, currentSteps) = pathsToVisit[0];
            pathsToVisit.RemoveAt(0);
            if (visitedLocations.Any(x => x.Item1 == currentLocation))
            {
                visitedLocations.Remove(visitedLocations.First(x => x.Item1 == currentLocation));
            }
            visitedLocations.Add((currentLocation, currentSteps));
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var nextLocation = FindNextLocation(currentLocation, direction);
                if (CheckIfLocationIsInGridBoundary(nextLocation, grid))
                {
                    if (grid[nextLocation.Item1][nextLocation.Item2] == '#')
                    {
                        continue;
                    }
                    if (visitedLocations.Any(x => x.Item1 == nextLocation))
                    {
                        if (visitedLocations.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                        {
                            if (pathsToVisit.Any(x => x.Item1 == nextLocation))
                            {
                                if (pathsToVisit.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                                {
                                    pathsToVisit.Remove(pathsToVisit.First(x => x.Item1 == nextLocation));
                                    pathsToVisit.Add((nextLocation, currentSteps + 1));
                                }
                            }
                            else
                            {
                                pathsToVisit.Add((nextLocation, currentSteps + 1));
                            }

                        }
                    }
                    else
                    {
                        if (pathsToVisit.Any(x => x.Item1 == nextLocation))
                        {
                            if (pathsToVisit.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                            {
                                pathsToVisit.Remove(pathsToVisit.First(x => x.Item1 == nextLocation));
                                pathsToVisit.Add((nextLocation, currentSteps + 1));
                            }
                        }
                        else
                        {
                            pathsToVisit.Add((nextLocation, currentSteps + 1));
                        }
                    }
                }
            }
        }
        Console.WriteLine(visitedLocations.First(x => x.Item1 == end).Item2);

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int rows = 71;
        int cols = 71;
        char[][] grid = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            grid[i] = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                grid[i][j] = '.';
            }
        }
        for (int i = 0; i < 1024; i++)
        {
            string line = _lines[i];
            string[] parts = line.Split(',');
            int col = int.Parse(parts[0]);
            int row = int.Parse(parts[1]);
            grid[row][col] = '#';
        }

        var start = (0, 0);
        var end = (70, 70);




        bool CanReachEnd(int i)
        {
            List<((int, int), int)> visitedLocations = new();
            List<((int, int), int)> pathsToVisit = new()
        {
            (start, 0)
        };
            string line = _lines[i];
            string[] parts = line.Split(',');
            int col = int.Parse(parts[0]);
            int row = int.Parse(parts[1]);
            grid[row][col] = '#';

            while (pathsToVisit.Count > 0)
            {
                if (pathsToVisit.Any(x => x.Item1 == end))
                {
                    return true;
                }
                //Console.WriteLine(pathsToVisit.Count);
                int currentSteps;
                (int, int) currentLocation;
                (currentLocation, currentSteps) = pathsToVisit[0];
                pathsToVisit.RemoveAt(0);
                if (visitedLocations.Any(x => x.Item1 == currentLocation))
                {
                    visitedLocations.Remove(visitedLocations.First(x => x.Item1 == currentLocation));
                }
                visitedLocations.Add((currentLocation, currentSteps));
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    var nextLocation = FindNextLocation(currentLocation, direction);
                    if (CheckIfLocationIsInGridBoundary(nextLocation, grid))
                    {
                        if (grid[nextLocation.Item1][nextLocation.Item2] == '#')
                        {
                            continue;
                        }
                        if (visitedLocations.Any(x => x.Item1 == nextLocation))
                        {
                            if (visitedLocations.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                            {
                                if (pathsToVisit.Any(x => x.Item1 == nextLocation))
                                {
                                    if (pathsToVisit.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                                    {
                                        pathsToVisit.Remove(pathsToVisit.First(x => x.Item1 == nextLocation));
                                        pathsToVisit.Add((nextLocation, currentSteps + 1));
                                    }
                                }
                                else
                                {
                                    pathsToVisit.Add((nextLocation, currentSteps + 1));
                                }

                            }
                        }
                        else
                        {
                            if (pathsToVisit.Any(x => x.Item1 == nextLocation))
                            {
                                if (pathsToVisit.First(x => x.Item1 == nextLocation).Item2 > currentSteps + 1)
                                {
                                    pathsToVisit.Remove(pathsToVisit.First(x => x.Item1 == nextLocation));
                                    pathsToVisit.Add((nextLocation, currentSteps + 1));
                                }
                            }
                            else
                            {
                                pathsToVisit.Add((nextLocation, currentSteps + 1));
                            }
                        }
                    }
                }
            }
            return false;
        }

        int byteIndex = 1024;
        while (CanReachEnd(byteIndex))
        {
            byteIndex++;
        }

        Console.WriteLine($"Part 2 solution is: {_lines[byteIndex]} ");
    }

}