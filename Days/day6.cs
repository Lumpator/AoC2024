using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day6 : IDay
{
    private readonly string[] _lines;
    private string _currentDirection;

    private (int, int) _currentLocation;
    private (int, int) _nextLocation;

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

        var startingInfo = FindStartingDirectionAndLocation(_grid);
        _currentDirection = startingInfo.Item1;
        _currentLocation = startingInfo.Item2;
    }

    public void SolvePart1()
    {
        int part1Result = 0;

        int MoveUntilLeavingGrid()
        {
            int steps = 1;

            while (_currentLocation.Item1 >= 0 && _currentLocation.Item1 < _grid.Length && _currentLocation.Item2 >= 0 && _currentLocation.Item2 < _grid[0].Length)
            {
                _grid[_currentLocation.Item1][_currentLocation.Item2] = 'X';
                switch (_currentDirection)
                {
                    case "up":
                        _nextLocation = (_currentLocation.Item1 - 1, _currentLocation.Item2);
                        break;
                    case "down":
                        _nextLocation = (_currentLocation.Item1 + 1, _currentLocation.Item2);
                        break;
                    case "left":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 - 1);
                        break;
                    case "right":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 + 1);
                        break;
                }

                if (!CheckIfNextLocationIsInGridBoundary(_nextLocation))
                {
                    return steps;
                }

                // Go straight if possible or change your direction right
                var moveresult = Move(_currentDirection, _currentLocation, _nextLocation);
                _currentDirection = moveresult.Item1;
                _currentLocation = moveresult.Item2;


                if (_grid[_currentLocation.Item1][_currentLocation.Item2] != 'X')
                {
                    steps++;
                }

            }
            return steps;
        }

        //part1Result += MoveUntilLeavingGrid();
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        HashSet<(int, int)> blockadeLocations = new HashSet<(int, int)>();


        int MoveUntilLeavingGrid((int, int) currentLocation, string currentDirection)
        {
            int paradoxCount = 0;
            var nextLocation = (0, 0);

            while (currentLocation.Item1 >= 0 && currentLocation.Item1 < _grid.Length && currentLocation.Item2 >= 0 && currentLocation.Item2 < _grid[0].Length)
            {
                _alreadyVisitedLocations.Add((currentLocation, currentDirection));

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

                if (!CheckIfNextLocationIsInGridBoundary(nextLocation))
                {
                    return paradoxCount;
                }

                // Check if adding '#' to the next location creates a cycle


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
                            blockadeLocations.Add(nextLocation);
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

        part2Result += MoveUntilLeavingGrid(_currentLocation, _currentDirection);
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
        Console.WriteLine($"Blockade locations: {blockadeLocations.Count}");

    }

    bool MoveUntilLeavingGridOrCreatingParadox((int, int) currentLocation, string currentDirection)
    {

        var nextLocation = (0, 0);
        // make copy of _grid[][]
        char[][] newGrid = new char[_grid.Length][];
        for (int i = 0; i < _grid.Length; i++)
        {
            newGrid[i] = new char[_grid[i].Length];
            Array.Copy(_grid[i], newGrid[i], _grid[i].Length);
        }


        HashSet<((int, int), string)> visitedLocatinosWhenTryingToMakeParadox = new();

        while (currentLocation.Item1 >= 0 && currentLocation.Item1 < _grid.Length && currentLocation.Item2 >= 0 && currentLocation.Item2 < _grid[0].Length)
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


            switch (currentDirection)
            {
                case "up":
                    newGrid[currentLocation.Item1][currentLocation.Item2] = '^';
                    break;
                case "down":
                    newGrid[currentLocation.Item1][currentLocation.Item2] = 'v';
                    break;
                case "left":
                    newGrid[currentLocation.Item1][currentLocation.Item2] = '<';
                    break;
                case "right":
                    newGrid[currentLocation.Item1][currentLocation.Item2] = '>';
                    break;
            }

        }
        return false;

    }


    private (string, (int, int)) FindStartingDirectionAndLocation(char[][] grid)
    {
        (int, int) currentLocation = (-1, -1);
        // Find the starting direction
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