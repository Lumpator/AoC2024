using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day6 : IDay
{
    private readonly string[] _lines;

    private (string, (int, int)) _startingInfo;

    private List<((int, int), string)> _alreadyVisitedLocations = new List<((int, int), string)>();

    private readonly char[][] _grid;
    public Day6()
    {
        _lines = File.ReadAllLines("Inputs/inputDay6.txt");
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }

        _startingInfo = FindStartingDirectionAndLocation(_grid);

    }

    public void SolvePart1()
    {
        int part1Result = 0;
        var currentDirection = _startingInfo.Item1;
        var currentLocation = _startingInfo.Item2;
        (int, int) nextLocation = (0, 0);

        int MoveUntilLeavingGrid()
        {
            int steps = 1;

            while (CheckIfNextLocationIsInGridBoundary(currentLocation))
            {
                _grid[currentLocation.Item1][currentLocation.Item2] = 'X';
                nextLocation = FindNextLocation(currentLocation, currentDirection, nextLocation);

                if (!CheckIfNextLocationIsInGridBoundary(nextLocation))
                {
                    return steps;
                }
                // Go straight if possible or change your direction right
                var moveresult = Move(currentDirection, currentLocation, nextLocation);
                currentDirection = moveresult.Item1;
                currentLocation = moveresult.Item2;

                if (_grid[currentLocation.Item1][currentLocation.Item2] != 'X')
                {
                    steps++;
                }

            }
            return steps;
        }

        part1Result += MoveUntilLeavingGrid();
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        var currentDirection = _startingInfo.Item1;
        var currentLocation = _startingInfo.Item2;

        int MoveUntilLeavingGrid((int, int) currentLocation, string currentDirection)
        {
            int paradoxCount = 0;
            var nextLocation = (0, 0);

            while (CheckIfNextLocationIsInGridBoundary(currentLocation))
            {
                _alreadyVisitedLocations.Add((currentLocation, currentDirection));

                nextLocation = FindNextLocation(currentLocation, currentDirection, nextLocation);

                if (!CheckIfNextLocationIsInGridBoundary(nextLocation))
                {
                    return paradoxCount;
                }

                // Check if adding '#' to the next location creates a paradox
                if (_grid[nextLocation.Item1][nextLocation.Item2] != '#')
                {
                    var gridCopy = _grid[nextLocation.Item1][nextLocation.Item2];
                    _grid[nextLocation.Item1][nextLocation.Item2] = '#';
                    if (MoveUntilLeavingGridOrCreatingParadox(currentLocation, currentDirection))
                    {

                        bool exists = _alreadyVisitedLocations.Any(item => item.Item1 == nextLocation);
                        if (!exists)
                        {
                            paradoxCount++;
                        }

                    }
                    _grid[nextLocation.Item1][nextLocation.Item2] = gridCopy;
                }

                // Go straight if possible or change your direction right
                var moveResult = Move(currentDirection, currentLocation, nextLocation);
                currentDirection = moveResult.Item1;
                currentLocation = moveResult.Item2;
            }
            return paradoxCount;
        }

        part2Result += MoveUntilLeavingGrid(currentLocation, currentDirection);
        Console.WriteLine($"Part 2 solution is: {part2Result}");

    }

    bool MoveUntilLeavingGridOrCreatingParadox((int, int) currentLocation, string currentDirection)
    // retunrs true if finds paradox or false, if leaves the grid = no paradox
    {

        var nextLocation = (0, 0);
        HashSet<((int, int), string)> visitedLocatinosWhenTryingToMakeParadox = new();

        while (CheckIfNextLocationIsInGridBoundary(currentLocation))
        {
            nextLocation = FindNextLocation(currentLocation, currentDirection, nextLocation);

            if (!CheckIfNextLocationIsInGridBoundary(nextLocation))
            {
                return false;
            }

            if (visitedLocatinosWhenTryingToMakeParadox.Contains((currentLocation, currentDirection)))
            {
                return true;
            }

            visitedLocatinosWhenTryingToMakeParadox.Add((currentLocation, currentDirection));

            // Go straight if possible or change your direction right
            var moveResult = Move(currentDirection, currentLocation, nextLocation);
            currentDirection = moveResult.Item1;
            currentLocation = moveResult.Item2;
        }
        return false;
    }

    private static (int, int) FindNextLocation((int, int) currentLocation, string currentDirection, (int, int) nextLocation)
    {
        switch (currentDirection)
        {
            case "up":
                nextLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
                break;
            case "down":
                nextLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
                break;
            case "left":
                nextLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
                break;
            case "right":
                nextLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
                break;
        }

        return nextLocation;
    }

    private (string, (int, int)) FindStartingDirectionAndLocation(char[][] grid)
    {
        (int, int) currentLocation = (-1, -1);
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '>')
                {
                    currentLocation = (i, j);
                    return ("right", currentLocation);
                }
                else if (grid[i][j] == '<')
                {
                    currentLocation = (i, j);
                    return ("left", currentLocation);
                }
                else if (grid[i][j] == '^')
                {
                    currentLocation = (i, j);
                    return ("up", currentLocation);
                }
                else if (grid[i][j] == 'v')
                {
                    currentLocation = (i, j);
                    return ("down", currentLocation);
                }
            }
        }
        return ("", currentLocation);
    }

    private string TurnRight(string direction)
    {
        switch (direction)
        {
            case "up":
                return "right";

            case "right":
                return "down";

            case "down":
                return "left";

            case "left":
                return "up";

        }
        return "";
    }

    private (string, (int, int)) Move(string currentDirection, (int, int) currentLocation, (int, int) nextLocation)
    {
        if (_grid[nextLocation.Item1][nextLocation.Item2] == '#')
        {
            currentDirection = TurnRight(currentDirection);
        }
        else
        {
            currentLocation = nextLocation;
        }
        return (currentDirection, currentLocation);
    }

    private bool CheckIfNextLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }


}